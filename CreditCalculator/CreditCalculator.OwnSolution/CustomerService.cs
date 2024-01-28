namespace CreditCalculator.OwnSolution;

public class CustomerService(CompanyRepository companyRepository, CustomerCreditServiceClient creditService, CustomerRepository customerRepository)
{
	private const decimal s_creditLimitBottomBorder = 500;

	public bool AddCustomer(string firstName, string lastName, string email, DateTime dateOfBirth, int companyId)
	{
		if (!CustomerNameValid(firstName, lastName))
		{
			return false;
		}

		if (!CustomerEmailValid(email))
		{
			return false;
		}

		if (!CustomerDateValid(dateOfBirth))
		{
			return false;
		}

		var company = companyRepository.GetById(companyId);

		var customer = new Customer {
			Company = company, DateOfBirth = dateOfBirth, EmailAddress = email, FirstName = firstName, LastName = lastName,
			HasCreditLimit = company.Type != CompanyType.VeryImportantClient
		};

		//todo: bad literals. At least should've used mapping multiplier to CompanyType
		var creditLimitMultiplier = company.Type == CompanyType.ImportantClient ? 2 : 1;

		customer.CreditLimit = creditService.GetCreditLimit(customer.FirstName, customer.LastName, customer.DateOfBirth) * creditLimitMultiplier;

		if (!HasValidCreditLimit(customer))
		{
			return false;
		}

		customerRepository.AddCustomer(customer);

		return true;
	}

	private bool HasValidCreditLimit(Customer customer)
	{
		return customer is not { HasCreditLimit: true, CreditLimit: < s_creditLimitBottomBorder };
	}

	private bool CustomerEmailValid(string email)
	{
		return email.Contains('@') || email.Contains('.');
	}

	private bool CustomerNameValid(string firstName, string lastName)
	{
		return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
	}

	private bool CustomerDateValid(DateTime dateOfBirth)
	{
		var now = DateTime.Now;
		var age = now.Year - dateOfBirth.Year;

		if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
		{
			age--;
		}

		return age >= 21;
	}
}

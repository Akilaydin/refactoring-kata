namespace CreditCalculator.OwnSolution;

public class CustomerCreditServiceClient
{
    public decimal GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth)
    {
        if (firstName == "John" && lastName == "Doe")
        {
            return 500.0m;
        }

        return DateTime.Now.Year - dateOfBirth.Year > 40 ? 600.0m : 249.9m;
    }
}
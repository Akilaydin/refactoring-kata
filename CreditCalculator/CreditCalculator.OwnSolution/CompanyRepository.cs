namespace CreditCalculator.OwnSolution;

public class CompanyRepository
{
    private readonly List<Company> _companies = 
    [
        new Company { Id = 1, Type = CompanyType.RegularClient },
        new Company { Id = 2, Type = CompanyType.ImportantClient },
        new Company { Id = 3, Type = CompanyType.VeryImportantClient },
    ];

    public Company GetById(int companyId)
    {
        return _companies.Single(c => c.Id == companyId);
    }
}
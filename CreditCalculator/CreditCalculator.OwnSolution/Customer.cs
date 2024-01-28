namespace CreditCalculator.OwnSolution;

public class Customer
{
    public required Company Company { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string EmailAddress { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public bool HasCreditLimit { get; init; }
    public decimal? CreditLimit { get; set; }
}
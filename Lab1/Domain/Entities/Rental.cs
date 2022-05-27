namespace Lab1.Domain.Entities;

public class Rental
{
    public int Id { get; set; }
    public DateTimeOffset IssueDate { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public decimal Pledge { get; set; }
    public decimal RentalPrice { get; set; }
    public int CarId { get; set; }
    public int ClientId { get; set; }

    public override string ToString()
    {
        return $"IssueDate: {IssueDate.LocalDateTime} - DueDate - {DueDate.LocalDateTime}" +
               $" - Pledge - {Pledge} - RentalPrice - {RentalPrice}";
    }
}
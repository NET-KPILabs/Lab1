using Lab1.Domain.Enums;

namespace Lab1.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ushort IssueYear { get; set; }
    public CarType CarType { get; set; }
    public decimal Price { get; set; }
    public decimal PricePerDay { get; set; }
    public int CarMakeId { get; set; }

    public override string ToString()
    {
        return $"{Name} - {IssueYear} - {CarType} - Price: {Price} - PricePerDay: {PricePerDay}";
    }
}
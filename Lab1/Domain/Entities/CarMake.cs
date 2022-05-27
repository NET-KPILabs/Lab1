namespace Lab1.Domain.Entities;

public class CarMake
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Manufacturer: {Name}";
    }
}
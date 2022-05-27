namespace Lab1.Domain.Entities;

public class Address
{
    public int Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Country} - {City} - {Street} - {Building} - PostalCode: {PostalCode}";
    }
}
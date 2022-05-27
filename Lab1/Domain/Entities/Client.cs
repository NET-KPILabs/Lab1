namespace Lab1.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int AddressId { get; set; }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Patronymic} - {PhoneNumber}";
    }
}
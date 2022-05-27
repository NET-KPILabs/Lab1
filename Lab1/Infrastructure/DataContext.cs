using Lab1.Domain.Entities;

namespace Lab1.Infrastructure;

public class DataContext
{
    public ICollection<Client> Clients { get; set; } = new List<Client>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    public ICollection<Car> Cars { get; set; } = new List<Car>();
    public ICollection<CarMake> CarMakes { get; set; } = new List<CarMake>();
}
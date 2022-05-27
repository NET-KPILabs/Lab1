using Bogus;
using Lab1.Domain.Entities;
using Lab1.Domain.Enums;

namespace Lab1.Infrastructure;

public static class DataContextExtensions
{
    public static void Seed(this DataContext dataContext)
    {
        var addresses = GenerateRandomAddresses(10);
        var clients = GenerateRandomClients(addresses, 10);
        var carMakes = GenerateRandomCarMakes(10);
        var cars = GenerateRandomCars(carMakes, 10);
        var rentals = GenerateRandomRentals(clients, cars, 10);

        dataContext.Addresses = addresses;
        dataContext.Clients = clients;
        dataContext.CarMakes = carMakes;
        dataContext.Cars = cars;
        dataContext.Rentals = rentals;
    }

    private static IList<Address> GenerateRandomAddresses(int count)
    {
        Faker.GlobalUniqueIndex = 0;

        return new Faker<Address>()
            .RuleFor(p => p.Id, f => f.IndexGlobal)
            .RuleFor(p => p.Country, f => f.Address.Country())
            .RuleFor(p => p.City, f => f.Address.City())
            .RuleFor(p => p.Street, f => f.Address.StreetName())
            .RuleFor(p => p.Building, f => f.Address.BuildingNumber())
            .RuleFor(p => p.PostalCode, f => f.Address.ZipCode())
            .Generate(count);
    }

    private static IList<Client> GenerateRandomClients(IList<Address> addresses, int count)
    {
        Faker.GlobalUniqueIndex = 0;

        return new Faker<Client>()
            .RuleFor(p => p.Id, f => f.IndexGlobal)
            .RuleFor(p => p.FirstName, f => f.Person.FirstName)
            .RuleFor(p => p.LastName, f => f.Person.LastName)
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber("+380-###-##-##"))
            .RuleFor(p => p.AddressId, f => f.PickRandom(addresses).Id)
            .Generate(count);
    }

    private static IList<CarMake> GenerateRandomCarMakes(int count)
    {
        Faker.GlobalUniqueIndex = 0;

        return new Faker<CarMake>()
            .RuleFor(p => p.Id, f => f.IndexGlobal)
            .RuleFor(p => p.Name, f => f.Vehicle.Manufacturer())
            .Generate(count)
            .DistinctBy(c => c.Name)
            .ToList();
    }

    private static IList<Car> GenerateRandomCars(IList<CarMake> carMakes, int count)
    {
        Faker.GlobalUniqueIndex = 0;

        return new Faker<Car>()
            .RuleFor(p => p.Id, f => f.IndexGlobal)
            .RuleFor(p => p.Name, f => f.Vehicle.Model())
            .RuleFor(p => p.IssueYear, f => f.Random.UShort(1980, 2022))
            .RuleFor(p => p.CarType, f => f.PickRandom<CarType>())
            .RuleFor(p => p.Price, f => f.Random.Int(1000, 50000))
            .RuleFor(p => p.PricePerDay, f => f.Random.Int(20, 50))
            .RuleFor(p => p.CarMakeId, f => f.PickRandom(carMakes).Id)
            .Generate(count);
    }

    private static IList<Rental> GenerateRandomRentals(IList<Client> clients, IList<Car> cars, int count)
    {
        Faker.GlobalUniqueIndex = 0;

        return new Faker<Rental>()
            .RuleFor(p => p.Id, f => f.IndexGlobal)
            .RuleFor(p => p.Pledge, f => f.Random.Int(300, 1000))
            .RuleFor(p => p.ClientId, f => f.PickRandom(clients).Id)
            .FinishWith((f, r) =>
            {
                var car = f.PickRandom(cars);
                r.IssueDate = f.Date.BetweenOffset(DateTimeOffset.Now - TimeSpan.FromDays(30), DateTimeOffset.Now);
                r.DueDate = f.Date.BetweenOffset(r.IssueDate, DateTimeOffset.Now);
                r.CarId = car.Id;
                var profit = Math.Round((r.DueDate - r.IssueDate).TotalDays * Decimal.ToDouble(car.PricePerDay), 2);
                r.RentalPrice = (decimal) profit;
            })
            .Generate(count);
    }
}
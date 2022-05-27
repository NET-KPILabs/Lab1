using Lab1.Application.ViewModels;
using Lab1.Domain.Entities;
using Lab1.Domain.Enums;
using Lab1.Infrastructure;

namespace Lab1.Application;

public class QueryService
{
    private readonly DataContext _context;

    public QueryService(DataContext context)
    {
        _context = context;
    }

    public decimal GetProfit(DateTimeOffset dateStartFrom)
    {
        return _context.Rentals
            .Where(r => r.DueDate >= dateStartFrom)
            .Sum(r => r.RentalPrice);
    }
    
    public IEnumerable<Car> GetAllCars()
    {
        return from car in _context.Cars
            select car;
    }
    
    public IEnumerable<Rental> GetAllRentalsSortedByPrice()
    {
        return from rental in _context.Rentals
            orderby rental.RentalPrice descending
            select rental;
    }

    public IEnumerable<ClientProfit> GetClientsSortedByProfit()
    {
        return _context.Clients
            .GroupJoin(_context.Rentals,
                c => c.Id,
                r => r.ClientId,
                (c, r) => new ClientProfit()
                {
                    Client = c,
                    TotalProfit = r.Sum(rental => rental.RentalPrice)
                })
            .OrderByDescending(t => t.TotalProfit);
    }

    public IEnumerable<CarInfo> GetCarsByType(CarType carType)
    {
        return _context.Cars
            .Join(_context.CarMakes,
                c => c.CarMakeId,
                cm => cm.Id,
                (c, cm) => new CarInfo()
                {
                    Car = c,
                    CarMake = cm
                })
            .Where(c => c.Car.CarType == carType);
    }

    public IEnumerable<ClientInfo> GetClientsInfos()
    {
        return _context.Clients
            .Join(_context.Addresses,
                c => c.AddressId,
                a => a.Id,
                (c, a) => new ClientInfo()
                {
                    Client = c,
                    Address = a
                });
    }

    public IEnumerable<Car> GetAvailableCarsAtTheMoment()
    {
        return _context.Rentals
            .Where(rental => rental.DueDate < DateTimeOffset.Now)
            .Join(_context.Cars,
                r => r.CarId,
                c => c.Id,
                (r, c) => c)
            .DistinctBy(c => c.Id);
    }

    public decimal GetAverageCarsTypeRentalPrice(CarType carType)
    {
        return _context.Cars
            .Where(c => c.CarType == carType)
            .Average(c => c.PricePerDay);
    }

    public IDictionary<CarType, int> GetCarsQuantityByType()
    {
        return _context.Cars
            .GroupBy(c => c.CarType)
            .ToDictionary(key => key.Key,
                value => value.Count());
    }

    public Car GetCarWithTheMostExpensiveRentalPrice()
    {
        return _context.Cars.MaxBy(c => c.PricePerDay) ?? 
               throw new InvalidOperationException("There`re no cars");
    }

    public IEnumerable<CarMake> GetManufacturersOrderedByPopularity()
    {
        return _context.CarMakes
            .GroupJoin(_context.Cars,
                cm => cm.Id,
                c => c.CarMakeId,
                (cm, c) => new
                {
                    CarMake = cm,
                    RentalsQuantity = c.GroupJoin(_context.Rentals,
                        car => car.Id,
                        r => r.CarId,
                        (car, r) => new
                        {
                            Car = car,
                            CarRentalsQuantity = r.Count()
                        })
                        .Sum(cr => cr.CarRentalsQuantity)
                })
            .OrderByDescending(r => r.RentalsQuantity)
            .Select(r => r.CarMake);
    }

    public IEnumerable<Car> GetCarsIssuedAfter(int startFromYear)
    {
        return from car in _context.Cars
            where car.IssueYear > startFromYear
            select car;
    }

    public IEnumerable<Client> GetClientsSortedByFullName()
    {
        return from client in _context.Clients
            orderby client.LastName, client.FirstName
            select client;
    }

    public IEnumerable<Address> GetAllClientsAddresses()
    {
        return _context.Addresses
            .Join(_context.Clients,
                a => a.Id,
                c => c.AddressId,
                (a, c) => new ClientInfo()
                {
                    Client = c,
                    Address = a
                })
            .Select(r => r.Address)
            .DistinctBy(a => a.Id);
    }

    public Car GetTheMostPopularCar()
    {
        return _context.Cars
            .GroupJoin(_context.Rentals,
                c => c.Id,
                r => r.CarId,
                (c, r) => new
                {
                    Car = c,
                    RentalsQuantity = r.Count()
                })
            .OrderByDescending(r => r.RentalsQuantity)
            .Select(r => r.Car)
            .First();
    }
}
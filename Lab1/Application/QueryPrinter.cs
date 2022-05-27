using Lab1.Domain.Entities;
using Lab1.Domain.Enums;

namespace Lab1.Application;

public class QueryPrinter
{
    private readonly QueryService _queryService;

    public QueryPrinter(QueryService queryService)
    {
        _queryService = queryService;
    }

    public void PrintProfit(DateTimeOffset dateStartFrom)
    {
        Console.WriteLine(_queryService.GetProfit(dateStartFrom));
    }

    public void PrintAllCars()
    {
        PrintCars(_queryService.GetAllCars());
    }

    public void PrintClientsSortedByProfit()
    {
        var clients = _queryService.GetClientsSortedByProfit();

        foreach (var client in clients)
        {
            Console.WriteLine($"{client.Client} - {client.TotalProfit}");
        }
    }

    public void PrintCarsByType(CarType carType)
    {
        var cars = _queryService.GetCarsByType(carType);

        foreach (var car in cars)
        {
            Console.WriteLine(car.Car);
            Console.WriteLine(car.CarMake);
        }
    }

    public void PrintClientsInfos()
    {
        var clients = _queryService.GetClientsInfos();

        foreach (var client in clients)
        {
            Console.WriteLine(client.Client);
            Console.WriteLine(client.Address);
        }
    }

    public void PrintAvailableCarsAtTheMoment()
    {
        PrintCars(_queryService.GetAvailableCarsAtTheMoment());
    }

    public void PrintAverageCarsTypeRentalPrice(CarType carType)
    {
        Console.WriteLine(_queryService.GetAverageCarsTypeRentalPrice(carType).ToString(".##"));
    }

    public void PrintCarsQuantityByType()
    {
        var result = _queryService.GetCarsQuantityByType();

        foreach (var pair in result)
        {
            Console.WriteLine($"{pair.Key} - {pair.Value}");
        }
    }

    public void PrintRentalsSortedByPrice()
    {
        var rentals = _queryService.GetAllRentalsSortedByPrice();
        
        foreach (var rental in rentals)
        {
            Console.WriteLine(rental);
        }
    }

    public void PrintCarWithTheMostExpensiveRentalPrice()
    {
        Console.WriteLine(_queryService.GetCarWithTheMostExpensiveRentalPrice());
    }

    public void PrintManufacturersOrderedByPopularity()
    {
        var manufacturers = _queryService.GetManufacturersOrderedByPopularity();

        foreach (var manufacturer in manufacturers)
        {
            Console.WriteLine(manufacturer);
        }
    }

    public void PrintCarsIssuedAfter(int startFromYear)
    {
        PrintCars(_queryService.GetCarsIssuedAfter(startFromYear));
    }

    public void PrintClientsSortedByFullName()
    {
        var clients = _queryService.GetClientsSortedByFullName();

        foreach (var client in clients)
        {
            Console.WriteLine(client);
        }
    }

    public void PrintAllClientsAddresses()
    {
        var addresses = _queryService.GetAllClientsAddresses();

        foreach (var address in addresses)
        {
            Console.WriteLine(address);
        }
    }

    public void PrintTheMostPopularCar()
    {
        Console.WriteLine(_queryService.GetTheMostPopularCar());
    }

    private void PrintCars(IEnumerable<Car> cars)
    {
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }
}
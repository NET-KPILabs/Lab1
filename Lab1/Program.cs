using Lab1.Application;
using Lab1.Domain.Enums;
using Lab1.Infrastructure;

var context = new DataContext();
context.Seed();

var service = new QueryService(context);
var queryPrinter = new QueryPrinter(service);

Console.WriteLine("1. Weekly profit");
queryPrinter.PrintProfit(DateTimeOffset.Now - TimeSpan.FromDays(7));

Console.WriteLine("\n2. All cars\n");
queryPrinter.PrintAllCars();

Console.WriteLine("\n3. All rentals`re sorted by price\n");
queryPrinter.PrintRentalsSortedByPrice();

Console.WriteLine("\n4. All clients`re sorted by profit\n");
queryPrinter.PrintClientsSortedByProfit();

Console.WriteLine("\n5. All transport cars\n");
queryPrinter.PrintCarsByType(CarType.Transport);

Console.WriteLine("\n6. All clients with detail info\n");
queryPrinter.PrintClientsInfos();

Console.WriteLine("\n7. All available cars at the moment\n");
queryPrinter.PrintAvailableCarsAtTheMoment();

Console.WriteLine("\n8. The average special cars rental price\n");
queryPrinter.PrintAverageCarsTypeRentalPrice(CarType.Special);

Console.WriteLine("\n9. Cars quantity by different types\n");
queryPrinter.PrintCarsQuantityByType();

Console.WriteLine("\n10. The cars with the most expensive rental price\n");
queryPrinter.PrintCarWithTheMostExpensiveRentalPrice();

Console.WriteLine("\n11. The manufactures`re ordered by popularity\n");
queryPrinter.PrintManufacturersOrderedByPopularity();

Console.WriteLine($"\n12. The cars`re issued after {DateTime.UtcNow.Year - 10}\n");
queryPrinter.PrintCarsIssuedAfter(DateTime.UtcNow.Year - 10);

Console.WriteLine("\n13. All clients`re sorted by full name\n");
queryPrinter.PrintClientsSortedByFullName();

Console.WriteLine("\n14. All clients addresses\n");
queryPrinter.PrintAllClientsAddresses();

Console.WriteLine("\n15. The most popular car\n");
queryPrinter.PrintTheMostPopularCar();



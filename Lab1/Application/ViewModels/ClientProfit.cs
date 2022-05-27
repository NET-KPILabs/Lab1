using Lab1.Domain.Entities;

namespace Lab1.Application.ViewModels;

public class ClientProfit
{
    public Client Client { get; set; }
    public decimal TotalProfit { get; set; }
}
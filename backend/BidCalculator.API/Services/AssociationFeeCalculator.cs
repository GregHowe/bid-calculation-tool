using BidCalculator.API.Interfaces;

namespace BidCalculator.API.Services;

public class AssociationFeeCalculator : IAssociationFeeCalculator
{
    public decimal Calculate(decimal price)
    {
        return price switch
        {
            <= 500 => 5m,
            <= 1000 => 10m,
            <= 3000 => 15m,
            _ => 20m
        };
    }
}
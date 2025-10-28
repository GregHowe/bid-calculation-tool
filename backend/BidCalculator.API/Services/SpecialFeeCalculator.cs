
using BidCalculator.API.Interfaces;
using BidCalculator.API.Models;


namespace BidCalculator.API.Services;

public class SpecialFeeCalculator : ISpecialFeeCalculator
{
    public decimal Calculate(decimal price, VehicleType type)
    {
        var fee = type switch
        {
            VehicleType.Common => price * 0.02m,
            VehicleType.Luxury => price * 0.04m,
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown vehicle type")
        };

        return Math.Round(fee, 2, MidpointRounding.AwayFromZero);
    }
}
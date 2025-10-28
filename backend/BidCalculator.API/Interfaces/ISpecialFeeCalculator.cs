using BidCalculator.API.Models;

namespace BidCalculator.API.Interfaces;

public interface ISpecialFeeCalculator
{
    decimal Calculate(decimal price, VehicleType type);
}



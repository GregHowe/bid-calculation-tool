namespace BidCalculator.API.Interfaces;

    using BidCalculator.API.Models;

    public interface IBasicFeeCalculator
    {
        decimal Calculate(decimal price, VehicleType type);
    }

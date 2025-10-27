using BidCalculator.API.Models;

public interface ISpecialFeeCalculator
{
    decimal Calculate(decimal price, VehicleType type);
}

public class SpecialFeeCalculator : ISpecialFeeCalculator
{
    public decimal Calculate(decimal price, VehicleType type)
    {
        return type switch
        {
            VehicleType.Common => price * 0.02m,
            VehicleType.Luxury => price * 0.04m,
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown vehicle type")
        };
    }
}

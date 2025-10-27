using BidCalculator.API.Models;

public interface IBasicFeeCalculator
{
    decimal Calculate(decimal price, VehicleType type);
}

public class BasicFeeCalculator : IBasicFeeCalculator
{
    public decimal Calculate(decimal price, VehicleType type)
    {
        var fee = price * 0.10m;

        return type switch
        {
            VehicleType.Common => Math.Clamp(fee, 10m, 50m),
            VehicleType.Luxury => Math.Clamp(fee, 25m, 200m),
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown vehicle type")
        };
    }
}

using BidCalculator.API.Interfaces;

namespace BidCalculator.API.Services;

public class StorageFeeCalculator : IStorageFeeCalculator
{
    public decimal Calculate()
    {
        return Math.Round(100m, 2, MidpointRounding.AwayFromZero);
    }
}
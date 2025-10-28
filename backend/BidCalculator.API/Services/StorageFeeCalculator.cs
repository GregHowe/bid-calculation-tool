using BidCalculator.API.Interfaces;

namespace BidCalculator.API.Services;

public class StorageFeeCalculator : IStorageFeeCalculator
{
    public decimal Calculate()
    {
        return 100m;
    }
}
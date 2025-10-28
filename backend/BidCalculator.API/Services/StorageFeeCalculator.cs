public class StorageFeeCalculator : IStorageFeeCalculator
{
    public decimal Calculate()
    {
        return Math.Round(100m, 2, MidpointRounding.AwayFromZero);
    }
}
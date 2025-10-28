public class AssociationFeeCalculator : IAssociationFeeCalculator
{
    public decimal Calculate(decimal price)
    {
        var fee = price switch
        {
            <= 500 => 5m,
            <= 1000 => 10m,
            <= 3000 => 15m,
            _ => 20m
        };

        return Math.Round(fee, 2, MidpointRounding.AwayFromZero);
    }
}
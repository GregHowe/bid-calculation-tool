namespace BidCalculator.API.Interfaces;

public interface IAssociationFeeCalculator
    {
        decimal Calculate(decimal price);
    }

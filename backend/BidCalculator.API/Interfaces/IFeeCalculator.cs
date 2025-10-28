using BidCalculator.API.Models;

namespace BidCalculator.API.Interfaces;

    public interface IFeeCalculator
    {
        FeeResult CalculateFees(VehicleInput input);
    }

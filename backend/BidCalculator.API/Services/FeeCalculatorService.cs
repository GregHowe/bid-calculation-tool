using BidCalculator.API.Interfaces;
using BidCalculator.API.Models;

namespace BidCalculator.API.Services;

    public class FeeCalculatorService : IFeeCalculator
    {
        private readonly IBasicFeeCalculator _basicFeeCalculator;
        private readonly ISpecialFeeCalculator _specialFeeCalculator;
        private readonly IAssociationFeeCalculator _associationFeeCalculator;
        private readonly IStorageFeeCalculator _storageFeeCalculator;

        public FeeCalculatorService(
            IBasicFeeCalculator basicFeeCalculator,
            ISpecialFeeCalculator specialFeeCalculator,
            IAssociationFeeCalculator associationFeeCalculator,
            IStorageFeeCalculator storageFeeCalculator)
        {
            _basicFeeCalculator = basicFeeCalculator;
            _specialFeeCalculator = specialFeeCalculator;
            _associationFeeCalculator = associationFeeCalculator;
            _storageFeeCalculator = storageFeeCalculator;
        }

        public FeeResult CalculateFees(VehicleInput input)
        {
            var price = input.Price;
            var type = input.Type;

            // Basic buyer fee
            decimal basicFee = _basicFeeCalculator.Calculate(price, type);

            // Seller's special fee
            decimal specialFee = _specialFeeCalculator.Calculate(price, type);

            // Association fee
           decimal associationFee = _associationFeeCalculator.Calculate(price);

            // Fixed storage fee
            decimal storageFee = _storageFeeCalculator.Calculate();

            // Total
            decimal total = Math.Round(price + basicFee + specialFee + associationFee + storageFee, 2, MidpointRounding.AwayFromZero);

            return new FeeResult
            {
                BasicFee = basicFee,
                SpecialFee = specialFee,
                AssociationFee = associationFee,
                StorageFee = storageFee,
                Total = total
            };
        }
    }


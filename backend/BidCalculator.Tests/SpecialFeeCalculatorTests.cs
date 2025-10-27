using BidCalculator.API.Services;
using BidCalculator.API.Models;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Tests
{
    public class SpecialFeeCalculatorTests
    {
        [Theory]
        [InlineData(1000, VehicleType.Common, 20)]
        [InlineData(1000, VehicleType.Luxury, 40)]
        [InlineData(50000, VehicleType.Common, 1000)]
        [InlineData(50000, VehicleType.Luxury, 2000)]
        public void Calculate_ReturnsExpectedSpecialFee(decimal price, VehicleType type, decimal expected)
        {
            var calculator = new SpecialFeeCalculator();
            var result = calculator.Calculate(price, type);
            result.Should().Be(expected);
        }
    }
}

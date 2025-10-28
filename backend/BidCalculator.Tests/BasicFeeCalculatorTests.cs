using BidCalculator.API.Services;
using BidCalculator.API.Models;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Tests
{
    public class BasicFeeCalculatorTests
    {
        [Theory]
        [InlineData(50, VehicleType.Common, 10)]     // 5 → minimum is 10
        [InlineData(398, VehicleType.Common, 39.8)]   // within allowed range
        [InlineData(1000, VehicleType.Common, 50)]    // 100 → maximum is 50
        [InlineData(1800, VehicleType.Luxury, 180)]   // within allowed range
        [InlineData(1000000, VehicleType.Luxury, 200)]// 100000 → maximum is 200
        public void Calculate_ReturnsExpectedFee(decimal price, VehicleType type, decimal expected)
        {
            var calculator = new BasicFeeCalculator();
            var result = calculator.Calculate(price, type);
            result.Should().Be(expected);
        }
    }
}

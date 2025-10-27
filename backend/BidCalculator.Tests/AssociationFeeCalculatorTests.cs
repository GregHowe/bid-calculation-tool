using BidCalculator.API.Services;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Tests
{
    public class AssociationFeeCalculatorTests
    {
        [Theory]
        [InlineData(1, 5)]
        [InlineData(500, 5)]
        [InlineData(501, 10)]
        [InlineData(1000, 10)]
        [InlineData(1001, 15)]
        [InlineData(3000, 15)]
        [InlineData(3001, 20)]
        [InlineData(100000, 20)]
        public void Calculate_ReturnsExpectedAssociationFee(decimal price, decimal expected)
        {
            var calculator = new AssociationFeeCalculator();
            var result = calculator.Calculate(price);
            result.Should().Be(expected);
        }
    }
}

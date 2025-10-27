using BidCalculator.API.Services;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Tests
{
    public class StorageFeeCalculatorTests
    {
        [Fact]
        public void Calculate_AlwaysReturnsFixedFee()
        {
            var calculator = new StorageFeeCalculator();
            var result = calculator.Calculate();
            result.Should().Be(100m);
        }
    }
}

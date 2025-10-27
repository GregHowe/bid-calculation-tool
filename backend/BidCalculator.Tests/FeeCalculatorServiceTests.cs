using Moq;
using BidCalculator.API.Interfaces;
using BidCalculator.API.Models;
using BidCalculator.API.Services;
using FluentAssertions;

namespace BidCalculator.Tests
{
    public class FeeCalculatorServiceTests
    {
        [Fact]
        public void CalculateFees_CommonVehicle_ReturnsExpectedFees()
        {
            // Arrange
            var input = new VehicleInput { Price = 1000m, Type = VehicleType.Common };

            var mockBasic = new Mock<IBasicFeeCalculator>();
            mockBasic.Setup(x => x.Calculate(1000m, VehicleType.Common)).Returns(50m);

            var mockSpecial = new Mock<ISpecialFeeCalculator>();
            mockSpecial.Setup(x => x.Calculate(1000m, VehicleType.Common)).Returns(20m);

            var mockAssociation = new Mock<IAssociationFeeCalculator>();
            mockAssociation.Setup(x => x.Calculate(1000m)).Returns(10m);

            var mockStorage = new Mock<IStorageFeeCalculator>();
            mockStorage.Setup(x => x.Calculate()).Returns(100m);

            var service = new FeeCalculatorService(
                mockBasic.Object,
                mockSpecial.Object,
                mockAssociation.Object,
                mockStorage.Object);

            // Act
            var result = service.CalculateFees(input);

            // Assert
            result.BasicFee.Should().Be(50m);
            result.SpecialFee.Should().Be(20m);
            result.AssociationFee.Should().Be(10m);
            result.StorageFee.Should().Be(100m);
            result.Total.Should().Be(1180m);
        }

        [Fact]
        public void CalculateFees_LuxuryVehicle_ReturnsExpectedFees()
        {
            // Arrange
            var input = new VehicleInput { Price = 5000m, Type = VehicleType.Luxury };

            var mockBasic = new Mock<IBasicFeeCalculator>();
            mockBasic.Setup(x => x.Calculate(5000m, VehicleType.Luxury)).Returns(200m);

            var mockSpecial = new Mock<ISpecialFeeCalculator>();
            mockSpecial.Setup(x => x.Calculate(5000m, VehicleType.Luxury)).Returns(200m);

            var mockAssociation = new Mock<IAssociationFeeCalculator>();
            mockAssociation.Setup(x => x.Calculate(5000m)).Returns(20m);

            var mockStorage = new Mock<IStorageFeeCalculator>();
            mockStorage.Setup(x => x.Calculate()).Returns(100m);

            var service = new FeeCalculatorService(
                mockBasic.Object,
                mockSpecial.Object,
                mockAssociation.Object,
                mockStorage.Object);

            // Act
            var result = service.CalculateFees(input);

            // Assert
            result.BasicFee.Should().Be(200m);
            result.SpecialFee.Should().Be(200m);
            result.AssociationFee.Should().Be(20m);
            result.StorageFee.Should().Be(100m);
            result.Total.Should().Be(5520m);
        }

        [Fact]
        public void CalculateFees_CommonVehicle_LowPrice_ReturnsMinimumBasicFee()
        {
            // Arrange
            var input = new VehicleInput { Price = 50m, Type = VehicleType.Common };

            var mockBasic = new Mock<IBasicFeeCalculator>();
            mockBasic.Setup(x => x.Calculate(50m, VehicleType.Common)).Returns(10m);

            var mockSpecial = new Mock<ISpecialFeeCalculator>();
            mockSpecial.Setup(x => x.Calculate(50m, VehicleType.Common)).Returns(1m);

            var mockAssociation = new Mock<IAssociationFeeCalculator>();
            mockAssociation.Setup(x => x.Calculate(50m)).Returns(5m);

            var mockStorage = new Mock<IStorageFeeCalculator>();
            mockStorage.Setup(x => x.Calculate()).Returns(100m);

            var service = new FeeCalculatorService(
                mockBasic.Object,
                mockSpecial.Object,
                mockAssociation.Object,
                mockStorage.Object);

            // Act
            var result = service.CalculateFees(input);

            // Assert
            result.BasicFee.Should().Be(10m);
            result.SpecialFee.Should().Be(1m);
            result.AssociationFee.Should().Be(5m);
            result.StorageFee.Should().Be(100m);
            result.Total.Should().Be(166m);
        }

        [Fact]
        public void CalculateFees_CommonVehicle_HighPrice_ReturnsMaximumBasicFee()
        {
            // Arrange
            var input = new VehicleInput { Price = 100000m, Type = VehicleType.Common };

            var mockBasic = new Mock<IBasicFeeCalculator>();
            mockBasic.Setup(x => x.Calculate(100000m, VehicleType.Common)).Returns(50m);

            var mockSpecial = new Mock<ISpecialFeeCalculator>();
            mockSpecial.Setup(x => x.Calculate(100000m, VehicleType.Common)).Returns(2000m);

            var mockAssociation = new Mock<IAssociationFeeCalculator>();
            mockAssociation.Setup(x => x.Calculate(100000m)).Returns(20m);

            var mockStorage = new Mock<IStorageFeeCalculator>();
            mockStorage.Setup(x => x.Calculate()).Returns(100m);

            var service = new FeeCalculatorService(
                mockBasic.Object,
                mockSpecial.Object,
                mockAssociation.Object,
                mockStorage.Object);

            // Act
            var result = service.CalculateFees(input);

            // Assert
            result.BasicFee.Should().Be(50m);
            result.SpecialFee.Should().Be(2000m);
            result.AssociationFee.Should().Be(20m);
            result.StorageFee.Should().Be(100m);
            result.Total.Should().Be(102170m);
        }

        [Fact]
        public void CalculateFees_LuxuryVehicle_HighPrice_ReturnsMaximumBasicFee()
        {
            // Arrange
            var input = new VehicleInput { Price = 100000m, Type = VehicleType.Luxury };

            var mockBasic = new Mock<IBasicFeeCalculator>();
            mockBasic.Setup(x => x.Calculate(100000m, VehicleType.Luxury)).Returns(200m);

            var mockSpecial = new Mock<ISpecialFeeCalculator>();
            mockSpecial.Setup(x => x.Calculate(100000m, VehicleType.Luxury)).Returns(4000m);

            var mockAssociation = new Mock<IAssociationFeeCalculator>();
            mockAssociation.Setup(x => x.Calculate(100000m)).Returns(20m);

            var mockStorage = new Mock<IStorageFeeCalculator>();
            mockStorage.Setup(x => x.Calculate()).Returns(100m);

            var service = new FeeCalculatorService(
                mockBasic.Object,
                mockSpecial.Object,
                mockAssociation.Object,
                mockStorage.Object);

            // Act
            var result = service.CalculateFees(input);

            // Assert
            result.BasicFee.Should().Be(200m);
            result.SpecialFee.Should().Be(4000m);
            result.AssociationFee.Should().Be(20m);
            result.StorageFee.Should().Be(100m);
            result.Total.Should().Be(104320m);
        }
    }
}

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BidCalculator.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BidCalculator.IntegrationTests
{
    public class FeesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FeesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_CalculateFees_ReturnsExpectedResult()
        {
            var input = new VehicleInput
            {
                Price = 1000,
                Type = VehicleType.Common
            };

            var response = await _client.PostAsJsonAsync("/api/FeeCalculator", input);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FeeResult>();

            Assert.Equal(50, result.BasicFee);
            Assert.Equal(20, result.SpecialFee);
            Assert.Equal(10, result.AssociationFee);
            Assert.Equal(100, result.StorageFee);
            Assert.Equal(1180, result.Total);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using YourBestDestination.API.Controllers;
using YourBestDestination.Domain.Abstractions;
using YourBestDestination.Domain.Dto;
using YourBestDestination.Domain.Models.Paging;
using Moq;

namespace YourBestDestination.Tests.Controllers
{
    public class HotelSearchControllerUnitTests
    {

        public HotelSearchControllerUnitTests()
        {
        }

        [Fact]
        public async Task GetClosestLocations_ReturnsExpectedResult()
        {
            // Arrange
            var mockLocationService = new Mock<ILocationService>();

            double latitude = 37.785834;
            double longitude = -122.400811;
            int page = 1;
            int size = 10;
            var hotels = new List<HotelDto>
            {
                new HotelDto { Id = Guid.NewGuid(), Name = "Hotel 1", Latitude = 37.785834, Longitude = -122.400811, Price = 170 },
                new HotelDto { Id = Guid.NewGuid(), Name = "Hotel 2", Latitude = 37.785834, Longitude = -122.400812, Price = 123 }
            };

            var expectedHotels = new PaginatedList<HotelDto>(hotels, hotels.Count, page, size);

            mockLocationService.Setup(x => x.GetClosestLocations(latitude, longitude, page, size))
                .ReturnsAsync(expectedHotels);

            var controller = new HotelSearchController(mockLocationService.Object);

            // Act
            var result = await controller.GetClosestLocations(latitude, longitude, page, size);

            // Assert
            var okObjectResult = result as OkObjectResult;
            var hotelsResult = okObjectResult?.Value as PaginatedList<HotelDto>;
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.Equal(expectedHotels.Items, hotelsResult?.Items);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using YourBestDestination.API.Controllers;
using YourBestDestination.Domain.Abstractions;
using YourBestDestination.Domain.Dto;
using YourBestDestination.Domain.Models.Paging;
using Moq;

namespace YourBestDestination.Tests.Controllers
{
    public class HotelContollerUnitTests
    {

        public HotelContollerUnitTests()
        {
        }

        [Fact]
        public async void GetHotels_Ok()
        {
            // Arrange
            var hotels = MockData.Hotels;

            var mockHotelService = new Mock<IHotelService>();

            mockHotelService
                .Setup(x => x.GetHotelsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(hotels);

            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.GetHotels();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.IsType<PaginatedList<HotelDto>>(okResult.Value);

            var returnedHotels = okResult.Value as PaginatedList<HotelDto>;

            Assert.Equal(hotels.Items.Count, returnedHotels?.Items.Count);
            Assert.Equal(hotels.Total, returnedHotels?.Total);
            Assert.Equal(hotels.Page, returnedHotels?.Page);
            Assert.Equal(hotels.Size, returnedHotels?.Size);

            Assert.Equal(hotels.Items[0].Id, returnedHotels?.Items[0].Id);
            Assert.Equal(hotels.Items[0].Name, returnedHotels?.Items[0].Name);
            Assert.Equal(hotels.Items[0].Price, returnedHotels?.Items[0].Price);
        }

        [Fact]
        public async void GetById_Ok()
        {

            // Arrange
            var hotelId = Guid.NewGuid();
            var hotel = new HotelDto { Id = hotelId, Name = "Hotel 1", Price = 100 };

            var mockHotelService = new Mock<IHotelService>();
            mockHotelService
                .Setup(x => x.GetHotelByIdAsync(It.Is<Guid>(id => id == hotelId)))
                .ReturnsAsync(hotel);

            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.GetById(hotelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<HotelDto>(okResult.Value);

            var returnedHotel = okResult.Value as HotelDto;

            Assert.Equal(hotelId, returnedHotel?.Id);
            Assert.Equal(hotel.Name, returnedHotel?.Name);
            Assert.Equal(hotel.Price, returnedHotel?.Price);
        }

        [Fact]
        public async Task GetById_InvalidGuid_BadRequest()
        {
            // Arrange
            var invalidGuid = Guid.Empty;
            var mockHotelService = new Mock<IHotelService>();
            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.GetById(invalidGuid);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid hotel id.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetById_Hotel_NotFound()
        {
            // Arrange
            var validGuid = Guid.NewGuid();
            var mockHotelService = new Mock<IHotelService>();
            mockHotelService
                .Setup(x => x.GetHotelByIdAsync(It.Is<Guid>(id => id == validGuid)))
                .Returns(Task.FromResult<HotelDto>(null));

            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.GetById(validGuid);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Cannot find Hotel with {validGuid} id.", notFoundResult.Value);
        }

        [Fact]
        public async void UpdateHotel_Ok()
        {

            // Arrange
            var hotelId = MockData.Hotels.Items.First().Id;
            var hotel = new HotelCreateUpdateDto
            {
                Name = "Updated hotel name",
                Price = 230,
                Latitude = (new Random().NextDouble() * 180) - 90,
                Longitude = (new Random().NextDouble() * 360) - 180
            };

            var mockHotelService = new Mock<IHotelService>();
            mockHotelService
                .Setup(x => x.Update(It.Is<Guid>(id => id == hotelId), It.IsAny<HotelCreateUpdateDto>()))
                .ReturnsAsync(() => new HotelDto
                {
                    Id = hotelId,
                    Name = hotel.Name,
                    Price = hotel.Price,
                    Latitude = hotel.Latitude,
                    Longitude = hotel.Longitude
                });

            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.UpdateHotel(hotelId, hotel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<HotelDto>(okResult.Value);

            var returnedHotel = okResult.Value as HotelDto;

            Assert.Equal(hotelId, returnedHotel?.Id);
            Assert.Equal(hotel.Name, returnedHotel?.Name);
            Assert.Equal(hotel.Price, returnedHotel?.Price);
        }

        [Fact]
        public async Task Delete_Ok()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotel = new HotelDto { Id = hotelId, Name = "Hotel 1", Price = 100 };


            var mockHotelService = new Mock<IHotelService>();
           
            mockHotelService.Setup(x => x.Delete(hotelId)).ReturnsAsync(hotel);

            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.Delete(hotelId);

            // Assert
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<HotelDto>(okResult.Value);

            var returnedHotel = okResult.Value as HotelDto;

            Assert.Equal(hotelId, returnedHotel?.Id);
            Assert.Equal(hotel.Name, returnedHotel?.Name);
            Assert.Equal(hotel.Price, returnedHotel?.Price);
        }

        [Fact]
        public async Task Delete_InvalidGuid_BadRequest()
        {
            // Arrange
            var invalidGuid = Guid.Empty;
            var mockHotelService = new Mock<IHotelService>();
            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.Delete(invalidGuid);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid hotel id.", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_Hotel_NotFound()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var mockHotelService = new Mock<IHotelService>();
            mockHotelService
                .Setup(x => x.Delete(It.Is<Guid>(id => id == hotelId)))
                .Returns(Task.FromResult<HotelDto>(null));

            var controller = new HotelController(mockHotelService.Object);

            // Act
            var result = await controller.Delete(hotelId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal($"Cannot delete Hotel with {hotelId} id.", badRequestResult.Value);
        }
    }
}

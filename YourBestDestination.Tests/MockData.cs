using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Dto;
using YourBestDestination.Domain.Models;
using YourBestDestination.Domain.Models.Paging;

namespace YourBestDestination.Tests
{
    public static class MockData
    {

        public static PaginatedList<HotelDto> Hotels { get; set; }

        static MockData()
        {
            var hotels = new List<HotelDto>();

            for (int i = 0; i < 10; i++)
            {
                var hotel = new HotelDto
                {
                    Id = Guid.NewGuid(),
                    Name = $"Test Hotel Name - {i}",
                    Price = new Random().Next(50, 220),
                    Latitude = (new Random().NextDouble() * 180) - 90,
                    Longitude = (new Random().NextDouble() * 360) - 180,
                };

                hotels.Add(hotel);
            }

            hotels.Sort(delegate (HotelDto firstHotel, HotelDto secondHotel)
            {
                return firstHotel.Id.CompareTo(secondHotel.Id);
            });

            Hotels = new PaginatedList<HotelDto>(hotels, hotels.Count, 1, 10);
        }
    }
}

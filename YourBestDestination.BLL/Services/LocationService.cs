using YourBestDestination.Domain.Abstractions;
using YourBestDestination.Domain.Models.Paging;
using YourBestDestination.Domain.Dto;
using Microsoft.EntityFrameworkCore;

namespace YourBestDestination.BLL.Services
{
    public class LocationService : ILocationService
    {
        private readonly IHotelRepository _hotelRepository;

        public LocationService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<PaginatedList<HotelDto>> GetClosestLocations(double userLatitude, double userLongitude, int page = 1, int size = 10)
        {

            if (userLatitude < -90 || userLatitude > 90)
            {
                throw new ArgumentOutOfRangeException(
                    "userLatitude",
                    "Latitude must be a value between -90 and 90."
                );
            }

            if (userLongitude < -180 || userLongitude > 180)
            {
                throw new ArgumentOutOfRangeException(
                    "userLongitude",
                    "Longitude must be a value between -180 and 180."
                );
            }

            var hotels = await _hotelRepository.FindAll().ToListAsync();

            if (hotels == null)
            {
                throw new ArgumentException("An error occurred while getting hotels. Please check if hotels is null.");
            }

            var sortedHotels = hotels
                .OrderBy(d => Distance(userLatitude, userLongitude, d.Latitude, d.Longitude))
                .OrderBy(p => p.Price);

            var hotelDtos = sortedHotels.Select(x => x.ToHotelDto()).ToList();

            return new PaginatedList<HotelDto>(hotelDtos, sortedHotels.Count(), page, size);
        }

        public double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) +
                          Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                          Math.Cos(Deg2Rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2Deg(dist);
            dist = dist * 60 * 1.1515;
            return dist;
        }

        private double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}

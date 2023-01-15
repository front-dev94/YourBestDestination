using YourBestDestination.Domain.Dto;
using YourBestDestination.Domain.Models;
using YourBestDestination.Domain.Models.Paging;

namespace YourBestDestination.Infrastructure.Extensions
{
    public static class PaginatedListMappingExtensions
    {
        public static PaginatedList<HotelDto> ToHotelDtos(this PaginatedList<Hotel> hotels)
        {
            var hotelDtos = hotels.Items.Select(x => x.ToHotelDto()).ToList();

            return new PaginatedList<HotelDto>(hotelDtos, hotels.Total, hotels.Page, hotels.Size);
        }
    }
}

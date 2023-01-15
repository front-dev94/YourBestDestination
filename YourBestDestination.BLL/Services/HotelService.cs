using YourBestDestination.Domain.Abstractions;
using YourBestDestination.Domain.Models.Paging;
using YourBestDestination.Domain.Dto;
using YourBestDestination.Infrastructure.Extensions;

namespace YourBestDestination.BLL.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository, ILocationService locationService)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<PaginatedList<HotelDto>> GetHotelsAsync(int page = 0, int pageSize = 10)
        {
            if (page < 0 || pageSize < 0)
            {
                throw new ArgumentException("Values for page and size cannot be lower than zero.");
            }

            var hotels = await _hotelRepository.GetHotelsAsync(page, pageSize);

            if (hotels == null)
            {
                throw new ArgumentException("An error occurred while getting hotels. Please check if hotels is null.");
            }

            return PaginatedListMappingExtensions.ToHotelDtos(hotels);
        }

        public async Task<HotelDto> GetHotelByIdAsync(Guid id)
        {

            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid hotel id.");
            }

            var hotel = await _hotelRepository.GetHotelByIdAsync(id);

            if (hotel == null)
            {
                throw new ArgumentException("Hotel not found.");
            }

            return hotel.ToHotelDto();
        }

        public async Task<HotelDto> CreateHotelAsync(HotelCreateUpdateDto hotel)
        {

            if(hotel == null)
            {
                throw new ArgumentNullException("Hotel cannot be null.");
            }

            var hotelEntity = hotel.ToHotel();

            if (hotelEntity == null)
            {
                throw new ArgumentNullException("Hotel not found.");
            }

            var createdHotel = await _hotelRepository.Create(hotelEntity);
            await _hotelRepository.SaveChangesAsync();

            return createdHotel.ToHotelDto();
        }


        public async Task<HotelDto> Update(Guid id, HotelCreateUpdateDto hotelUpdate)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid hotel id.");
            }

            if (hotelUpdate == null)
            {
                throw new ArgumentNullException("Hotel cannot be null.");
            }

            var hotelEntity = await _hotelRepository.GetHotelByIdAsync(id);

            if (hotelEntity == null)
            {
                throw new ArgumentNullException("Hotel not found.");
            }

            var mapped = hotelUpdate.ToHotel();
            mapped.Id = id;

             var updatedHotel = await _hotelRepository.Update(mapped);

            return updatedHotel.ToHotelDto();
        }

        public async Task<HotelDto> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid hotel id.");
            }

            var hotel = await _hotelRepository.GetHotelByIdAsync(id);

            if (hotel == null)
            {
                throw new ArgumentNullException("Hotel not found.");
            }

            var deletedHotel = await _hotelRepository.Delete(hotel);

            return deletedHotel.ToHotelDto();
        }
    }
}

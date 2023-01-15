using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Dto;
using YourBestDestination.Domain.Models;
using YourBestDestination.Domain.Models.Paging;

namespace YourBestDestination.Domain.Abstractions
{
    public interface IHotelService
    {
        Task<PaginatedList<HotelDto>> GetHotelsAsync(int page, int pageSize);

        Task<HotelDto> GetHotelByIdAsync(Guid id);
        Task<HotelDto> CreateHotelAsync(HotelCreateUpdateDto hotel);
        Task<HotelDto> Update(Guid id, HotelCreateUpdateDto hotel);
        Task<HotelDto> Delete(Guid id);
    }
}

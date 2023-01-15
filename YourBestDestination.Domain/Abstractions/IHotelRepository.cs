using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Models;
using YourBestDestination.Domain.Models.Paging;

namespace YourBestDestination.Domain.Abstractions
{
    public interface IHotelRepository : IBaseRepository<Hotel>
    {
        Task<PaginatedList<Hotel>> GetHotelsAsync(int page, int pageSize);
        Task<Hotel> GetHotelByIdAsync(Guid id);
    }
}

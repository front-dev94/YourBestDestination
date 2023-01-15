using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Dto;
using YourBestDestination.Domain.Models.Paging;

namespace YourBestDestination.Domain.Abstractions
{
    public interface ILocationService
    {
        Task<PaginatedList<HotelDto>> GetClosestLocations(double userLatitude, double userLongitude, int page, int size);
    }
}

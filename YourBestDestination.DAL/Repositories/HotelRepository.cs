using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Abstractions;
using YourBestDestination.Domain.Models;
using Microsoft.EntityFrameworkCore;
using YourBestDestination.Domain.Models.Paging;

namespace YourBestDestination.DAL.Repositories
{
    public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<PaginatedList<Hotel>> GetHotelsAsync(int page, int pageSize)
        {
            var hotels = await _context.Hotels
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await _context.Hotels.CountAsync();

            return new PaginatedList<Hotel>(hotels, totalCount, page, pageSize);
        }

        public async Task<Hotel> GetHotelByIdAsync(Guid id)
        {
            var hotel = await FindByCondition(h => h.Id == id).FirstOrDefaultAsync();

            if (hotel == null)
            {
                throw new Exception("Hotel does not exist.");
            }

            return hotel;
        }
    }
}

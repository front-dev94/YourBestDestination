using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Dto;

namespace YourBestDestination.Domain.Models.Paging
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }

        public PaginatedList(List<T> items, int totalCount, int page, int size)
        {
            Items = items;
            Total = totalCount;
            Page = page;
            Size = size;
        }
    }
}

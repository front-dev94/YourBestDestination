using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Models;

namespace YourBestDestination.Domain.Dto
{
    public class HotelDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

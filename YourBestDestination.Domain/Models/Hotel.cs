using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourBestDestination.Domain.Dto;

namespace YourBestDestination.Domain.Models
{
    public class Hotel
    {
        [Column("HotelId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }

        public HotelDto ToHotelDto()
        {
            return new HotelDto
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Latitude = Latitude,
                Longitude = Longitude
            };
        }
    }
}

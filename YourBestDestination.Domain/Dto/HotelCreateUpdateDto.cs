using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YourBestDestination.Domain.Models;

namespace YourBestDestination.Domain.Dto
{
    public class HotelCreateUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can not be longer than 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }

        public Hotel ToHotel()
        {
            return new Hotel
            {
                Name = Name,
                Price = Price,
                Latitude = Latitude,
                Longitude = Longitude,
            };
        }
    }
}

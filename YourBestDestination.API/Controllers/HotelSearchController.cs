using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YourBestDestination.Domain.Abstractions;

namespace YourBestDestination.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class HotelSearchController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public HotelSearchController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("hotels/closest", Name = "GetClosestLocations")]
        public async Task<IActionResult> GetClosestLocations([FromQuery] double latitude, double longitude, int? page = null, int? size = null)
        {
            if (latitude == 0 || longitude == 0)
                return BadRequest("Latitude and longitude parameters cannot be zero");

            if (double.IsNaN(latitude) || double.IsNaN(longitude))
                return BadRequest("Latitude and longitude parameters cannot be empty");

            var hotels = await _locationService.GetClosestLocations(latitude, longitude, page ?? 1, size ?? 10);

            if (hotels == null)
            {
                return NotFound();
            }

            return Ok(hotels);
        }
    }
}

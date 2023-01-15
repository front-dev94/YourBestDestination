using Microsoft.AspNetCore.Mvc;
using YourBestDestination.Domain.Abstractions;
using YourBestDestination.Domain.Dto;

namespace YourBestDestination.API.Controllers
{
    [Route("api/v1/management")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("hotels", Name = "GetHotels")]
        public async Task<IActionResult> GetHotels([FromQuery] int? page = null, int? size = null)
        {

            var hotels = await _hotelService.GetHotelsAsync(page ?? 1, size ?? 10);

            return Ok(hotels);
        }

        [HttpGet("hotel/details", Name = "HotelDetails")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid hotel id.");
            }

            var hotelResult = await _hotelService.GetHotelByIdAsync(id);

            if (hotelResult == null) return NotFound($"Cannot find Hotel with {id} id.");

            return Ok(hotelResult);
        }

        [HttpPost("hotel/create", Name = "CreateHotel")]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateUpdateDto hotel)
        {

            if (hotel == null)
            {
                return BadRequest("Hotel object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var createdHotel = await _hotelService.CreateHotelAsync(hotel);

            return CreatedAtRoute("HotelDetails", new { id = createdHotel.Id }, createdHotel);
        }

        [HttpPut("hotel/edit", Name = "UpdateHotelDetails")]
        public async Task<IActionResult> UpdateHotel([FromQuery] Guid id, [FromBody] HotelCreateUpdateDto hotel)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid hotel id.");
            }

            if (hotel == null)
            {
                return BadRequest("Hotel object is null");
            }

            var updatedHotel = await _hotelService.Update(id, hotel);

            if (updatedHotel == null) return BadRequest("Hotel is not updated.");

            return Ok(updatedHotel);
        }

        [HttpDelete("hotel/delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid hotel id.");
            }

            var deletedHotel = await _hotelService.Delete(id);

            if (deletedHotel == null) return BadRequest($"Cannot delete Hotel with {id} id.");

            return Ok(deletedHotel);
        }
    }
}

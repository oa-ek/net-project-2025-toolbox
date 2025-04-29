using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IBaseService<LocationDto> _locationService;

        public LocationController(IBaseService<LocationDto> locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
        {
            var locations = await _locationService.GetAllAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetById(int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDto>> Add([FromBody] LocationDto locationDto)
        {
            var newLocation = await _locationService.AddAsync(locationDto);
            return CreatedAtAction(nameof(GetById), new { id = newLocation.Id }, newLocation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LocationDto locationDto)
        {
            var updatedLocation = await _locationService.UpdateAsync(id, locationDto);
            if (updatedLocation == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _locationService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}


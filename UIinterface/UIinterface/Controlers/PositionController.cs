using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IBaseService<PositionDto> _positionService;

        public PositionController(IBaseService<PositionDto> positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDto>>> GetAll()
        {
            var positions = await _positionService.GetAllAsync();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDto>> GetById(int id)
        {
            var position = await _positionService.GetByIdAsync(id);
            if (position == null) return NotFound();
            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<PositionDto>> Add([FromBody] PositionDto positionDto)
        {
            var newPosition = await _positionService.AddAsync(positionDto);
            return CreatedAtAction(nameof(GetById), new { id = newPosition.Id }, newPosition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PositionDto positionDto)
        {
            var updatedPosition = await _positionService.UpdateAsync(id, positionDto);
            if (updatedPosition == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _positionService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}


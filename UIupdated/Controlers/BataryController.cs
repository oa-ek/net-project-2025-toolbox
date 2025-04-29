using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BataryController : ControllerBase
    {
        private readonly IBaseService<BataryDto> _bataryService;

        public BataryController(IBaseService<BataryDto> bataryService)
        {
            _bataryService = bataryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BataryDto>>> GetAll()
        {
            var bataries = await _bataryService.GetAllAsync();
            return Ok(bataries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BataryDto>> GetById(int id)
        {
            var batary = await _bataryService.GetByIdAsync(id);
            if (batary == null) return NotFound();
            return Ok(batary);
        }

        [HttpPost]
        public async Task<ActionResult<BataryDto>> Add([FromBody] BataryDto bataryDto)
        {
            var newBatary = await _bataryService.AddAsync(bataryDto);
            return CreatedAtAction(nameof(GetById), new { id = newBatary.Id }, newBatary);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BataryDto bataryDto)
        {
            var updatedBatary = await _bataryService.UpdateAsync(id, bataryDto);
            if (updatedBatary == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bataryService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

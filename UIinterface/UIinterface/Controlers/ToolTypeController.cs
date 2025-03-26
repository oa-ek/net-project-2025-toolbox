using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolTypeController : ControllerBase
    {
        private readonly IBaseService<ToolTypeDto> _toolTypeService;

        public ToolTypeController(IBaseService<ToolTypeDto> toolTypeService)
        {
            _toolTypeService = toolTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolTypeDto>>> GetAll()
        {
            var toolTypes = await _toolTypeService.GetAllAsync();
            return Ok(toolTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToolTypeDto>> GetById(int id)
        {
            var toolType = await _toolTypeService.GetByIdAsync(id);
            if (toolType == null) return NotFound();
            return Ok(toolType);
        }

        [HttpPost]
        public async Task<ActionResult<ToolTypeDto>> Add([FromBody] ToolTypeDto toolTypeDto)
        {
            var newToolType = await _toolTypeService.AddAsync(toolTypeDto);
            return CreatedAtAction(nameof(GetById), new { id = newToolType.Id }, newToolType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ToolTypeDto toolTypeDto)
        {
            var updatedToolType = await _toolTypeService.UpdateAsync(id, toolTypeDto);
            if (updatedToolType == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _toolTypeService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

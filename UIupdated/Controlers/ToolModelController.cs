using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolModelController : ControllerBase
    {
        private readonly IBaseService<ToolModelDto> _toolModelService;

        public ToolModelController(IBaseService<ToolModelDto> toolModelService)
        {
            _toolModelService = toolModelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolModelDto>>> GetAll()
        {
            var toolModels = await _toolModelService.GetAllAsync();
            return Ok(toolModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToolModelDto>> GetById(int id)
        {
            var toolModel = await _toolModelService.GetByIdAsync(id);
            if (toolModel == null) return NotFound();
            return Ok(toolModel);
        }

        [HttpPost]
        public async Task<ActionResult<ToolModelDto>> Add([FromBody] ToolModelDto toolModelDto)
        {
            var newToolModel = await _toolModelService.AddAsync(toolModelDto);
            return CreatedAtAction(nameof(GetById), new { id = newToolModel.Id }, newToolModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ToolModelDto toolModelDto)
        {
            var updatedToolModel = await _toolModelService.UpdateAsync(id, toolModelDto);
            if (updatedToolModel == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _toolModelService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandToolController : ControllerBase
    {
        private readonly IBaseService<HandToolDto> _handToolService;

        public HandToolController(IBaseService<HandToolDto> handToolService)
        {
            _handToolService = handToolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HandToolDto>>> GetAll()
        {
            var handTools = await _handToolService.GetAllAsync();
            return Ok(handTools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HandToolDto>> GetById(int id)
        {
            var handTool = await _handToolService.GetByIdAsync(id);
            if (handTool == null) return NotFound();
            return Ok(handTool);
        }

        [HttpPost]
        public async Task<ActionResult<HandToolDto>> Add([FromBody] HandToolDto handToolDto)
        {
            var newHandTool = await _handToolService.AddAsync(handToolDto);
            return CreatedAtAction(nameof(GetById), new { id = newHandTool.Id }, newHandTool);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HandToolDto handToolDto)
        {
            var updatedHandTool = await _handToolService.UpdateAsync(id, handToolDto);
            if (updatedHandTool == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _handToolService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

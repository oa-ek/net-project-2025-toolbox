using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerToolController : ControllerBase
    {
        private readonly IBaseService<PowerToolDto> _powerToolService;

        public PowerToolController(IBaseService<PowerToolDto> powerToolService)
        {
            _powerToolService = powerToolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PowerToolDto>>> GetAll()
        {
            var powerTools = await _powerToolService.GetAllAsync();
            return Ok(powerTools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PowerToolDto>> GetById(int id)
        {
            var powerTool = await _powerToolService.GetByIdAsync(id);
            if (powerTool == null) return NotFound();
            return Ok(powerTool);
        }

        [HttpPost]
        public async Task<ActionResult<PowerToolDto>> Add([FromBody] PowerToolDto powerToolDto)
        {
            var newPowerTool = await _powerToolService.AddAsync(powerToolDto);
            return CreatedAtAction(nameof(GetById), new { id = newPowerTool.Id }, newPowerTool);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PowerToolDto powerToolDto)
        {
            var updatedPowerTool = await _powerToolService.UpdateAsync(id, powerToolDto);
            if (updatedPowerTool == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _powerToolService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

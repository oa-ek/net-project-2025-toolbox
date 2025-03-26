using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerSupplyTypeController : ControllerBase
    {
        private readonly IBaseService<PowerSupplyTypeDto> _powerSupplyTypeService;

        public PowerSupplyTypeController(IBaseService<PowerSupplyTypeDto> powerSupplyTypeService)
        {
            _powerSupplyTypeService = powerSupplyTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PowerSupplyTypeDto>>> GetAll()
        {
            var powerSupplyTypes = await _powerSupplyTypeService.GetAllAsync();
            return Ok(powerSupplyTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PowerSupplyTypeDto>> GetById(int id)
        {
            var powerSupplyType = await _powerSupplyTypeService.GetByIdAsync(id);
            if (powerSupplyType == null) return NotFound();
            return Ok(powerSupplyType);
        }

        [HttpPost]
        public async Task<ActionResult<PowerSupplyTypeDto>> Add([FromBody] PowerSupplyTypeDto powerSupplyTypeDto)
        {
            var newPowerSupplyType = await _powerSupplyTypeService.AddAsync(powerSupplyTypeDto);
            return CreatedAtAction(nameof(GetById), new { id = newPowerSupplyType.Id }, newPowerSupplyType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PowerSupplyTypeDto powerSupplyTypeDto)
        {
            var updatedPowerSupplyType = await _powerSupplyTypeService.UpdateAsync(id, powerSupplyTypeDto);
            if (updatedPowerSupplyType == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _powerSupplyTypeService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : ControllerBase
    {
        private readonly IBaseService<ConditionDto> _conditionService;

        public ConditionController(IBaseService<ConditionDto> conditionService)
        {
            _conditionService = conditionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConditionDto>>> GetAll()
        {
            var conditions = await _conditionService.GetAllAsync();
            return Ok(conditions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConditionDto>> GetById(int id)
        {
            var condition = await _conditionService.GetByIdAsync(id);
            if (condition == null) return NotFound();
            return Ok(condition);
        }

        [HttpPost]
        public async Task<ActionResult<ConditionDto>> Add([FromBody] ConditionDto conditionDto)
        {
            var newCondition = await _conditionService.AddAsync(conditionDto);
            return CreatedAtAction(nameof(GetById), new { id = newCondition.Id }, newCondition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConditionDto conditionDto)
        {
            var updatedCondition = await _conditionService.UpdateAsync(id, conditionDto);
            if (updatedCondition == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _conditionService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

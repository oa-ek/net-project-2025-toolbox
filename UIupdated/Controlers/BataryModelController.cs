using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BataryModelController : ControllerBase
    {
        private readonly IBaseService<BataryModelDto> _bataryModelService;

        public BataryModelController(IBaseService<BataryModelDto> bataryModelService)
        {
            _bataryModelService = bataryModelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BataryModelDto>>> GetAll()
        {
            var bataryModels = await _bataryModelService.GetAllAsync();
            return Ok(bataryModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BataryModelDto>> GetById(int id)
        {
            var bataryModel = await _bataryModelService.GetByIdAsync(id);
            if (bataryModel == null) return NotFound();
            return Ok(bataryModel);
        }

        [HttpPost]
        public async Task<ActionResult<BataryModelDto>> Add([FromBody] BataryModelDto bataryModelDto)
        {
            var newBataryModel = await _bataryModelService.AddAsync(bataryModelDto);
            return CreatedAtAction(nameof(GetById), new { id = newBataryModel.Id }, newBataryModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BataryModelDto bataryModelDto)
        {
            var updatedBataryModel = await _bataryModelService.UpdateAsync(id, bataryModelDto);
            if (updatedBataryModel == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bataryModelService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

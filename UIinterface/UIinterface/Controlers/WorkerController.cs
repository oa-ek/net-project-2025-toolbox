using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IBaseService<WorkerDto> _workerService;

        public WorkerController(IBaseService<WorkerDto> workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetAll()
        {
            var workers = await _workerService.GetAllAsync();
            return Ok(workers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDto>> GetById(int id)
        {
            var worker = await _workerService.GetByIdAsync(id);
            if (worker == null) return NotFound();
            return Ok(worker);
        }

        [HttpPost]
        public async Task<ActionResult<WorkerDto>> Add([FromBody] WorkerDto workerDto)
        {
            var newWorker = await _workerService.AddAsync(workerDto);
            return CreatedAtAction(nameof(GetById), new { id = newWorker.Id }, newWorker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkerDto workerDto)
        {
            var updatedWorker = await _workerService.UpdateAsync(id, workerDto);
            if (updatedWorker == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _workerService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}


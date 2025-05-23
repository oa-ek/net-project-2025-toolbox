using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UIupdated.Services;
using UIinterface.Services;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IBaseService<WorkerDto> _workerService;
        private readonly IUserService _userService;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(
            IBaseService<WorkerDto> workerService,
            IUserService userService,
            ILogger<WorkerController> logger)
        {
            _workerService = workerService;
            _userService = userService;
            _logger = logger;
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
            _logger.LogInformation("Adding worker for email {Email}", workerDto.Email);

            // 1. Знаходимо користувача по email
            var user = await _userService.GetUserByEmailAsync(workerDto.Email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found", workerDto.Email);
                return BadRequest($"User with email '{workerDto.Email}' does not exist.");
            }

            // 2. Призначаємо роль Worker через userId (через UserService)
            var roleAssigned = await _userService.AssignRoleAsync(user.Id, "Worker");
            if (!roleAssigned)
            {
                _logger.LogError("Failed to assign 'Worker' role to user {UserId}", user.Id);
                return StatusCode(500, "Failed to assign 'Worker' role to user.");
            }

            // 3. Додаємо воркера
            var newWorker = await _workerService.AddAsync(workerDto);
            _logger.LogInformation("Worker added with id {WorkerId} for user {UserId}", newWorker.Id, user.Id);
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

        [HttpPost("assign-test")]
        public async Task<IActionResult> TestAssign([FromBody] string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return NotFound("User not found");
            var result = await _userService.AssignRoleAsync(user.Id, "Worker");
            return Ok(result);
        }
    }
}

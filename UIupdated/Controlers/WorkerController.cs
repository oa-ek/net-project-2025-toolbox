using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UIupdated.Services;
using UIinterface.Services;
using Microsoft.EntityFrameworkCore;
using UIupdated.Data;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IBaseService<WorkerDto> _workerService;
        private readonly IUserService _userService;
        private readonly ILogger<WorkerController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly PowerToolService _powerToolService;
        private readonly HandToolService _handToolService;
        private readonly BataryService _bataryService;

        public WorkerController(
            IBaseService<WorkerDto> workerService,
            IUserService userService,
            ILogger<WorkerController> logger,
            PowerToolService powerToolService,
            HandToolService handToolService,
            BataryService bataryService)
        {
            _workerService = workerService;
            _userService = userService;
            _logger = logger;
            _powerToolService = powerToolService;
            _handToolService = handToolService;
            _bataryService = bataryService;
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
            var worker = await _workerService.GetByIdAsync(id);
            if (worker == null) return NotFound();

            // Отримати всі інструменти через сервіси
            var bataryList = (await _bataryService.GetAllAsync())
                .Where(b => b.LastWorkerId == id)
                .ToList();
            var powerToolList = (await _powerToolService.GetAllAsync())
                .Where(p => p.LastWorkerId == id)
                .ToList();
            var handToolList = (await _handToolService.GetAllAsync())
                .Where(h => h.LastWorkerId == id)
                .ToList();

            // Формуємо лог
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Worker: {worker.FirstName} {worker.LastName}, Phone: {worker.Phone}, Email: {worker.Email}");
            sb.AppendLine("Batary:");
            foreach (var b in bataryList)
                sb.AppendLine($"  Id: {b.Id}, Number: {b.Number}, Serial: {b.SerialNumber}");
            sb.AppendLine("PowerTool:");
            foreach (var p in powerToolList)
                sb.AppendLine($"  Id: {p.Id}, Number: {p.Number}, Serial: {p.SerialNumber}");
            sb.AppendLine("HandTool:");
            foreach (var h in handToolList)
                sb.AppendLine($"  Id: {h.Id}");

            // Відв'язуємо інструменти через сервіси
            foreach (var b in bataryList)
            {
                b.LastWorkerId = null;
                await _bataryService.UpdateAsync(b.Id, b);
            }
            foreach (var p in powerToolList)
            {
                p.LastWorkerId = null;
                await _powerToolService.UpdateAsync(p.Id, p);
            }
            foreach (var h in handToolList)
            {
                h.LastWorkerId = null;
                await _handToolService.UpdateAsync(h.Id, h);
            }

            // Далі - як було
            var user = await _userService.GetUserByEmailAsync(worker.Email);
            if (user != null)
            {
                var workerRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Worker");
                if (workerRole != null)
                {
                    await _userService.RemoveRoleByIdAsync(user.Id, workerRole.Id);
                }
            }

            var deleted = await _workerService.DeleteAsync(id);
            if (!deleted) return NotFound();

            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/plain", $"worker_{worker.Id}_tools_log.txt");
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

using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using UIupdated.Services;
using UIupdated.Data;
using Microsoft.EntityFrameworkCore;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAdminController : ControllerBase
    {
        private readonly IBaseService<SystemAdminDto> _systemAdminService;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _dbContext;

        public SystemAdminController(
    IBaseService<SystemAdminDto> systemAdminService,
    IUserService userService,
    ApplicationDbContext dbContext)
        {
            _systemAdminService = systemAdminService;
            _userService = userService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemAdminDto>>> GetAll()
        {
            var systemAdmins = await _systemAdminService.GetAllAsync();
            return Ok(systemAdmins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SystemAdminDto>> GetById(int id)
        {
            var systemAdmin = await _systemAdminService.GetByIdAsync(id);
            if (systemAdmin == null) return NotFound();
            return Ok(systemAdmin);
        }

        [HttpPost]
        public async Task<ActionResult<SystemAdminDto>> Add([FromBody] SystemAdminDto systemAdminDto)
        {
            var user = await _userService.GetUserByEmailAsync(systemAdminDto.Email);
            if (user == null)
            {
                return BadRequest($"User with email '{systemAdminDto.Email}' does not exist.");
            }

            var roleAssigned = await _userService.AssignRoleAdminAsync(user.Id);
            if (!roleAssigned)
            {
                return StatusCode(500, "Failed to assign 'Admin' role to user.");
            }

            var newSystemAdmin = await _systemAdminService.AddAsync(systemAdminDto);
            return CreatedAtAction(nameof(GetById), new { id = newSystemAdmin.Id }, newSystemAdmin);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SystemAdminDto systemAdminDto)
        {
            var updatedSystemAdmin = await _systemAdminService.UpdateAsync(id, systemAdminDto);
            if (updatedSystemAdmin == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var systemAdmin = await _systemAdminService.GetByIdAsync(id);
            if (systemAdmin == null) return NotFound();

            var user = await _userService.GetUserByEmailAsync(systemAdmin.Email);
            if (user != null)
            {
                // Отримати Id ролі Admin
                var adminRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
                if (adminRole != null)
                {
                    await _userService.RemoveRoleByIdAsync(user.Id, adminRole.Id);
                }
            }

            var deleted = await _systemAdminService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }


    }
}


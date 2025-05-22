using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAdminController : ControllerBase
    {
        private readonly IBaseService<SystemAdminDto> _systemAdminService;

        public SystemAdminController(IBaseService<SystemAdminDto> systemAdminService)
        {
            _systemAdminService = systemAdminService;
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
            var deleted = await _systemAdminService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}


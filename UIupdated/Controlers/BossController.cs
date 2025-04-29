using UIinterface.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UIinterface.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BossController : ControllerBase
    {
        private readonly IBaseService<BossDto> _bossService;

        public BossController(IBaseService<BossDto> bossService)
        {
            _bossService = bossService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BossDto>>> GetAll()
        {
            var bosses = await _bossService.GetAllAsync();
            return Ok(bosses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BossDto>> GetById(int id)
        {
            var boss = await _bossService.GetByIdAsync(id);
            if (boss == null) return NotFound();
            return Ok(boss);
        }

        [HttpPost]
        public async Task<ActionResult<BossDto>> Add([FromBody] BossDto bossDto)
        {
            var newBoss = await _bossService.AddAsync(bossDto);
            return CreatedAtAction(nameof(GetById), new { id = newBoss.Id }, newBoss);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BossDto bossDto)
        {
            var updatedBoss = await _bossService.UpdateAsync(id, bossDto);
            if (updatedBoss == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bossService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

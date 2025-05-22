using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class BossRepository : BaseRepository<Boss>
    {
        public BossRepository(TTContext context) : base(context) { }

        public async Task AddBossWithLocationsAsync(BossDto bossDto)
        {
            var locations = new List<Location>();

            foreach (var locDto in bossDto.Locations)
            {
                var loc = await _context.Locations.AsNoTracking().FirstOrDefaultAsync(l => l.Id == locDto.Id);
                if (loc != null)
                {
                    // Перевіряємо, чи вже відстежується ця локація
                    var trackedLocation = _context.ChangeTracker.Entries<Location>().FirstOrDefault(e => e.Entity.Id == loc.Id);
                    if (trackedLocation == null)
                    {
                        _context.Attach(loc);  // Повідомляємо контексту, що локація вже існує
                    }
                    else
                    {
                        loc = trackedLocation.Entity;  // Використовуємо вже відстежувану локацію
                    }
                    locations.Add(loc);  // Додаємо до тимчасового списку
                }
            }

            var boss = new Boss
            {
                FirstName = bossDto.FirstName,
                LastName = bossDto.LastName,
                Email = bossDto.Email,
                Phone = bossDto.Phone,
                Password = bossDto.Password,
                Locations = locations // тут уже призначаєш готовий список
            };

            await _context.Bosses.AddAsync(boss);
            await _context.SaveChangesAsync();
        }
    }
}




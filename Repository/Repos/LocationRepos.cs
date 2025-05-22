using Core;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class LocationRepository : BaseRepository<Location>
    {
        public LocationRepository(TTContext context) : base(context) { }

        //public TTContext Context => _context;

        public async Task<Location> GetLocationWithBossesAsync(int id)
        {
            return await _context.Locations
                .Include(l => l.Bosses)
                .Include(l => l.Workers)
                .AsSplitQuery() // Додаємо розділення запитів
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}

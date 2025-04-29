using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Repos;

namespace UIinterface.Services
{
    public class LocationService : BaseService<Location, LocationDto>, IBaseService<LocationDto>
    {
        private readonly LocationRepository _locationRepository;
        private readonly BossRepository _bossRepository;

        public LocationService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.LocationRepository, mapper)
        {
            _locationRepository = repositoryContainer.LocationRepository;
            _bossRepository = repositoryContainer.BossRepository;
        }

        // Метод для призначення боса до локації
        /*  public async Task AssignBossToLocation(int locationId, int bossId)
          {
              var location = await _locationRepository.Context.Locations
                  .Include(l => l.Bosses)
                  .FirstOrDefaultAsync(l => l.Id == locationId);
              var boss = await _bossRepository.Context.Bosses
                  .Include(b => b.Locations)
                  .FirstOrDefaultAsync(b => b.Id == bossId);

              if (location == null || boss == null)
                  throw new Exception("Location or Boss not found");

              // Перевіряємо, чи вже є цей бос в локації
              if (!location.Bosses.Any(b => b.Id == boss.Id))
              {
                  // Перевіряємо, чи вже відстежується ця локація
                  var trackedLocation = _locationRepository.Context.ChangeTracker.Entries<Location>().FirstOrDefault(e => e.Entity.Id == location.Id);
                  if (trackedLocation == null)
                  {
                      _locationRepository.Context.Attach(location);  // Повідомляємо контексту, що локація вже існує
                  }
                  else
                  {
                      location = trackedLocation.Entity;  // Використовуємо вже відстежувану локацію
                  }

                  // Перевіряємо, чи вже відстежується цей бос
                  var trackedBoss = _locationRepository.Context.ChangeTracker.Entries<Boss>().FirstOrDefault(e => e.Entity.Id == boss.Id);
                  if (trackedBoss == null)
                  {
                      _locationRepository.Context.Attach(boss);  // Повідомляємо контексту, що бос вже існує
                  }
                  else
                  {
                      boss = trackedBoss.Entity;  // Використовуємо вже відстежуваного боса
                  }

                  // Додаємо босів до локації
                  location.Bosses.Add(boss);
                  await _locationRepository.SaveChangesAsync();
              }
          }*/
        public async Task AssignBossToLocation(int locationId, int bossId)
        {
            try
            {
                var location = await _locationRepository.Context.Locations
                    .Include(l => l.Bosses)
                    .FirstOrDefaultAsync(l => l.Id == locationId);
                var boss = await _bossRepository.Context.Bosses
                    .Include(b => b.Locations)
                    .FirstOrDefaultAsync(b => b.Id == bossId);

                if (location == null || boss == null)
                    throw new Exception("Location or Boss not found");

                if (!location.Bosses.Any(b => b.Id == boss.Id))
                {
                    location.Bosses.Add(boss);
                    await _locationRepository.SaveChangesAsync();
                    Console.WriteLine($"Boss {bossId} assigned to Location {locationId}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error assigning Boss to Location: {ex.Message}");
            }
        }



        // Метод для отримання локації з босами та працівниками
        public override async Task<LocationDto> GetByIdAsync(int id)
        {
            var location = await _locationRepository.Context.Locations
                .Include(l => l.Bosses)
                .Include(l => l.Workers)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
                throw new Exception("Location not found");

            return _mapper.Map<LocationDto>(location);
        }

    }
}






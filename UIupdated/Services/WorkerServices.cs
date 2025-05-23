using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class WorkerService : BaseService<Worker, WorkerDto>, IBaseService<WorkerDto>
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public async Task<List<string>> GetUsedEmailsAsync()
        {
            var all = await GetAllAsync();
            return all
                .Select(w => w.Email)
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .ToList();
        }


        public WorkerService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<WorkerService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.WorkerRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<WorkerDto> UpdateAsync(int id, WorkerDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.Workers.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<Worker>(dto);
                    context.Workers.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<WorkerDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the worker.");
                throw;
            }
        }

        public override async Task<WorkerDto> AddAsync(WorkerDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();

                    // Логування вхідних даних
                    _logger.LogInformation($"Adding worker: {dto.FirstName} {dto.LastName}, LocationId: {dto.LocationId}, BossId: {dto.BossId}");

                    // Перевірка, чи існує LocationId
                    if (!await context.Locations.AnyAsync(l => l.Id == dto.LocationId))
                    {
                        throw new ArgumentException($"Location with Id {dto.LocationId} does not exist.");
                    }

                    // Перевірка, чи існує BossId
                    if (!await context.Bosses.AnyAsync(b => b.Id == dto.BossId))
                    {
                        throw new ArgumentException($"Boss with Id {dto.BossId} does not exist.");
                    }

                    // Мапінг DTO на сутність Worker
                    var entity = _mapper.Map<Worker>(dto);

                    // Уникаємо створення нового Location
                    entity.Location = null;

                    context.Workers.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<WorkerDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the worker.");
                throw;
            }
        }


        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = await context.Workers.FindAsync(id);
                    if (entity != null)
                    {
                        context.Workers.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the worker.");
                throw;
            }
        }

        public override async Task<IEnumerable<WorkerDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var workers = await context.Workers
                    .Include(w => w.Position)
                    .ToListAsync();

                var workerDtos = _mapper.Map<IEnumerable<WorkerDto>>(workers);

                foreach (var workerDto in workerDtos)
                {
                    var position = workers.FirstOrDefault(w => w.Id == workerDto.Id)?.Position;
                    if (position != null)
                    {
                        workerDto.PositionName = position.Name;
                    }
                }

                return workerDtos;
            }
        }

        public override async Task<WorkerDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var worker = await context.Workers
                    .Include(w => w.Position)
                    .FirstOrDefaultAsync(w => w.Id == id);

                var workerDto = _mapper.Map<WorkerDto>(worker);

                if (worker?.Position != null)
                {
                    workerDto.PositionName = worker.Position.Name;
                }

                return workerDto;
            }
        }

        /// Метод для пошуку працівників 
        public async Task<IEnumerable<WorkerDto>> SearchAsync(string searchTerm)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var workers = await context.Workers
                    .Include(w => w.Position)
                    .Where(w => w.FirstName.Contains(searchTerm) || w.LastName.Contains(searchTerm) || w.Email.Contains(searchTerm) || w.Phone.Contains(searchTerm))
                    .ToListAsync();

                var workerDtos = _mapper.Map<IEnumerable<WorkerDto>>(workers);

                foreach (var workerDto in workerDtos)
                {
                    var position = workers.FirstOrDefault(w => w.Id == workerDto.Id)?.Position;
                    if (position != null)
                    {
                        workerDto.PositionName = position.Name;
                    }
                }

                return workerDtos;
            }
        }


        public async Task<WorkerDto?> GetWorkerByEmailAsync(string email)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var worker = await context.Workers
                    .Include(w => w.Location)
                    .Include(w => w.Position)
                    .FirstOrDefaultAsync(w => w.Email == email);

                return _mapper.Map<WorkerDto>(worker);
            }
        }

        public async Task<bool> AssignToolsToWorkerAsync(int workerId, List<int> toolIds, string toolType, int locationId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();

                switch (toolType)
                {
                    case "PowerTool":
                        var powerTools = await context.PowerTools.Where(pt => toolIds.Contains(pt.Id)).ToListAsync();
                        foreach (var tool in powerTools)
                        {
                            tool.LastWorkerId = workerId;
                            tool.LastLocationId = locationId;
                        }
                        break;

                    case "HandTool":
                        var handTools = await context.HandTools.Where(ht => toolIds.Contains(ht.Id)).ToListAsync();
                        foreach (var tool in handTools)
                        {
                            tool.LastWorkerId = workerId;
                            tool.LastLocationId = locationId;
                        }
                        break;

                    case "Batary":
                        var bataries = await context.Bataries.Where(b => toolIds.Contains(b.Id)).ToListAsync();
                        foreach (var tool in bataries)
                        {
                            tool.LastWorkerId = workerId;
                            tool.LastLocationId = locationId;
                        }
                        break;

                    default:
                        throw new ArgumentException("Invalid tool type.");
                }

                await context.SaveChangesAsync();
                return true;
            }
        }

    }
}

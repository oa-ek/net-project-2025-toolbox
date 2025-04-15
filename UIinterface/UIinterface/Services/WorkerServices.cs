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
                    var entity = _mapper.Map<Worker>(dto);

                    // Ensure all required properties are set
                    if (string.IsNullOrEmpty(entity.FirstName))
                    {
                        entity.FirstName = "Default FirstName"; // Set a default value or handle accordingly
                    }
                    if (string.IsNullOrEmpty(entity.LastName))
                    {
                        entity.LastName = "Default LastName"; // Set a default value or handle accordingly
                    }
                    if (string.IsNullOrEmpty(entity.Email))
                    {
                        entity.Email = "default@example.com"; // Set a default value or handle accordingly
                    }
                    if (string.IsNullOrEmpty(entity.Phone))
                    {
                        entity.Phone = "000-000-0000"; // Set a default value or handle accordingly
                    }
                    if (string.IsNullOrEmpty(entity.Password))
                    {
                        entity.Password = "DefaultPassword"; // Set a default value or handle accordingly
                    }

                    // Ensure the Description and Name properties are set for Location
                    if (entity.Location != null)
                    {
                        if (string.IsNullOrEmpty(entity.Location.Description))
                        {
                            entity.Location.Description = "Default Description";
                        }
                        if (string.IsNullOrEmpty(entity.Location.Name))
                        {
                            entity.Location.Name = "Default Name"; // Set a default value or handle accordingly
                        }
                    }

                    // Ensure the Name property is set for Position
                    if (entity.Position != null && string.IsNullOrEmpty(entity.Position.Name))
                    {
                        entity.Position.Name = "Default Position Name"; // Set a default value or handle accordingly
                    }

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
    }
}

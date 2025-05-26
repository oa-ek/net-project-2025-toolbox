using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class HandToolService : BaseService<HandTool, HandToolDto>, IBaseService<HandToolDto>
    {
        private readonly ILogger<HandToolService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HandToolService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<HandToolService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.HandToolRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<HandToolDto> AddAsync(HandToolDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<HandTool>(dto);
                    context.HandTools.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<HandToolDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the hand tool.");
                throw;
            }
        }

        public override async Task<HandToolDto> UpdateAsync(int id, HandToolDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.HandTools.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<HandTool>(dto);
                    context.HandTools.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<HandToolDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the hand tool.");
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
                    var entity = await context.HandTools.FindAsync(id);
                    if (entity != null)
                    {
                        context.HandTools.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the hand tool.");
                throw;
            }
        }

        public override async Task<IEnumerable<HandToolDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var handTools = await context.HandTools.ToListAsync();
                return _mapper.Map<IEnumerable<HandToolDto>>(handTools);
            }
        }

        public override async Task<HandToolDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var handTool = await context.HandTools.FindAsync(id);
                return _mapper.Map<HandToolDto>(handTool);
            }
        }

        public async Task<List<HandToolDto>> GetToolsAsync(string searchTerm, string sortColumn, bool sortAscending)
        {
            var query = _repository.Context.HandTools
                .Include(ht => ht.ToolType)
                .Include(ht => ht.Condition)
                .Include(ht => ht.Brand)
                .AsQueryable();

            // Фільтрація
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(ht =>
                    ht.ToolType.Name.Contains(searchTerm) ||
                    ht.Brand.Name.Contains(searchTerm) ||
                    ht.Condition.Name.Contains(searchTerm));
            }

            // Сортування
            query = sortColumn switch
            {
                "Name" => sortAscending ? query.OrderBy(ht => ht.ToolType.Name) : query.OrderByDescending(ht => ht.ToolType.Name),
                "Brand" => sortAscending ? query.OrderBy(ht => ht.Brand.Name) : query.OrderByDescending(ht => ht.Brand.Name),
                "Condition" => sortAscending ? query.OrderBy(ht => ht.Condition.Name) : query.OrderByDescending(ht => ht.Condition.Name),
                _ => query.OrderBy(ht => ht.Id)
            };

            // Отримання даних
            var tools = await query.ToListAsync();

            // Мапінг у DTO
            return tools.Select(ht => new HandToolDto
            {
                Id = ht.Id,
                Name = ht.ToolType.Name,
                BrandId = ht.Brand.Id,
                ConditionId = ht.Condition.Id,
                ToolTypeId = ht.ToolType.Id,
                Price = ht.Price
            }).ToList();
        }

        public async Task<List<HandToolDto>> GetToolsByLocationAsync(int locationId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var tools = await context.HandTools
                    .Include(ht => ht.ToolType)
                    .Include(ht => ht.Brand)
                    .Include(ht => ht.Condition)
                    .Where(ht => ht.LastLocationId == locationId)
                    .ToListAsync();

                return _mapper.Map<List<HandToolDto>>(tools);
            }
        }



        public async Task<List<HandToolDto>> SearchToolsAsync(string searchTerm)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var query = context.HandTools
                    .Include(ht => ht.ToolType)
                    .Include(ht => ht.Brand)
                    .Include(ht => ht.Condition)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(ht =>
                        ht.ToolType.Name.Contains(searchTerm) ||
                        ht.Brand.Name.Contains(searchTerm) ||
                        ht.Condition.Name.Contains(searchTerm));
                }

                var tools = await query.ToListAsync();
                return _mapper.Map<List<HandToolDto>>(tools);
            }
        }
        public async Task UpdateToolLocationAsync(int toolId, int locationId, int workerId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var tool = await context.HandTools.FindAsync(toolId);
                if (tool != null)
                {
                    tool.LastLocationId = locationId;
                    tool.LastWorkerId = workerId;
                    await context.SaveChangesAsync();
                }
            }
        }

    }
}


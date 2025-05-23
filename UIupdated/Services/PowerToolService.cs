using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class PowerToolService : BaseService<PowerTool, PowerToolDto>, IBaseService<PowerToolDto>
    {
        private readonly ILogger<PowerToolService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PowerToolService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<PowerToolService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.PowerToolRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<PowerToolDto> AddAsync(PowerToolDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<PowerTool>(dto);

                    // Check required fields
                    if (entity.ToolTypeId == 0 || entity.ConditionId == 0 ||
                        entity.ToolModelId == 0 || entity.PowerSupplyTypeId == 0 ||
                        string.IsNullOrEmpty(entity.SerialNumber) || string.IsNullOrEmpty(entity.Number))
                    {
                        throw new InvalidOperationException("All required fields must be filled.");
                    }

                    // Validate ToolTypeId
                    if (!await context.ToolTypes.AnyAsync(tt => tt.Id == entity.ToolTypeId))
                    {
                        throw new ArgumentException("Invalid ToolTypeId", nameof(entity.ToolTypeId));
                    }

                    // Validate ToolModelId
                    if (!await context.ToolModels.AnyAsync(tm => tm.Id == entity.ToolModelId))
                    {
                        throw new ArgumentException("Invalid ToolModelId", nameof(entity.ToolModelId));
                    }

                    // Validate PowerSupplyTypeId
                    if (!await context.PowerSupplyTypes.AnyAsync(pst => pst.Id == entity.PowerSupplyTypeId))
                    {
                        throw new ArgumentException("Invalid PowerSupplyTypeId", nameof(entity.PowerSupplyTypeId));
                    }

                    // Ensure Condition is loaded and has a Name
                    var condition = await context.Conditions.FindAsync(entity.ConditionId);
                    if (condition == null || string.IsNullOrEmpty(condition.Name))
                    {
                        throw new ArgumentException("ConditionName cannot be null or empty", nameof(condition.Name));
                    }

                    // Validate SerialNumber
                    if (string.IsNullOrEmpty(entity.SerialNumber))
                    {
                        throw new ArgumentException("SerialNumber cannot be null or empty", nameof(entity.SerialNumber));
                    }

                    // Validate Number
                    if (string.IsNullOrEmpty(entity.Number))
                    {
                        throw new ArgumentException("Number cannot be null or empty", nameof(entity.Number));
                    }

                    // Validate DateMade
                    if (entity.DateMade > DateOnly.FromDateTime(DateTime.Now))
                    {
                        throw new ArgumentException("DateMade cannot be in the future", nameof(entity.DateMade));
                    }

                    // Validate Price
                    if (entity.Price <= 0)
                    {
                        throw new ArgumentException("Price must be a positive value", nameof(entity.Price));
                    }

                    context.PowerTools.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<PowerToolDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the power tool.");
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception:");
                }
                throw;
            }
        }





        public override async Task<PowerToolDto> UpdateAsync(int id, PowerToolDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.PowerTools.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<PowerTool>(dto);
                    context.PowerTools.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<PowerToolDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the power tool.");
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
                    var entity = await context.PowerTools.FindAsync(id);
                    if (entity != null)
                    {
                        context.PowerTools.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the power tool.");
                throw;
            }
        }

        public override async Task<IEnumerable<PowerToolDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var powerTools = await context.PowerTools
                    .Include(pt => pt.ToolType)
                    .Include(pt => pt.Condition)
                    .Include(pt => pt.ToolModel)
                    .Include(pt => pt.PowerSupplyType)
                    .ToListAsync();

                var powerToolDtos = _mapper.Map<IEnumerable<PowerToolDto>>(powerTools);

                foreach (var dto in powerToolDtos)
                {
                    dto.ToolTypeName = powerTools.First(pt => pt.Id == dto.Id).ToolType.Name;
                    dto.ConditionName = powerTools.First(pt => pt.Id == dto.Id).Condition.Name;
                    dto.ToolModelName = powerTools.First(pt => pt.Id == dto.Id).ToolModel.Name;
                    dto.PowerSupplyTypeName = powerTools.First(pt => pt.Id == dto.Id).PowerSupplyType.Name;
                }

                return powerToolDtos;
            }
        }

        public override async Task<PowerToolDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var powerTool = await context.PowerTools.FindAsync(id);
                return _mapper.Map<PowerToolDto>(powerTool);
            }
        }

        public async Task<PaginatedResult<PowerToolDto>> SearchToolsAsync(string searchTerm, string sortColumn, bool sortAscending, int pageNumber, int pageSize)
        {
            IQueryable<PowerTool> query = _repository.Context.PowerTools
                .Include(pt => pt.ToolType)
                .Include(pt => pt.ToolModel);

            // Filtering
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(pt =>
                    pt.ToolType.Name.Contains(searchTerm) ||
                    pt.ToolModel.Name.Contains(searchTerm) ||
                    pt.SerialNumber.Contains(searchTerm));
            }

            // Sorting
            query = sortColumn switch
            {
                "Type" => sortAscending
                    ? query.OrderBy(pt => pt.ToolType.Name)
                    : query.OrderByDescending(pt => pt.ToolType.Name),
                "Model" => sortAscending
                    ? query.OrderBy(pt => pt.ToolModel.Name)
                    : query.OrderByDescending(pt => pt.ToolModel.Name),
                "SerialNumber" => sortAscending
                    ? query.OrderBy(pt => pt.SerialNumber)
                    : query.OrderByDescending(pt => pt.SerialNumber),
                _ => query.OrderBy(pt => pt.Id)
            };

            // Pagination
            int totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            // Map to DTO
            var resultItems = _mapper.Map<List<PowerToolDto>>(items);

            return new PaginatedResult<PowerToolDto>
            {
                Items = resultItems,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<List<PowerToolDto>> GetToolsAsync(string searchTerm, string sortColumn, bool sortAscending)
        {
            var query = _repository.Context.PowerTools
                .Include(pt => pt.ToolType)
                .Include(pt => pt.Condition)
                .Include(pt => pt.ToolModel)
                .Include(pt => pt.PowerSupplyType)
                .AsQueryable();

            // Фільтрація
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(pt =>
                    pt.ToolType.Name.Contains(searchTerm) ||
                    pt.ToolModel.Name.Contains(searchTerm) ||
                    pt.Condition.Name.Contains(searchTerm) ||
                    pt.SerialNumber.Contains(searchTerm));
            }

            // Сортування
            query = sortColumn switch
            {
                "Name" => sortAscending ? query.OrderBy(pt => pt.ToolType.Name) : query.OrderByDescending(pt => pt.ToolType.Name),
                "Model" => sortAscending ? query.OrderBy(pt => pt.ToolModel.Name) : query.OrderByDescending(pt => pt.ToolModel.Name),
                "Condition" => sortAscending ? query.OrderBy(pt => pt.Condition.Name) : query.OrderByDescending(pt => pt.Condition.Name),
                _ => query.OrderBy(pt => pt.Id)
            };

            // Отримання даних
            var tools = await query.ToListAsync();

            // Мапінг у DTO
            return tools.Select(pt => new PowerToolDto
            {
                Id = pt.Id,
                ToolTypeId = pt.ToolType.Id,
                ToolTypeName = pt.ToolType.Name,
                ConditionId = pt.Condition.Id,
                ConditionName = pt.Condition.Name,
                ToolModelId = pt.ToolModel.Id,
                ToolModelName = pt.ToolModel.Name,
                PowerSupplyTypeId = pt.PowerSupplyType.Id,
                PowerSupplyTypeName = pt.PowerSupplyType.Name,
                SerialNumber = pt.SerialNumber,
                Number = pt.Number,
                Price = pt.Price,
                HasCase = pt.HasCase,
                DateMade = pt.DateMade
            }).ToList();
        }

    }
}

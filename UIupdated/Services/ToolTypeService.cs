using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class ToolTypeService : BaseService<ToolType, ToolTypeDto>, IBaseService<ToolTypeDto>
    {
        private readonly ILogger<ToolTypeService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ToolTypeService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<ToolTypeService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.ToolTypeRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<ToolTypeDto> AddAsync(ToolTypeDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<ToolType>(dto);
                    context.ToolTypes.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<ToolTypeDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the tool type.");
                throw;
            }
        }

        public override async Task<ToolTypeDto> UpdateAsync(int id, ToolTypeDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.ToolTypes.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<ToolType>(dto);
                    context.ToolTypes.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<ToolTypeDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the tool type.");
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
                    var entity = await context.ToolTypes.FindAsync(id);
                    if (entity != null)
                    {
                        context.ToolTypes.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the tool type.");
                throw;
            }
        }

        public override async Task<IEnumerable<ToolTypeDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var toolTypes = await context.ToolTypes.ToListAsync();
                return _mapper.Map<IEnumerable<ToolTypeDto>>(toolTypes);
            }
        }

        public override async Task<ToolTypeDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var toolType = await context.ToolTypes.FindAsync(id);
                return _mapper.Map<ToolTypeDto>(toolType);
            }
        }
    }
}

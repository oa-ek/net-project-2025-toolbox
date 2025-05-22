using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class ToolModelService : BaseService<ToolModel, ToolModelDto>, IBaseService<ToolModelDto>
    {
        private readonly ILogger<ToolModelService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ToolModelService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<ToolModelService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.ToolModelRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<ToolModelDto> AddAsync(ToolModelDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<ToolModel>(dto);
                    context.ToolModels.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<ToolModelDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the tool model.");
                throw;
            }
        }

        public override async Task<ToolModelDto> UpdateAsync(int id, ToolModelDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.ToolModels.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<ToolModel>(dto);
                    context.ToolModels.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<ToolModelDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the tool model.");
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
                    var entity = await context.ToolModels.FindAsync(id);
                    if (entity != null)
                    {
                        context.ToolModels.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the tool model.");
                throw;
            }
        }

        public override async Task<IEnumerable<ToolModelDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var toolModels = await context.ToolModels.ToListAsync();
                return _mapper.Map<IEnumerable<ToolModelDto>>(toolModels);
            }
        }

        public override async Task<ToolModelDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var toolModel = await context.ToolModels.FindAsync(id);
                return _mapper.Map<ToolModelDto>(toolModel);
            }
        }
    }
}

using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class ConditionService : BaseService<Condition, ConditionDto>, IBaseService<ConditionDto>
    {
        private readonly ILogger<ConditionService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ConditionService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<ConditionService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.ConditionRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<ConditionDto> AddAsync(ConditionDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<Condition>(dto);
                    context.Conditions.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<ConditionDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the condition.");
                throw;
            }
        }

        public override async Task<ConditionDto> UpdateAsync(int id, ConditionDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.Conditions.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<Condition>(dto);
                    context.Conditions.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<ConditionDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the condition.");
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
                    var entity = await context.Conditions.FindAsync(id);
                    if (entity != null)
                    {
                        context.Conditions.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the condition.");
                throw;
            }
        }

        public override async Task<IEnumerable<ConditionDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var conditions = await context.Conditions.ToListAsync();
                return _mapper.Map<IEnumerable<ConditionDto>>(conditions);
            }
        }

        public override async Task<ConditionDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var condition = await context.Conditions.FindAsync(id);
                return _mapper.Map<ConditionDto>(condition);
            }
        }
    }
}

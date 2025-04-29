using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class PowerSupplyTypeService : BaseService<PowerSupplyType, PowerSupplyTypeDto>, IBaseService<PowerSupplyTypeDto>
    {
        private readonly ILogger<PowerSupplyTypeService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PowerSupplyTypeService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<PowerSupplyTypeService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.PowerSupplyTypeRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<PowerSupplyTypeDto> AddAsync(PowerSupplyTypeDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<PowerSupplyType>(dto);
                    context.PowerSupplyTypes.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<PowerSupplyTypeDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the power supply type.");
                throw;
            }
        }

        public override async Task<PowerSupplyTypeDto> UpdateAsync(int id, PowerSupplyTypeDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.PowerSupplyTypes.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<PowerSupplyType>(dto);
                    context.PowerSupplyTypes.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<PowerSupplyTypeDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the power supply type.");
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
                    var entity = await context.PowerSupplyTypes.FindAsync(id);
                    if (entity != null)
                    {
                        context.PowerSupplyTypes.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the power supply type.");
                throw;
            }
        }

        public override async Task<IEnumerable<PowerSupplyTypeDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var powerSupplyTypes = await context.PowerSupplyTypes.ToListAsync();
                return _mapper.Map<IEnumerable<PowerSupplyTypeDto>>(powerSupplyTypes);
            }
        }

        public override async Task<PowerSupplyTypeDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var powerSupplyType = await context.PowerSupplyTypes.FindAsync(id);
                return _mapper.Map<PowerSupplyTypeDto>(powerSupplyType);
            }
        }
    }
}

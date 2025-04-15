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
    }
}


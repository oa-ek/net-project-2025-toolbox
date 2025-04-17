using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class BataryModelService : BaseService<BataryModel, BataryModelDto>, IBaseService<BataryModelDto>
    {
        private readonly ILogger<BataryModelService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BataryModelService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<BataryModelService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.BataryModelRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<BataryModelDto> AddAsync(BataryModelDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<BataryModel>(dto);
                    context.BataryModels.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<BataryModelDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the batary model.");
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception:");
                }
                throw;
            }
        }



        public override async Task<BataryModelDto> UpdateAsync(int id, BataryModelDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.BataryModels.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<BataryModel>(dto);
                    context.BataryModels.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<BataryModelDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the batary model.");
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
                    var entity = await context.BataryModels.FindAsync(id);
                    if (entity != null)
                    {
                        context.BataryModels.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the batary model.");
                throw;
            }
        }

        public override async Task<IEnumerable<BataryModelDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var bataryModels = await context.BataryModels.ToListAsync();
                return _mapper.Map<IEnumerable<BataryModelDto>>(bataryModels);
            }
        }

        public override async Task<BataryModelDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var bataryModel = await context.BataryModels.FindAsync(id);
                return _mapper.Map<BataryModelDto>(bataryModel);
            }
        }
    }
}

using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class BrandService : BaseService<Brand, BrandDto>, IBaseService<BrandDto>
    {
        private readonly ILogger<BrandService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BrandService(RepositoryContainer repositoryContainer, IMapper mapper, ILogger<BrandService> logger, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.BrandRepository, mapper)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task<BrandDto> AddAsync(BrandDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var entity = _mapper.Map<Brand>(dto);
                    context.Brands.Add(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<BrandDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the brand.");
                throw;
            }
        }

        public override async Task<BrandDto> UpdateAsync(int id, BrandDto dto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                    var existingEntity = await context.Brands.FindAsync(id);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    var entity = _mapper.Map<Brand>(dto);
                    context.Brands.Update(entity);
                    await context.SaveChangesAsync();
                    return _mapper.Map<BrandDto>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the brand.");
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
                    var entity = await context.Brands.FindAsync(id);
                    if (entity != null)
                    {
                        context.Brands.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the brand.");
                throw;
            }
        }

        public override async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var brands = await context.Brands.ToListAsync();
                return _mapper.Map<IEnumerable<BrandDto>>(brands);
            }
        }

        public override async Task<BrandDto> GetByIdAsync(int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var brand = await context.Brands.FindAsync(id);
                return _mapper.Map<BrandDto>(brand);
            }
        }
    }
}

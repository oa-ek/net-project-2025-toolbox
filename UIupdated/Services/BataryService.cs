using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace UIinterface.Services
{
    public class BataryService : BaseService<Batary, BataryDto>, IBaseService<BataryDto>
    {
        private readonly BaseRepository<Batary> _bataryRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory; // Add this field

        public BataryService(RepositoryContainer repositoryContainer, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
            : base(repositoryContainer.BataryRepository, mapper)
        {
            _bataryRepository = repositoryContainer.BataryRepository;
            _serviceScopeFactory = serviceScopeFactory; // Initialize the field
        }

        // Existing methods remain unchanged
        public async Task<List<BataryDto>> SearchToolsAsync(string searchTerm)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var query = context.Bataries
                    .Include(b => b.BataryModel)
                    .Include(b => b.Condition)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(b =>
                        b.BataryModel.Name.Contains(searchTerm) ||
                        b.SerialNumber.Contains(searchTerm) ||
                        b.Condition.Name.Contains(searchTerm));
                }

                var tools = await query.ToListAsync();
                return _mapper.Map<List<BataryDto>>(tools);
            }
        }

        public async Task<List<BataryDto>> GetToolsByLocationAsync(int locationId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var tools = await context.Bataries
                    .Include(b => b.BataryModel)
                    .Include(b => b.Condition)
                    .Where(b => b.LastLocationId == locationId)
                    .ToListAsync();

                return _mapper.Map<List<BataryDto>>(tools);
            }
        }
        public async Task<List<BataryDto>> GetToolsAsync(string searchTerm, string sortColumn, bool sortAscending)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TTContext>();
                var query = context.Bataries
                    .Include(b => b.BataryModel)
                    .Include(b => b.Condition)
                    .AsQueryable();

                // Фільтрація
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(b =>
                        b.BataryModel.Name.Contains(searchTerm) ||
                        b.SerialNumber.Contains(searchTerm) ||
                        b.Condition.Name.Contains(searchTerm));
                }

                // Сортування
                query = sortColumn switch
                {
                    "BataryModelName" => sortAscending ? query.OrderBy(b => b.BataryModel.Name) : query.OrderByDescending(b => b.BataryModel.Name),
                    "ConditionName" => sortAscending ? query.OrderBy(b => b.Condition.Name) : query.OrderByDescending(b => b.Condition.Name),
                    _ => query.OrderBy(b => b.Id)
                };

                var tools = await query.ToListAsync();
                return _mapper.Map<List<BataryDto>>(tools);
            }
        }

    }
}





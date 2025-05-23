using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class BataryService : BaseService<Batary, BataryDto>, IBaseService<BataryDto>
    {
        private readonly BaseRepository<Batary> _bataryRepository;

        public BataryService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.BataryRepository, mapper)
        {
            _bataryRepository = repositoryContainer.BataryRepository;
        }


        public async Task<List<BataryDto>> GetToolsAsync(string searchTerm, string sortColumn, bool sortAscending)
        {
            var query = _repository.Context.Bataries
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
                "Name" => sortAscending ? query.OrderBy(b => b.BataryModel.Name) : query.OrderByDescending(b => b.BataryModel.Name),
                "Condition" => sortAscending ? query.OrderBy(b => b.Condition.Name) : query.OrderByDescending(b => b.Condition.Name),
                _ => query.OrderBy(b => b.Id)
            };

            // Отримання даних
            var tools = await query.ToListAsync();

            // Мапінг у DTO
            return tools.Select(b => new BataryDto
            {
                Id = b.Id,
                BataryModelId = b.BataryModel.Id,
                BataryModelName = b.BataryModel.Name,
                ConditionId = b.Condition.Id,
                ConditionName = b.Condition.Name,
                SerialNumber = b.SerialNumber,
                Number = b.Number,
                Price = b.Price,
                DateMade = b.DateMade
            }).ToList();
        }

    }
}





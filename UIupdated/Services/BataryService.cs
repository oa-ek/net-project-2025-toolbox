using AutoMapper;
using Core;
using Core.DTOs;
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
    }
}





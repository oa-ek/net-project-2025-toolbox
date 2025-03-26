using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class BataryService : BaseService<Batary, BataryDto>, IBaseService<BataryDto>
    {
        public BataryService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.BateryRepository, mapper) { }
    }
}

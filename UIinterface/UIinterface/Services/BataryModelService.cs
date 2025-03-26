using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class BataryModelService : BaseService<BataryModel, BataryModelDto>, IBaseService<BataryModelDto>
    {
        public BataryModelService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.BataryModelRepository, mapper) { }
    }
}

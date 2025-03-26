using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class HandToolService : BaseService<HandTool, HandToolDto>, IBaseService<HandToolDto>
    {
        public HandToolService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.HandToolRepository, mapper) { }
    }
}

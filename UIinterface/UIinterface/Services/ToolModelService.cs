using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class ToolModelService : BaseService<ToolModel, ToolModelDto>, IBaseService<ToolModelDto>
    {
        public ToolModelService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.ToolModelRepository, mapper) { }
    }
}

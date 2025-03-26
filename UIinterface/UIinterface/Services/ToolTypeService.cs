using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class ToolTypeService : BaseService<ToolType, ToolTypeDto>, IBaseService<ToolTypeDto>
    {
        public ToolTypeService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.ToolTypeRepository, mapper) { }
    }
}

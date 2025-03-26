using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class PowerToolService : BaseService<PowerTool, PowerToolDto>, IBaseService<PowerToolDto>
    {
        public PowerToolService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.PowerToolRepository, mapper) { }
    }
}

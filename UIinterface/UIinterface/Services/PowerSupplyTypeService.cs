using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class PowerSupplyTypeService : BaseService<PowerSupplyType, PowerSupplyTypeDto>, IBaseService<PowerSupplyTypeDto>
    {
        public PowerSupplyTypeService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.PowerSupplyTypeRepository, mapper) { }
    }
}

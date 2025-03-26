using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class ConditionService : BaseService<Condition, ConditionDto>, IBaseService<ConditionDto>
    {
        public ConditionService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.ConditionRepository, mapper) { }
    }
}

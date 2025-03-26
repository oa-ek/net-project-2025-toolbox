using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class PositionService : BaseService<Position, PositionDto>, IBaseService<PositionDto>
    {
        public PositionService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.PositionRepository, mapper) { }
    }
}

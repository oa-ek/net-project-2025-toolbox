using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class BossService : BaseService<Boss, BossDto>, IBaseService<BossDto>
    {
        public BossService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.BossRepository, mapper) { }
    }
}

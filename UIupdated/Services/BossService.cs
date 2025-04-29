using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class BossService : BaseService<Boss, BossDto>
    {
        public BossService(BaseRepository<Boss> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class SystemAdminService : BaseService<SystemAdmin, SystemAdminDto>, IBaseService<SystemAdminDto>
    {
        public SystemAdminService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.SystemAdminRepository, mapper) { }
    }
}

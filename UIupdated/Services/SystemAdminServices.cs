using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class SystemAdminService : BaseService<SystemAdmin, SystemAdminDto>, IBaseService<SystemAdminDto>
    {
        public SystemAdminService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.SystemAdminRepository, mapper) { }

        public override async Task<SystemAdminDto> UpdateAsync(int id, SystemAdminDto dto)
        {
            var existingEntity = await _repository.GetAsync(id);
            if (existingEntity != null)
            {
                _repository.Context.Entry(existingEntity).State = EntityState.Detached;
            }

            var entity = _mapper.Map<SystemAdmin>(dto);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<SystemAdminDto>(entity);
        }
    }
}

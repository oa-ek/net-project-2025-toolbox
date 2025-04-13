using AutoMapper;
using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UIinterface.Services
{
    public class PositionService : BaseService<Position, PositionDto>, IBaseService<PositionDto>
    {
        public PositionService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.PositionRepository, mapper) { }

        public override async Task<PositionDto> UpdateAsync(int id, PositionDto dto)
        {
            var existingEntity = await _repository.GetAsync(id);
            if (existingEntity != null)
            {
                _repository.Context.Entry(existingEntity).State = EntityState.Detached;
            }

            var entity = _mapper.Map<Position>(dto);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<PositionDto>(entity);
        }

        //
        public override async Task<IEnumerable<PositionDto>> GetAllAsync()
        {
            var positions = await _repository.Context.Positions
                .Include(p => p.Boss)
                .ToListAsync();

            var positionDtos = _mapper.Map<IEnumerable<PositionDto>>(positions);

            foreach (var positionDto in positionDtos)
            {
                var boss = positions.FirstOrDefault(p => p.Id == positionDto.Id)?.Boss;
                if (boss != null)
                {
                    positionDto.BossName = $"{boss.FirstName} {boss.LastName}";
                }
            }

            return positionDtos;
        }

        public override async Task<PositionDto> GetByIdAsync(int id)
        {
            var position = await _repository.Context.Positions
                .Include(p => p.Boss)
                .FirstOrDefaultAsync(p => p.Id == id);

            var positionDto = _mapper.Map<PositionDto>(position);

            if (position?.Boss != null)
            {
                positionDto.BossName = $"{position.Boss.FirstName} {position.Boss.LastName}";
            }

            return positionDto;
        }
    }
}

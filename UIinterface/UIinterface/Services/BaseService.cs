using AutoMapper;
using Repository;

namespace UIinterface.Services
{
    public class BaseService<T, TDto> : IBaseService<TDto> where T : class
    {
        protected readonly BaseRepository<T> _repository;
        protected readonly IMapper _mapper;

        public BaseService(BaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            return entity == null ? default : _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _repository.CreateAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(int id, TDto dto)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null) return default;

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}

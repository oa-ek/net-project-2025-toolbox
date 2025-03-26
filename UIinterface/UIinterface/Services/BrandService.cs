using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class BrandService : BaseService<Brand, BrandDto>, IBaseService<BrandDto>
    {
        public BrandService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.BrandRepository, mapper) { }
    }
}

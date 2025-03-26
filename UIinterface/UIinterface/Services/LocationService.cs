using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class LocationService : BaseService<Location, LocationDto>, IBaseService<LocationDto>
    {
        public LocationService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.LocationRepository, mapper) { }
    }
}

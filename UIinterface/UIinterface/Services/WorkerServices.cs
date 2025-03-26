using AutoMapper;
using Core;
using Core.DTOs;
using Repository;

namespace UIinterface.Services
{
    public class WorkerService : BaseService<Worker, WorkerDto>, IBaseService<WorkerDto>
    {
        public WorkerService(RepositoryContainer repositoryContainer, IMapper mapper)
            : base(repositoryContainer.WorkerRepository, mapper) { }
    }
}

using Core;
using Repository.Repos;

namespace Repository
{
    public class RepositoryContainer
    {
        private readonly TTContext _context;

        public RepositoryContainer(TTContext context)
        {
            _context = context;
            BrandRepository = new BaseRepository<Brand>(_context);
            BataryModelRepository = new BaseRepository<BataryModel>(_context);
            ConditionRepository = new BaseRepository<Condition>(_context);
            WorkerRepository = new BaseRepository<Worker>(_context);
            LocationRepository = new LocationRepository(_context);
            PositionRepository = new BaseRepository<Position>(_context);
            ToolTypeRepository = new BaseRepository<ToolType>(_context);
            PowerSupplyTypeRepository = new BaseRepository<PowerSupplyType>(_context);
            ToolModelRepository = new BaseRepository<ToolModel>(_context);
            PowerToolRepository = new BaseRepository<PowerTool>(_context);
            BossRepository = new BossRepository(_context);
            SystemAdminRepository = new BaseRepository<SystemAdmin>(_context);
            HandToolRepository = new BaseRepository<HandTool>(_context);
            BataryRepository = new BaseRepository<Batary>(_context);
        }

        public BaseRepository<Brand> BrandRepository { get; }
        public BaseRepository<BataryModel> BataryModelRepository { get; }
        public BaseRepository<Condition> ConditionRepository { get; }
        public BaseRepository<Worker> WorkerRepository { get; }
        public LocationRepository LocationRepository { get; }
        public BaseRepository<Position> PositionRepository { get; }
        public BaseRepository<ToolType> ToolTypeRepository { get; }
        public BaseRepository<PowerSupplyType> PowerSupplyTypeRepository { get; }
        public BaseRepository<ToolModel> ToolModelRepository { get; }
        public BaseRepository<PowerTool> PowerToolRepository { get; }
        public BossRepository BossRepository { get; }
        public BaseRepository<SystemAdmin> SystemAdminRepository { get; }
        public BaseRepository<HandTool> HandToolRepository { get; }
        public BaseRepository<Batary> BataryRepository { get; }
    }
}





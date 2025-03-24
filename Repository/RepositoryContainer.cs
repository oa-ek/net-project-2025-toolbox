using Core;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContainer
    {
        private readonly TTContext _context;
        public BaseRepository<Brand> BrandRepository { get; set; }
        public BaseRepository<BataryModel> BataryModelRepository { get; set; }
        public BaseRepository<Condition> ConditionRepository { get; set; }
        public BaseRepository<Worker> WorkerRepository { get; set; }
        public BaseRepository<Location> LocationRepository { get; set; }
        public BaseRepository<Position> PositionRepository { get; set; }
        public BaseRepository<ToolType> ToolTypeRepository { get; set; }
        public BaseRepository<PowerSupplyType> PowerSupplyTypeRepository { get; set; }
        public BaseRepository<ToolModel> ToolModelRepository { get; set; }
        public BaseRepository<PowerTool> PowerToolRepository { get; set; }
        public BaseRepository<Boss> BossRepository { get; set; }
        public BaseRepository<SystemAdmin> SystemAdminRepository { get; set; }
        //added missing 
        public BaseRepository<HandTool> HandToolRepository { get; set; }
        public BaseRepository<Batary> BateryRepository { get; set; }

        public TTContext Context => _context;
        public RepositoryContainer(TTContext context)
        {
            _context = context;
            BrandRepository = new BaseRepository<Brand>(_context);
            BataryModelRepository = new BaseRepository<BataryModel>(_context);
            ConditionRepository = new BaseRepository<Condition>(_context);
            WorkerRepository = new BaseRepository<Worker>(_context);
            LocationRepository = new BaseRepository<Location>(_context);
            PositionRepository = new BaseRepository<Position>(_context);
            ToolTypeRepository = new BaseRepository<ToolType>(_context);
            PowerSupplyTypeRepository = new BaseRepository<PowerSupplyType>(_context);
            ToolModelRepository = new BaseRepository<ToolModel>(_context);
            PowerToolRepository = new BaseRepository<PowerTool>(_context);
            BossRepository = new BaseRepository<Boss>(_context);
            SystemAdminRepository = new BaseRepository<SystemAdmin>(_context);
            ////
            HandToolRepository = new BaseRepository<HandTool>(_context);
            BateryRepository = new BaseRepository<Batary>(_context);
        }
    }

}

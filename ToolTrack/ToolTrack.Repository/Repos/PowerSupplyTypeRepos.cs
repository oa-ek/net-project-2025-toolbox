using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTrack.Repository.Repos
{
    public class PowerSupplyTypeRepository : BaseRepository<PowerSupplyType>
    {
        public PowerSupplyTypeRepository(DbContext context) : base(context) { }
    }
}

using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTrack.Repository.Repos
{

    public class WorkStatisticRepository : BaseRepository<WorkStatistic>
    {
        public WorkStatisticRepository(DbContext context) : base(context) { }
    }
}

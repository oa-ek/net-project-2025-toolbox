using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class PositionRepository : BaseRepository<Position>
    {
        public PositionRepository(TTContext context) : base(context) { }
    }
}

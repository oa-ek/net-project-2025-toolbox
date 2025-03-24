using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class PowerToolRepository : BaseRepository<PowerTool>
    {
        public PowerToolRepository(TTContext context) : base(context) { }
    }
}

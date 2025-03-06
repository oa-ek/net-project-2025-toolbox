using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTrack.Repository.Repos
{
    public class SystemAdminRepository : BaseRepository<SystemAdmin>
    {
        public SystemAdminRepository(TTContext context) : base(context) { }
    }
}

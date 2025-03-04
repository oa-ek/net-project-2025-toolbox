using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int LocationId { get; set; }
        public int BryhadyrId { get; set; } // worker
        public int PositionId { get; set; }
        public int BossId { get; set; }
        public double Latitute { get; set; }
        public double Longitute { get; set; }

        // navigation properties
        public Location Location { get; set; }
        public Position Position { get; set; }
        public Boss Boss { get; set; }
        public IEnumerable<WorkStatistic> WorkStatistics { get; set; }
        public Worker Workers { get; set; } // bryhadyr
        public IEnumerable<PowerTool> PowerTools { get; set; }
        public IEnumerable<HandTool> HandTools { get; set; }
        public IEnumerable<Batary> Batarys { get; set; }
    }
}

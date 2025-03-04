using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Position
    {
        public string Name { get; set; }
        public int SalaryPerHour { get; set; } // UAN
        public int BossId { get; set; }

        // navigation property
        public IEnumerable<Worker> Workers { get; set; }
    }
}

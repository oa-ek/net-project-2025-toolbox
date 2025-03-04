using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class WorkStatistic
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public DateOnly Date { get; set; }
        public double HoursWorked { get; set; }
        public int LocationId { get; set; }
        public bool Submiteed { get; set; }

        // navigation properties
        public Worker Workers { get; set; }
    }
}

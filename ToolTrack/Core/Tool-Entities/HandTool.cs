using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class HandTool
    {
        public int id { get; set; }
        public int BrandId { get; set; }
        public int ConditionId { get; set; }
        public int ToolTypeId { get; set; }
        public int LastWorkerId { get; set; }
        public int LastLocationId { get; set; }
        public double Price { get; set; }

        // Navigation properties
        public Brand Brand { get; set; }
        public Condition Condition { get; set; }
        public ToolType ToolType { get; set; }
        public Worker LastWorker { get; set; }
    }
}

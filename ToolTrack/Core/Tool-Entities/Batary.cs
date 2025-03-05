using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Batary
    {
        [Key]
        public int Id { get; set; }
        public int BataryModelId { get; set; }
        public DateOnly DateMade { get; set; }
        public string SerialNumber { get; set; }
        public string Number { get; set; }
        public double Price { get; set; } // $
        public int ConditionId { get; set; }
        public int? LastWorkerId { get; set; }
        public int LastLocationId { get; set; }

        // Navigation properties
        public BataryModel BataryModel { get; set; }
        public Condition Condition { get; set; }
        public Worker LastWorker { get; set; }
    }
}

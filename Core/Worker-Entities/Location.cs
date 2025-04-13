using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitute { get; set; }
        public double Longitute { get; set; }
        // navigation properties
        public ICollection<Boss> Bosses { get; set; } = new List<Boss>();
        public ICollection<Worker> Workers { get; set; } = new List<Worker>();
    }
}

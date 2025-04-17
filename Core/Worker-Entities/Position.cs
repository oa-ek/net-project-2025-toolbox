using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SalaryPerHour { get; set; } // UAN
        public int BossId { get; set; }

        // navigation property
        public Boss Boss { get; set; } //added new property
        public IEnumerable<Worker> Workers { get; set; }
    }
}

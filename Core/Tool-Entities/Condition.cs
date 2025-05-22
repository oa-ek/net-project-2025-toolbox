using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Condition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        // navigation property
        public IEnumerable<PowerTool> PowerTools { get; set; }
        public IEnumerable<HandTool> HandTools { get; set; }
        public IEnumerable<Batary> Bataries { get; set; }
    }
}

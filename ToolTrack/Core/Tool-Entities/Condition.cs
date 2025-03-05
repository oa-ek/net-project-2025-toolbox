using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Condition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property
        public IEnumerable<PowerTool> Tools { get; set; }
        public IEnumerable<HandTool> HandTools { get; set; }
        public IEnumerable<Batary> Bataries { get; set; }
    }
}

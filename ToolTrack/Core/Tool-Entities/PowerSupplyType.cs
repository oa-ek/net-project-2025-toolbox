using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class PowerSupplyType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property
        public IEnumerable<PowerTool> Tools { get; set; }
    }
}

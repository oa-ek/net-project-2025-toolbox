using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class ToolModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }

        // navigation property
        public IEnumerable<PowerTool> PowerTools { get; set; }
        public Brand Brand { get; set; }
    }
}

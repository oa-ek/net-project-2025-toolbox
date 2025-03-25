using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property
        public IEnumerable<ToolModel> ToolModels { get; set; }
        public IEnumerable<BataryModel> BataryModels { get; set; }
        public IEnumerable<HandTool> HandTools { get; set; }
    }
}

namespace Core
{
    public class ToolModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }

        // navigation property
        public IEnumerable<PowerTool> Tools { get; set; }
        public Brand Brand { get; set; }
    }
}

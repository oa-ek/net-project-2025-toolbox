namespace Core
{
    public class Brand
    {
        public int id { get; set; }
        public string Name { get; set; }

        // navigation property
        public IEnumerable<ToolModel> ToolModels { get; set; }
        public IEnumerable<HandTool> HandTools { get; set; }
    }
}

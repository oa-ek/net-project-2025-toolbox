namespace Core
{
    public class ToolType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property
        public IEnumerable<PowerTool> Tools { get; set; }
        public IEnumerable<HandTool> HandTool { get; set; }

    }
}

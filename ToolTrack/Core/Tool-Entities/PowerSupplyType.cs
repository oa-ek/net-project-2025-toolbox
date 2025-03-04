namespace Core
{
    public class PowerSupplyType
    {
        public int id { get; set; }
        public string Name { get; set; }

        // navigation property
        public IEnumerable<PowerTool> Tools { get; set; }
    }
}

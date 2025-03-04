namespace Core
{
    public class PowerSupplyType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property
        public IEnumerable<PowerTool> Tools { get; set; }
    }
}

namespace Core
{
    public class PowerTool
    {
        public int id { get; set; }
        public int TypeId { get; set; }
        public int ConditionId { get; set; }
        public int LastWorkerId { get; set; }
        public int LastLocationId { get; set; }
        public int ToolModelId { get; set; }
        public bool HaveCase { get; set; }
        public DateOnly DateMade { get; set; }
        public string SerialNumber { get; set; }
        public string Number { get; set; }
        public double Price { get; set; } // $
        public int PowerSupplyTypeId { get; set; } 

        // Navigation properties
        public ToolType ToolType { get; set; }
        public Condition Condition { get; set; }
        public ToolModel ToolModel { get; set; }
        public PowerSupplyType PowerSupplyType { get; set; }
        public Worker LastWorker { get; set; }
    }
}

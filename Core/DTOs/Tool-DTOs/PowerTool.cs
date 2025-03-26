namespace Core.DTOs
{
    public class PowerToolDto
    {
        public int Id { get; set; }
        public int ToolTypeId { get; set; }
        public int ConditionId { get; set; }
        public int? LastWorkerId { get; set; }
        public int LastLocationId { get; set; }
        public int ToolModelId { get; set; }
        public bool HasCase { get; set; }
        public DateOnly DateMade { get; set; }
        public string SerialNumber { get; set; }
        public string Number { get; set; }
        public double Price { get; set; }
        public int PowerSupplyTypeId { get; set; }

        public string ToolTypeName { get; set; }
        public string ConditionName { get; set; }
        public string ToolModelName { get; set; }
        public string PowerSupplyTypeName { get; set; }
    }
}

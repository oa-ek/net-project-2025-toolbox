namespace Core.DTOs
{
    public class HandToolDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ConditionId { get; set; }
        public int ToolTypeId { get; set; }
        public int? LastWorkerId { get; set; }
        public int LastLocationId { get; set; }
        public double Price { get; set; }

        public string BrandName { get; set; }
        public string ConditionName { get; set; }
        public string ToolTypeName { get; set; }
    }
}

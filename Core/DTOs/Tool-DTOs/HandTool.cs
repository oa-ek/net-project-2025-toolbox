namespace Core.DTOs
{
    public class HandToolDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // Додано властивість Name
        public int BrandId { get; set; }
        public int ConditionId { get; set; }
        public int ToolTypeId { get; set; }
        public double Price { get; set; }
        public int? LastWorkerId { get; set; } // ДОДАЙТЕ ЦЕЙ РЯДОК
    }
}


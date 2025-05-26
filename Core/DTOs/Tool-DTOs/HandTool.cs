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

        // Додано для відстеження вибору
        public bool IsSelected { get; set; } = false;
        // Додайте ці властивості:
        public int? LastWorkerId { get; set; }
        public int LastLocationId { get; set; }

        public string ToolTypeName { get; set; } // Назва типу інструменту (модель)
        public string BrandName { get; set; } // ← додай

    }
}


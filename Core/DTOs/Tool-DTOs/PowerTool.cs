using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PowerToolDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tool Type is required.")]


        public int? ToolTypeId { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        public int? ConditionId { get; set; }

        public int? LastWorkerId { get; set; }

        [Required(ErrorMessage = "Last Location is required.")]
        public int? LastLocationId { get; set; }

        [Required(ErrorMessage = "Tool Model is required.")]
        public int? ToolModelId { get; set; }

        public bool HasCase { get; set; }

        [Required(ErrorMessage = "Date Made is required.")]
        public DateOnly DateMade { get; set; }

        [Required(ErrorMessage = "Serial Number is required.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Number is required.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Power Supply Type is required.")]
        public int? PowerSupplyTypeId { get; set; }

        public string? ToolTypeName { get; set; }
        public string? ConditionName { get; set; }
        public string? ToolModelName { get; set; }
        public string? PowerSupplyTypeName { get; set; }

        // Додано властивість для відстеження вибору
        public bool IsSelected { get; set; }
    }
}


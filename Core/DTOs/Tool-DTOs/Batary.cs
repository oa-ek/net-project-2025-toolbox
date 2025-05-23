using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.DTOs
{
    public class BataryDto
    {
        public int Id { get; set; }

        [Required]
        public int BataryModelId { get; set; }

        [Required]
        public DateOnly DateMade { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Serial Number can only contain letters and numbers.")]
        public string SerialNumber { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Number can only contain letters and numbers.")]
        public string Number { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Required]
        public int ConditionId { get; set; }

        public int? LastWorkerId { get; set; }

        [Required]
        public int LastLocationId { get; set; }

        public string BataryModelName { get; set; }
        public string ConditionName { get; set; }

        // Додано для відстеження вибору
        public bool IsSelected { get; set; } = false;

    }
}

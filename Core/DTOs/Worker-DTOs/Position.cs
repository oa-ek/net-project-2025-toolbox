using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PositionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary per hour is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary per hour must be greater than 0")]
        public int SalaryPerHour { get; set; }

        [Required(ErrorMessage = "Boss is required")]
        public int BossId { get; set; }

        public string BossName { get; set; }
    }
}

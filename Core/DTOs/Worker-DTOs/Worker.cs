using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class WorkerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Boss is required.")]
        public int BossId { get; set; }

        public double? Latitute { get; set; }
        public double? Longitute { get; set; }
        public string PositionName { get; set; }
        public string LocationName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Latitude is required.")]
        [Range(0, 1000000000, ErrorMessage = "Latitude must be between 0 and 100000000")]
        public double Latitute { get; set; }

        [Required(ErrorMessage = "Longitude is required.")]
        [Range(0, 1000000000, ErrorMessage = "Longitude must be between 0 and 1000000000.")]
        public double Longitute { get; set; }

        public List<BossDto> Bosses { get; set; } = new List<BossDto>();
        public List<WorkerDto> Workers { get; set; } = new List<WorkerDto>();
    }

}



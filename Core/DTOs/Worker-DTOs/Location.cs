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
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double Latitute { get; set; }

        [Required(ErrorMessage = "Longitude is required.")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double Longitute { get; set; }

        public List<BossDto> Bosses { get; set; } = new List<BossDto>();
        public List<WorkerDto> Workers { get; set; } = new List<WorkerDto>();
    }

}



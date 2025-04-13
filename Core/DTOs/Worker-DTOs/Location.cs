namespace Core.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitute { get; set; }
        public double Longitute { get; set; }
        public List<BossDto> Bosses { get; set; } = new List<BossDto>();
        public List<WorkerDto> Workers { get; set; } = new List<WorkerDto>();
    }
}



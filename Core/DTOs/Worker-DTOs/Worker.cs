namespace Core.DTOs
{
    public class WorkerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int LocationId { get; set; }
        public int? BryhadyrId { get; set; }
        public int PositionId { get; set; }
        public int BossId { get; set; }
        public double? Latitute { get; set; }
        public double? Longitute { get; set; }

        public string PositionName { get; set; }
        public string LocationName { get; set; }
    }
}

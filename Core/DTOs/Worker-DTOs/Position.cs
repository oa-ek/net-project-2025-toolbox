namespace Core.DTOs
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SalaryPerHour { get; set; }
        
        public int BossId { get; set; }
        public string BossName { get; set; }
    }
}

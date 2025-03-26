namespace Core.DTOs
{
    public class BataryDto
    {
        public int Id { get; set; }
        public int BataryModelId { get; set; }
        public DateOnly DateMade { get; set; }
        public string SerialNumber { get; set; }
        public string Number { get; set; }
        public double Price { get; set; }
        public int ConditionId { get; set; }
        public int? LastWorkerId { get; set; }
        public int LastLocationId { get; set; }

        public string BataryModelName { get; set; }
        public string ConditionName { get; set; }
    }
}

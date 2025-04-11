namespace Core.DTOs
{
    public class BossDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; } // add password property

        public List<BossDto> Bosses { get; set; }



    }
}

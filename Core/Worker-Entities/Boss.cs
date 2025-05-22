using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Boss
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        // navigation properties
    public ICollection<Location> Locations { get; set; } = new List<Location>();
        public IEnumerable<Worker> Workers { get; set; }
        public IEnumerable<SystemAdmin> SystemAdmins { get; set; }
    }
}

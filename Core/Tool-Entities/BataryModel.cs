using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BataryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }

        // navigation property
        public Brand Brand { get; set; }
        public IEnumerable<Batary> Bataries { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ConditionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
    }

}

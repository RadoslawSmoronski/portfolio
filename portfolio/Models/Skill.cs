using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}

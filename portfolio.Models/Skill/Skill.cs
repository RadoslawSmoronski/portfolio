using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace portfolio.Models.Skill
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string NameENG { get; set; } = "";
        [Required]
        [MaxLength(25)]
        public string NamePL { get; set; } = "";
        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}

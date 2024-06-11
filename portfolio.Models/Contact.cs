using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Content { get; set; }
        public string? UrlAddress { get; set; }
        public string? Icon { get; set; }
    }
}

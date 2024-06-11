using System.ComponentModel.DataAnnotations;

namespace portfolio.Models.Email
{
    public class ContactForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Subject { get; set; } = "";

        [Required]
        public string Content { get; set; } = "";
    }
}

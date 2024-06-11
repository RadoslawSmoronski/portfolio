using System.ComponentModel.DataAnnotations;

namespace portfolio.Models.Email
{
    public class EmailMessage : ContactForm
    {
        [Required]
        public int Id { get; set; }
        public bool IsReaded { get; set; } = false;
        public DateTime SentAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    public class EditAdminLogin : AdminLogin
    {
        [Required]
        public string NewPassword { get; set; } = "";

        [Compare("NewPassword")]
        public string ConfrimNewPassword { get; set; } = "";
    }
}

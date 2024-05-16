using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models
{
    public class EditAdminLogin : AdminLogin
    {
        [Required]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfrimNewPassword { get; set; }
    }
}

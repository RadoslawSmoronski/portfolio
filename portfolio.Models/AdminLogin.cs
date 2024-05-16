using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models
{
    public class AdminLogin
    {
        [Required]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.Email
{
    public class EmailMessage : ContactForm
    {
        [Required]
        public int Id { get; set; }
        public bool IsReaded { get; set; } = false;
    }
}

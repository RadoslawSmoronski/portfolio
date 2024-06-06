using Newtonsoft.Json;
using portfolio.Models.ConfigureData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models
{
    public class AdminLogin : IConfigureDataClass
    {
        [Required]
        public string Password { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

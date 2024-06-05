using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.ConfigureData
{
    public class AdminPanelAccessPassword
    {
        [Required]
        public string HashedPassword { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

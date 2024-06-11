using Newtonsoft.Json;
using portfolio.Models.ConfigureData;
using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    public class AdminLogin : IConfigureDataClass
    {
        [Required]
        public string Password { get; set; } = "";

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

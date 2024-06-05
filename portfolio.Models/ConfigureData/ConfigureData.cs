using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.ConfigureData
{
    public class ConfigureData
    {
        public int Id { get; set; }
        public string JSON { get; set; }

        public T Convert<T>()
        {
            return JsonConvert.DeserializeObject<T>(JSON);
        }
    }
}

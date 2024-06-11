using Newtonsoft.Json;

namespace portfolio.Models.ConfigureData
{
    public class ConfigureData
    {
        public int Id { get; set; }
        public string? JSON { get; set; }

        public T Convert<T>()
        {
            return JsonConvert.DeserializeObject<T>(JSON);
        }
    }
}

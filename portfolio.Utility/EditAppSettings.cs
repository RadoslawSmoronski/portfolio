using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Utility
{
    public static class EditAppSettings
    {
        public static void AddOrUpdateAppSetting<T>(string key, T value)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                var secondFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                var sectionPath = key.Split(":")[0];

                if (!string.IsNullOrEmpty(sectionPath))
                {
                    var keyPath = key.Split(":")[1];
                    jsonObj[sectionPath][keyPath] = value;
                }
                else
                {
                    jsonObj[sectionPath] = value;
                }

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
                File.WriteAllText(secondFilePath, output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

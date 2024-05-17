using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace portfolio.DataAccess.Json
{
    public static class JsonFileManager<T>
    {
        public static T Get()
        {
            Type typ = typeof(T);
            string fileName = typ.Name;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "json", $"{fileName}.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json);
            }


            T newObject = Activator.CreateInstance<T>();
            string newJson = JsonConvert.SerializeObject(newObject);
            File.WriteAllText(filePath, newJson);
            return newObject;
        }

        public static void Save(T entity)
        {
            if (entity == null) return;

            Type typ = entity.GetType();
            string fileName = typ.Name;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "json", $"{fileName}.json");

            string newJson = JsonConvert.SerializeObject(entity);
            File.WriteAllText(filePath, newJson);
        }

        public static void AddOrUpdateAppSetting(string key, T value)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

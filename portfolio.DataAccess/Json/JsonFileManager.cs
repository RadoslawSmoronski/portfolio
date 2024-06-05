using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Json
{
    public class JsonFileManager : IJsonFileManager
    {
        public string RootDirectoryPath { get; }

        public JsonFileManager(string rootDirectoryPath)
        {
            RootDirectoryPath = rootDirectoryPath;
        }

        public T Get<T>()
        {
            //Type typ = typeof(T);
            //string fileName = typ.Name;
            //string filePath = Path.Combine(RootDirectoryPath, "json", $"{fileName}.json");

            //if (File.Exists(filePath))
            //{
            //    string json = File.ReadAllText(filePath);
            //    return JsonConvert.DeserializeObject<T>(json);
            //}


            T newObject = Activator.CreateInstance<T>();
            //string newJson = JsonConvert.SerializeObject(newObject);
            //File.WriteAllText(filePath, newJson);
            return newObject;
        }

        public void Save<T>(T entity)
        {
            if (entity == null) return;

            Type typ = entity.GetType();
            string fileName = typ.Name;
            string filePath = Path.Combine(RootDirectoryPath, "json", $"{fileName}.json"); ;

            string newJson = JsonConvert.SerializeObject(entity);
            File.WriteAllText(filePath, newJson);
        }
    }
}

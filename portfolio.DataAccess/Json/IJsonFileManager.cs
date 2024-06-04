using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Json
{
    public interface IJsonFileManager
    {
        string RootDirectoryPath { get; }
        public T Get<T>();
        public void Save<T>(T entity);
    }
}

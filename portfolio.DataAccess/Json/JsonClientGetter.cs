using Newtonsoft.Json;
using portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Json
{
    public static class JsonClientGetter<T> where T : class
    {
        public static T Get()
        {
            if (typeof(T) == typeof(NavbarLogo))
            {
                return JsonFileManager<T>.Get();
            }
            else
            {
                throw new InvalidOperationException("Unsupported type.");
            }
        }
    }
}

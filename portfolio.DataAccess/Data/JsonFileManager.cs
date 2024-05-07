using portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Data
{
    public class JsonFileManager
    {
        public AboutMe AboutMe { get; private set; }

        public JsonFileManager()
        {
            AboutMe = GetAboutMeFromJsonToObject();
        }

        private AboutMe GetAboutMeFromJsonToObject()
        {
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean vel augue purus. Etiam imperdiet dui a dui ultricies, eget sagittis lacus porttitor.Etiam id eleifend sapien. Suspendisse tempus mauris maximus fringilla rhoncus. Mauris vel nisi mollis, varius ex in, maximus enim. Aenean iaculis lobortis sem sed hendrerit.";

            AboutMe aboutMe = new AboutMe { Title = "Name and Surname", Description = description };

            return aboutMe;
        }
    }
}

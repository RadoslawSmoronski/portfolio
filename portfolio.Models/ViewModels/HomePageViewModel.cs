﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.ViewModels
{
    public class HomePageViewModel
    {
        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }

        public List<Contact> Contacts { get; set; }
        public AboutMe AboutMe { get; set; }


    }
}

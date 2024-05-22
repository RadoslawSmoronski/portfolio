using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.Email
{
    public class AutoEmailMessageContent
    {
        public string? SubjectENG { get; set; } = "Test Subject";
        public string? SubjectPL { get; set; } = "Testowy Tytuł";
        public string? ContentENG { get; set; }
            = "[ENG] Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
            " Etiam vel augue congue, aliquet risus in, auctor odio." +
            " Nunc quis tincidunt justo, at malesuada dui. Cras finibus mollis blandit." +
            " Cras egestas, risus vel aliquet egestas, neque felis tempus est, eu scelerisque metus arcu nec orci." +
            " Cras posuere.";
        public string? ContentPL { get; set; }
            = "[PL] Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
            " Etiam vel augue congue, aliquet risus in, auctor odio." +
            " Nunc quis tincidunt justo, at malesuada dui. Cras finibus mollis blandit." +
            " Cras egestas, risus vel aliquet egestas, neque felis tempus est, eu scelerisque metus arcu nec orci." +
            " Cras posuere.";
    }
}

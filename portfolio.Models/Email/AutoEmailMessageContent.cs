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
        public string? Subject { get; set; } = "Test Subject";
        public string? Content { get; set; }
            = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
            " Etiam vel augue congue, aliquet risus in, auctor odio." +
            " Nunc quis tincidunt justo, at malesuada dui. Cras finibus mollis blandit." +
            " Cras egestas, risus vel aliquet egestas, neque felis tempus est, eu scelerisque metus arcu nec orci." +
            " Cras posuere.";
    }
}

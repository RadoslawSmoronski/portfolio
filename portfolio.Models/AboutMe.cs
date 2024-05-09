using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    public class AboutMe
    {
        public string? Title { get; set; } = "John Smith";
        public string? Description { get; set; }
            = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
            " Etiam vel augue congue, aliquet risus in, auctor odio." +
            " Nunc quis tincidunt justo, at malesuada dui. Cras finibus mollis blandit." +
            " Cras egestas, risus vel aliquet egestas, neque felis tempus est, eu scelerisque metus arcu nec orci." +
            " Cras posuere.";
        public string? ImageUrl { get; set; }
    }
}

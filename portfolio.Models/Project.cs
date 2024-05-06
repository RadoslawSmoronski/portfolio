using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        public string GitRepositoryUrl { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }

    }
}

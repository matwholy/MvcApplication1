using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }
    }
}
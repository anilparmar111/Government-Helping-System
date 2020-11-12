using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models
{
    public class Employee
    { 
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Area { get; set; }

        public string ZipCode { get; set; }

        public string post { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models
{
    public class Citizen
    {

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Nullable<System.DateTime> Date_of_Birth { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string ZipCode { get; set; }

        public List<Querie> queries { get; set; }

    }
}

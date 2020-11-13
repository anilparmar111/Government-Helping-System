using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models
{
    public class Querie
    {
        [Key]
        public string Id { get; set; }

        public string CitizenId { get; set; }
        public Citizen Citizen { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string title { get; set; }

        public string textfilepath { get; set; }

        public string Area { get; set; }

        public string zipcode { get; set; }

        public string status { get; set; }

    }
}

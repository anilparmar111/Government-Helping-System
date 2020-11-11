using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models.ViewsModel
{
    public class LoginViews
    {
        [Required(ErrorMessage = "UserId is Required")]
        public string uid { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string pswd { get; set; }

        public bool remember { get; set; }
    }
}

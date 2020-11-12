using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models.ViewsModel
{
    public class EmployeeViews
    {


        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Area/Village Name Is Required")]
        public string Area { get; set; }

        [Required(ErrorMessage = "ZipCode is Required")]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Invalid Zip")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Post Is Required")]
        public string post { get; set; }

    }
}

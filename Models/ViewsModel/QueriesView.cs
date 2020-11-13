using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models.ViewsModel
{
    public class QueriesView
    {
        [Required(ErrorMessage = "*please provide Title")]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required(ErrorMessage ="Description Is Required")]
        public string textfile { get; set; }

        [Required(ErrorMessage = "Area/Village Name Is Required")]
        public string Area { get; set; }

        [Required(ErrorMessage = "ZipCode is Required")]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Invalid Zip")]
        public string zipcode { get; set; }


        [Display(Name = "Select Proof Photo")]
        [Required]
        public IFormFileCollection photos { get; set; }
        public List<UploadPhotoModel> uploadphoto { get; set; }

    }
}

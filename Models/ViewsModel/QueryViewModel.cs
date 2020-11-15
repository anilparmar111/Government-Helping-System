using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models.ViewsModel
{
    public class QueryViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadTime { get; set; }
        public List<string> URLS { get; set; }
        public List<string> Names { get; set; }
    }
}

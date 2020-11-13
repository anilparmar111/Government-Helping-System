using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string QueriesId { get; set; }
        public Querie querie { get; set; }
    }
}

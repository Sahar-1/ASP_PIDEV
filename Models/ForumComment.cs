using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class ForumComment
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string PostedDate { get; set; }
    }
}
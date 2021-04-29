using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class ForumSubject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public DateTime PostedDate { get; set; }
        public int VoteCount { get; set; }
        public double Status { get; set; }
    }
}
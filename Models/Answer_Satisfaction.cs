using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Answer_Satisfaction
    {

        public int id { get; set; }
        public Review review { get; set; }
        public Question_Satisfaction question { get; set; }
    }
}
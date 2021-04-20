using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Satisfaction
    {

        public long id { get; set; }
        private String name;
        public DateTime Satisfaction_Date { get; set; }
        public List<Question_Satisfaction> questions { get; set; }

    }
}
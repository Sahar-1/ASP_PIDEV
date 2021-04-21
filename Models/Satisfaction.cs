using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Satisfaction
    {

        public long Id { get; set; }
        public string name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Satisfaction_Date { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Question_Satisfaction
    {
        public long id { get; set; }
        public string question_Sat { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateOfCreation { get; set; }
        public List<Satisfaction> satisfactions { get; set; }
        public HashSet<Answer_Satisfaction> answers { get; set; } 
         



    }
}
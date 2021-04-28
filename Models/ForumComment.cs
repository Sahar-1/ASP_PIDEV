using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class ForumComment
    {
        public int id { get; set; }

        [Required(ErrorMessage = "answer is Required")]
        public string answer { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime postedDate { get; set; }

        public string userId { get; set; }

        public string userFirstName { get; set; }
        public string userLastName { get; set; }

        public ForumSubject forumSubject { get; set; }
    }
}
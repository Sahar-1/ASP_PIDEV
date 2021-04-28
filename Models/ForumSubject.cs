using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class ForumSubject
    {
        public long id { get; set; }
        [Required(ErrorMessage = "Topic Title is Required")]
        public string title { get; set; }
        [Required(ErrorMessage = "Topic  is Required")]
        public string question { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime postedDate { get; set; }
        public int voteCount { get; set; }
        public float status { get; set; }
        public DBO_User user { get; set; }

        public Garden garden { get; set; }

        public ICollection<ForumComment> forumComments { get; set; }
    }
}
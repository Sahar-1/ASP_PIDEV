using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PiDevEsprit.Models
{
    public class Post
    {

        [JsonProperty(PropertyName = "id")]
        public long post_id { get; set; }
        [JsonProperty(PropertyName = "dateOfCreation")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MMM dd HH:mm:ss}")]
        public DateTime date_of_creation { get; set; }
        [Required(ErrorMessage = "A Post Subject is required")]
        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }
        [Required(ErrorMessage = "A Post Title is required")]
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [Required(ErrorMessage = "A Post Type is required")]
        [JsonProperty(PropertyName = "types")]
        public string types { get; set; }
        [JsonProperty(PropertyName = "signaler")]
        public int signaler { get; set; }

        // foreign Key properties 
        public virtual ICollection<Comment> comments { get; set; }
        public virtual ICollection<DBO_User> users { get; set; }
        public virtual ICollection<Reaction> reactions { get; set; }
    }
}
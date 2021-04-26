using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Comment
    {
        [JsonProperty(PropertyName = "id")]
        public long comment_id { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string text { get; set; }
        // foreign Key properties 
        [JsonProperty(PropertyName = "id_post")]
        public long? PostId { get; set; }
        public virtual Post MyPost { get; set; }
        // public int? PUserId { get; set; }
        //public virtual User MyUser { get; set; }
    }
}
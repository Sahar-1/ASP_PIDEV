using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Reaction
    {
        [JsonProperty(PropertyName = "id")]
        public long reactionId { get; set; }
        [JsonProperty(PropertyName = "liked")]
        public Boolean liked { get; set; }
        // public string types { get; set; }
        // foreign Key properties 
        [JsonProperty(PropertyName = "id_post")]
        public int? PostId { get; set; }
        public virtual Post MyPost { get; set; }
        // [JsonProperty(PropertyName = "id_post")]
        // public int? PUserId { get; set; }
        //public virtual User MyUser { get; set; }
    }
}
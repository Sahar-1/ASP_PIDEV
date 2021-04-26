using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{

    public class Message
    {
        // [JsonProperty("id")]

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        //   [JsonProperty("date")]
        [JsonProperty(PropertyName = "date")]
 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        //  [JsonProperty("content")]
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
       
        public Message()
        {
        }


    }
}
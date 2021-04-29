using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace PiDevEsprit.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Type
    {
        [Display(Name = "SPORT")]
        SPORT,
        [Display(Name = "SOCIAL")]
        SOCIAL,
        [Display(Name = "HEALTH")]
        HEALTH
    }
    public class DBO_Events
    {
        public long Id { get; set; }
        [Required(ErrorMessage = " Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Name is Required")]
        public string Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public  Type Type { get; set; }
        [Required(ErrorMessage = "Capacity Name is Required")]
        public int Capacity { get; set; }
        public bool HasFinished { get; set; }

        [Required(ErrorMessage = "date Name is Required")]
        [DataType(DataType.DateTime)]
        public long Start_date { get; set; }
       
        [Required(ErrorMessage = "date Name is Required")]
        public long End_date { get; set; }
        public virtual DBO_User EventCreator { get; set; }
        

        public string StartDateOffset => DateTimeOffset.FromUnixTimeMilliseconds(this.Start_date).Date.ToShortDateString(); 
        public string EndDateOffset => DateTimeOffset.FromUnixTimeMilliseconds(this.End_date).Date.ToShortDateString(); 

    }
   
}
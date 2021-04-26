using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PiDevEsprit.Models
{
   
   
    public class Bill
    {

        public long id { get; set; }

        [Required(ErrorMessage = "A bill Title is required")]
        [StringLength(160)]
        public string title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime dateStart { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime dateDeadline { get; set; }
        


        public string discription { get; set; }


        [Range(0.01, 100.00,
         ErrorMessage = "totalPrice must be between 0.01 and 100.00")]
        public double total { get; set; }


        [Range(0.01, 101.00,
         ErrorMessage = "amount Price must be between 0.01 and 101.00")]
        public double amount { get; set; }


        public float discount { get; set; }


        public DBO_User user { get; set; }

        public Garden garden { get; set; }






    }
}

      
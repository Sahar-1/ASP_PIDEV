using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

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


        [Required(ErrorMessage = "discription is Required")]
        public string discription { get; set; }

        [Required(ErrorMessage = "total is Required")]
        [Range(0.01, 100.00,
         ErrorMessage = "totalPrice must be between 0.01 and 100.00")]
        public double total { get; set; }

        [Required(ErrorMessage = "amount is Required")]
        [Range(0.01, 101.00,
         ErrorMessage = "amount Price must be between 0.01 and 101.00")] 
        public double amount { get; set; }

        [Required(ErrorMessage = "discount is Required")]
        public float discount { get; set; }


        public DBO_User user { get; set; }

        public Garden garden { get; set; }
        public string gardenName { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }






    }
}

      
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Web;

namespace PiDevEsprit.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthUserProvider {
        [Display(Name = "LOCAL")]
        LOCAL,
        [Display(Name = "GOOGLE")]
        GOOGLE,
        [Display(Name = "FACEBOOK")]
        FACEBOOK
    }
    public class DBO_User
    {

        public long Id { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "adresse email non valide")]
        public string Email { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        public bool Actif { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool AccountNonLocked { get; set; }
        public int FailedAttempt { get; set; }
        public DateTime? Date { get; set; }
        public long? CreatedTime { get; set; }
        public long? LastLoggedIn { get; set; }
        public long? LastLoggedOut { get; set; }
        public long? LockTime { get; set; }

        
        public string created => CreatedTime.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(this.CreatedTime.Value).DateTime.ToString() : "N/A";
        public string lastlog => LastLoggedIn.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(this.LastLoggedIn.Value).DateTime.ToString() : "N/A";
        public string lastlogout => LastLoggedOut.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(this.LastLoggedOut.Value).DateTime.ToString() : "N/A";
        public string locktime => LockTime.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(this.LockTime.Value).DateTime.ToString() : "N/A";


        [JsonConverter(typeof(StringEnumConverter))]
        public AuthUserProvider dbo_User_Provider { get; set; }

        public DBO_User(string email , string pass)
        {
            this.Email = email;
            this.Password = pass;
        }
        public DBO_User()
        {

        }





    }
}
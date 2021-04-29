using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Garden
    {









		public long id { get; set; }

		public string description { get; set; }
		public string location { get; set; }
		public int phone { get; set; }
		public string email { get; set; }
		public string name { get; set; }
		public double price { get; set; }
		/*-------------------------------association Garden et user--------------------------------------------------*/

		//	public virtual ICollection<Dbo_User> users { get; set; }
		/*-------------------------------association Garden et user--------------------------------------------------*/




		/*-------------------------------association Garden et classe--------------------------------------------------
		public virtual ICollection<Classe> classes { get; set; }

		/*-------------------------------association Garden et classe--------------------------------------------------*/




		/*-------------------------------association Garden et appointment--------------------------------------------------
		public virtual ICollection<Appointment> appointments { get; set; }

		/*-------------------------------association Garden et appointment--------------------------------------------------*/






		/*-------------------------------association Activity et Garden--------------------------------------------------
		public virtual ICollection<Activity> activities { get; set; }

		/*-------------------------------association Activity et Garden--------------------------------------------------*/





		/*-------------------------------association trajet et garden--------------------------------------------------
		public virtual ICollection<Trajet> trajets { get; set; }

		/*-------------------------------association trajet et garden--------------------------------------------------*/




		/*-------------------------------association question et garden--------------------------------------------------
		public virtual ICollection<Question_appointment> questions { get; set; }

		/*-------------------------------association question et garden--------------------------------------------------*/





		/*-------------------------------association bus et garden--------------------------------------------------
		public virtual ICollection<Bus> bus { get; set; }

		/*-------------------------------association bus et garden--------------------------------------------------*/

	}
}
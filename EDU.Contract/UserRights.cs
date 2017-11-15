using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
	public class UserRights: IContract
	{
		// Constructor 
		public UserRights() { }

		// Public Members 

		public string  UserID { get; set; }

		public string  SecurableItem { get; set; }

		public string  Permission { get; set; }

		public string  CreatedBy { get; set; }

		public DateTime  CreatedOn { get; set; }

		public string  ModifiedBy { get; set; }

		public DateTime  ModifiedOn { get; set; }


	}
}

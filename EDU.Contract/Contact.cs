using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
	public class Contact: IContract
	{
		// Constructor 
		public Contact() { }

		// Public Members 

		public Int64  ContactID { get; set; }

		public string AddressLinkID { get; set; }

		public string  EntityType { get; set; }

		public string Address1 { get; set; }

		public string Address2 { get; set; }

		public string State { get; set; }

		public string  CountryCode { get; set; }

		public string  PostCode { get; set; }

		public string  PhoneNumber { get; set; }

		public string  FaxNumber { get; set; }

		public string ContactPerson { get; set; }

		public string  MobilePhoneNumber { get; set; }

		public string  EmailID { get; set; }

		public bool  IsDefault { get; set; }

		public bool  Status { get; set; }


	}
}




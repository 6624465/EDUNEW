using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
	public class Customer: IContract
	{
		// Constructor 
		public Customer() { }

        // Public Members 

        public Int16 BranchID { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public Int16 CustomerType { get; set; }

        public string CustomerTypeDescription { get; set; }


        public bool Status { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }

        public string CountryCode { get; set; }

        public string PostCode { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string ContactPerson { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string EmailID { get; set; }
        
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }


	}
}




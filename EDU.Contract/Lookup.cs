using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
	public class Lookup: IContract
	{
		// Constructor 
		public Lookup() { }

		// Public Members 

		public Int16  LookupID { get; set; }

		public string  LookupCode { get; set; }

		public string  LookupDescription { get; set; }

		public string  LookupCategory { get; set; }

		public bool  Status { get; set; }

		public string  ISOCode { get; set; }

		public string  MappingCode { get; set; }

		public string  CreatedBy { get; set; }

		public DateTime  CreatedOn { get; set; }

		public string  ModifiedBy { get; set; }

		public DateTime  ModifiedOn { get; set; }


	}
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
	public class Site: IContract
	{
		// Constructor 
		public Site() { }

		// Public Members 

		public string  SitePrefix { get; set; }

		public string  SiteCode { get; set; }

		public string  SiteName { get; set; }

		public bool  Status { get; set; }

		public string  CreatedBy { get; set; }

		public DateTime  CreatedOn { get; set; }

		public string  ModifiedBy { get; set; }

		public DateTime  ModifiedOn { get; set; }


	}
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.Web;
using EZY.EDU.Contract;

namespace EZY.EDU.Contract
{
	public class Country: IContract
	{
		// Constructor 
		public Country() { }

		// Public Members 

		[DisplayName("CountryCode")] 
		public string  CountryCode { get; set; }

		[DisplayName("CountryName")] 
		public string CountryName { get; set; }

		[DisplayName("Description")] 
		public string Description { get; set; }


	}
}




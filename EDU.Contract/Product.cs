using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
	public class Product: IContract
	{
		// Constructor 
		public Product() { }

		// Public Members 

		public string  ProductCode { get; set; }

        public string ModelNo { get; set; }

		public string  Description { get; set; }

		public bool  Status { get; set; }

		public string  CreatedBy { get; set; }

		public DateTime  CreatedOn { get; set; }

		public string  ModifiedBy { get; set; }

		public DateTime  ModifiedOn { get; set; }

        public Int16 ProductCategory { get; set; }

        public string ProductCategoryDescription { get; set; }



    }
}




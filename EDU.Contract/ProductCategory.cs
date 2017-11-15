using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
    public class ProductCategoryMaster : IContract
    {
        // Constructor 
        public ProductCategoryMaster() { }

		// Public Members 

		public Int16 ProductCategory { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public Int16 CategoryGroup { get; set; }

        public string CategoryGroupDescripton { get; set; }


    }
}




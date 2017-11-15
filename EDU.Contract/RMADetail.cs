using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class RMADetail : IContract
    {
        // Constructor 
        public RMADetail() { }

		// Public Members 

		public string DocumentNo { get; set; }

        public string SerialNo { get; set; }

        public DateTime WarrantyExpiryDate { get; set; }

        public bool IsValid { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public decimal Cost { get; set; }


    }
}

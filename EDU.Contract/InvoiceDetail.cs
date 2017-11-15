using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
	public class InvoiceDetail: IContract
	{
		// Constructor 
		public InvoiceDetail() { }

        // Public Members 
        public string DocumentNo { get; set; }
        public string InvoiceNo { get; set; }

        public string ModelNo { get; set; }

        public string SerialNo { get; set; }

        public string Make { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }


	}
}




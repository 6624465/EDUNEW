using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class RMAOutwardHeader : IContract
    {
        // Constructor 
        public RMAOutwardHeader() { }

		// Public Members 

		public Int16 BranchID { get; set; }

        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        public string ReferenceNo { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<RMAOutwardDetail> RmaDetails { get; set; }





    }
}

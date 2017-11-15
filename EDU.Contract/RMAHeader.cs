using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace EZY.EDU.Contract
{
    public class RMAHeader : IContract
    {
        // Constructor 
        public RMAHeader() { }

        // Public Members 
     //   public int BranchID { get; set; }
        public string DocumentNo { get; set; }

        public string CountryCode { get; set; }

        public string EmailID { get; set; }

        public string ContactNo { get; set; }
        public DateTime IncidentDate { get; set; }

        public string CustomerAddress { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<RMADetail> rmaDetails { get; set; }

        public bool IsCompleted { get; set; }

        public string CompanyName { get; set; }
    }
}

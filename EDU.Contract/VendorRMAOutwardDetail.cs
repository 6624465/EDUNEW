using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class VendorRMAOutwardDetail:IContract
    {
        public VendorRMAOutwardDetail() { }

        public Int16 BranchID { get; set; }

        public string DocumentNo { get; set; }

        public string OldSerialNo { get; set; }

        public string ProductCode { get; set; }

        public string ReplacementProductCode { get; set; }

        public string NewSerialNo { get; set; }

        public bool IsCreditNote { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ProductDescription { get; set; }

        public decimal Cost { get; set; }
    }
}

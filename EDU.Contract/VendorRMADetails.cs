using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace EZY.EDU.Contract
{
   public class VendorRMADetails : IContract
    {
            public VendorRMADetails() { }

        // Public Members 

        public string DocumentNo { get; set; }

        public string VendorInvoiceNo { get; set; }

        public string VendorName { get; set; }
 
        public string SerialNo { get; set; }

        public DateTime WarrantyExpiryDate { get; set; }

        public bool IsValid { get; set; }

        public bool RMAIsValid { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime? VendorWarrantyExpiryDate { get; set; }
    }
}

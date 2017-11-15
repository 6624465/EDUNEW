using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class MaterialsEnquiryDetailsDTO : IContract
    {
        public string SerialNo { get; set; }
        public DateTime? VendorExpiryDate { get; set; }
        public DateTime? CustomerExpiryDate { get; set; }
        public bool VendorIsValid { get; set; }
        public bool CustomerIsvalid { get; set; }
    }
}

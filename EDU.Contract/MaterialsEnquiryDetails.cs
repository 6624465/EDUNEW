using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class MaterialsEnquiryDetails : IContract
    {
        public MaterialsEnquiryDetails()
        {

        }
        public string SerialNo { get; set; }
        public DateTime? VendorExpiryDate { get; set; }
        public DateTime? CustomerExpiryDate { get; set; }
    }
}

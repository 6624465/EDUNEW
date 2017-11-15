using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
  public  class VendorRMAInwardDTO
    {
        public string DocumentNo { get; set; }

        public string VendorInvoiceNo { get; set; }

        public string VendorName { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string ProductCode { get; set; }

        public string Description { get; set; }

        public int WarrantyPeriod { get; set; }

        public decimal UnitPrice { get; set; }

        public string ModelNo { get; set; }

        public string SerialNo { get; set; }

        public DateTime? VendorWarrantyExpiryDate { get; set; }

        public DateTime CustomerWarrantyExpiryDate { get; set; }
    }
}

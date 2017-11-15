using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class RMAInwardDTO
    {
        public string DocumentNo { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string ProductCode { get; set; }

        public string Description { get; set; }

        public int WarrantyPeriod { get; set; }

        public decimal UnitPrice { get; set; }

        public string ModelNo { get; set; }

        public string SerialNo { get; set; }
    }
}

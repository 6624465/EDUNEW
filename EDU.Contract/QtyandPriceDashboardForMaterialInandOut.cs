using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
public  class QtyandPriceDashboardForMaterialInandOut:IContract
    {
        public QtyandPriceDashboardForMaterialInandOut()
        {

        }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public int MonthNo { get; set; }
        public string InvoiceMonth { get; set; }
        public int InvoiceYear { get; set; }
    }
}

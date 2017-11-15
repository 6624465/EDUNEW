using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
public class MaterialOutwardDashboardCount:IContract
    {
        public MaterialOutwardDashboardCount() { }

        public int InvoiceNoCount { get; set; }
        public int MonthNo { get; set; }
        public string InvoiceMonth { get; set; }
        public int InvoiceYear { get; set; }
    }
}

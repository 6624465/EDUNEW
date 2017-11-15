using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
   public class ProductandPriceDashboardForRMAInward:IContract
    {
        public ProductandPriceDashboardForRMAInward()
        {

        }
        public int ProductCode { get; set; }
        public decimal Cost { get; set; }
        public int MonthNo { get; set; }
        public string IncidentMonth { get; set; }
        public int IncidentYear { get; set; }
    }
}

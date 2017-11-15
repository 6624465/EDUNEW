using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
   public class QtyandPriceDashboardForRMAInward:IContract
    {
        public QtyandPriceDashboardForRMAInward()
        {

        }

        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public int MonthNo { get; set; }
        public string IncidentMonth { get; set; }
        public int IncidentYear { get; set; }
    }
}

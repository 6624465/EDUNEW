using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
public  class DashboardForQtyandPriceDTO:IContract
    {
        public DashboardForQtyandPriceDTO()
        {

        }
        public List<QtyandPriceDashboardForMaterialInandOut> QtyandPriceDashboardForMaterialIn { get; set; }
        public List<QtyandPriceDashboardForMaterialInandOut> QtyandPriceDashboardForMaterialOut { get; set; }
        public List<QtyandPriceDashboardForRMAInward> QtyandPriceDashboardForRMAInward { get; set; }

        public List<QtyandPriceDashboardForRMAOutward> QtyandPriceDashboardForRMAOutward { get; set; }
    }
}

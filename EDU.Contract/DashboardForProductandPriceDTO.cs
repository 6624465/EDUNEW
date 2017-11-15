using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class DashboardForProductandPriceDTO : IContract
    {
        public DashboardForProductandPriceDTO()
        {

        }
        public List<ProductandPriceDashboardForMaterialInandOut> ProductandPriceDashboardForMaterialIn { get; set; }

        public List<ProductandPriceDashboardForMaterialInandOut> ProductandPriceDashboardForMaterialOut { get; set; }

        public List<ProductandPriceDashboardForRMAInward> ProductandPriceDashboardForRMAInward { get; set; }

        public List<ProductandPriceDashboardForRMAOutward> ProductandPriceDashboardForRMAOutward { get; set; }
    }
}

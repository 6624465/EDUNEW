using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class DashBoardForTotalCountofProduct : IContract
    {
        public DashBoardForTotalCountofProduct()
        {

        }
        public List<DashboardForTotalListProductwise> DashboardForTotalListProductwise { get; set; }

        public List<DashBoardForProductListCountMonthWise> DashBoardForProductListCountMonthWise { get; set; }

        public List<DashBoard4> data { get; set; }

        public int TotalproductCount { get; set; }

        public List<DashboardForTotalProductsMonthWiseReport> DashboardForTotalProductsMonthWiseReport { get; set; }
    }
    public class DashBoard4
    {
        public string name { get; set; }
        public string id { get; set; }

        public Dictionary<string, int> data { get; set; }
    }

    public class DashBoard4Data
    {
        public string InvoiceMonth { get; set; }
        public int ProductCode { get; set; }
    }
}

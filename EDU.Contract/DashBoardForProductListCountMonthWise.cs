using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
  public  class DashBoardForProductListCountMonthWise:IContract
    {
        public DashBoardForProductListCountMonthWise()
        {

        }

        public string CategoryDescription { get; set; }

        public int ProductCode { get; set; }
        public int MonthNo { get; set; }
        public string InvoiceMonth { get; set; }
        public int InvoiceYear { get; set; }
    }
}

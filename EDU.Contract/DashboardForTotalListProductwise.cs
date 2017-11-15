using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
   public class DashboardForTotalListProductwise:IContract
    {
        public DashboardForTotalListProductwise()
        {

        }
        public string CategoryDescription { get; set; }
        public int ProductCodeCount { get; set; }
    }
}

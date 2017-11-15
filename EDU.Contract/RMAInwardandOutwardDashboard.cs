using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
public class RMAInwardandOutwardDashboard:IContract
    {
        public RMAInwardandOutwardDashboard()
        {

        }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int RMAInwardCount { get; set; }
        public int RMAOutwardCount { get; set; }
    }
}

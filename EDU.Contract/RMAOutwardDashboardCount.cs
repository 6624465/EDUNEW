using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
public class RMAOutwardDashboardCount:IContract
    {
        public RMAOutwardDashboardCount() { }
        public int DocumentNoCount { get; set; }
        public int MonthNo { get; set; }
        public String DocumentMonth { get; set; }
        public int DocumentYear { get; set; }
    }
}

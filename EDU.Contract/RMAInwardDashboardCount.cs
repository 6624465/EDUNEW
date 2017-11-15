using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
  public  class RMAInwardDashboardCount:IContract
    {
        public RMAInwardDashboardCount() { }
        public int DocumentNoCount { get; set; }
        public int MonthNo { get; set; }
        public string IncidentMonth { get; set; }
        public int IncidentYear { get; set; }
    }
}

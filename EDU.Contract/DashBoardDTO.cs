using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
   public class DashBoardDTO : IContract
    {
        public DashBoardDTO() { }

        public List<MaterialInwardDashboardCount> InwardCount { get; set; }
        public List<MaterialOutwardDashboardCount> OutwardCount { get; set; }
        public List<RMAInwardDashboardCount> RMAInwardCount { get; set; }
        public List<RMAOutwardDashboardCount> RMAOutwardCount { get; set; }
        public List<MaterialInandOutCountList> InwardAndOutward { get; set; }
        public List<RMAInwardandOutwardDashboard> RMAInwardAndOutward { get; set; }
    }
}

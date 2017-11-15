using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
  public  class DashboardInandOutDTO:IContract
    {
        public DashboardInandOutDTO()
        {
        }
       public List<MaterialInandOutCountList> InwardAndOutward { get; set; }

       public List<RMAInwardandOutwardDashboard> RMAInwardAndOutward { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
   public class ReportParams:IContract
    {
        public ReportParams()
        {

        }

        public int BranchID { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}

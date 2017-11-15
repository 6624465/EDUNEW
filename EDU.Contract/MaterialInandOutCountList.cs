using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
public class MaterialInandOutCountList:IContract
    {
        public MaterialInandOutCountList()
        {
                
        }

        public int Month { get; set; }
        public string MonthName { get; set; }
        public int InwardCount { get; set; }
        public int OutwardCount { get; set; }
    }
}

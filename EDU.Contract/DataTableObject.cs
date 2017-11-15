using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class DataTableObject
    {
        public int limit { get; set; }

        public int offset { get; set; }

        public string sortColumn { get; set; }

        public string sortType { get; set; }

        public Int16 BranchID { get; set; }
    }
}

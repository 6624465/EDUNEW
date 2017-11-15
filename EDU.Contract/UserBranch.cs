using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class UserBranch : IContract
    {
        public UserBranch() {}

        public string UserID { get; set; }

        public Int16 BranchID { get; set; }

        public string CompanyCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

    }

    public class UserBranchSelectedList
    {
        public string UserID { get; set; }

        public Int16 BranchID { get; set; }

        public string BranchName { get; set; }

        public bool IsSelected { get; set; }
    }
}

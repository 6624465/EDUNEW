using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class EduProduct : IContract
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}

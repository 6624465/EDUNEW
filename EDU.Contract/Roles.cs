using System;
using System.ComponentModel;

namespace EZY.EDU.Contract
{
    public class Roles : IContract
    {
        // Constructor 
        public Roles() { }

        // Public Members 

        [DisplayName("RoleCode")]
        public string RoleCode { get; set; }

        [DisplayName("RoleDescription")]
        public string RoleDescription { get; set; }

        [DisplayName("IsActive")]
        public bool IsActive { get; set; }

        [DisplayName("CreatedBy")]
        public string CreatedBy { get; set; }

        [DisplayName("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("ModifiedBy")]
        public string ModifiedBy { get; set; }

        [DisplayName("ModifiedOn")]
        public DateTime ModifiedOn { get; set; }


    }
}

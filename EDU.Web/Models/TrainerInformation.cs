//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDU.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrainerInformation
    {
        public int TrianerId { get; set; }
        public string Technology { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
        public string Profile { get; set; }
        public System.Web.HttpPostedFileBase FileName { get; set; }
        public Nullable<decimal> TrainerRate { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public string TrainerName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}

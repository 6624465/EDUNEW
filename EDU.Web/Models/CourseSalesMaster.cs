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
    
    public partial class CourseSalesMaster
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public int Product { get; set; }
        public int Course { get; set; }
        public short NoOfDays { get; set; }
        public Nullable<short> Month { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public short MinimumReg { get; set; }
        public short LeadsOnHead { get; set; }
        public short Registered { get; set; }
        public short AvailableSeats { get; set; }
        public System.DateTime RegClosingDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string LeadsOnHeadRemarks { get; set; }
        public string RegisteredRemarks { get; set; }
        public Nullable<bool> TOD { get; set; }
        public Nullable<bool> LVC { get; set; }
        public Nullable<bool> IsConfirm { get; set; }
        public Nullable<bool> ILT { get; set; }
    }
}

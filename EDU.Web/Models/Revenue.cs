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
    
    public partial class Revenue
    {
        public int RevenueId { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
        public int Product { get; set; }
        public string ProductName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyExRate { get; set; }
        public int Year { get; set; }
        public decimal YearlyTarget { get; set; }
        public decimal HalfYearlyTarget { get; set; }
        public decimal QuarterlyTarget { get; set; }
        public decimal MonthlyTarget { get; set; }
        public decimal YearlyTargetAmt { get; set; }
        public decimal HalfYearlyTargetAmt { get; set; }
        public decimal QuarterlyTargetAmt { get; set; }
        public decimal MonthlyTargetAmt { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}

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
    
    public partial class FinancialTransactionDetail
    {
        public int FinancialTransactionId { get; set; }
        public string TrainingConfirmationID { get; set; }
        public Nullable<short> DescriptionId { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> LocalAmount { get; set; }
        public string ReferenceDoc { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public System.Web.HttpPostedFileBase FileName { get; set; }
    }
}

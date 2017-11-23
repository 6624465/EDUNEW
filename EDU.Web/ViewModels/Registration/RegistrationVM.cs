using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.Registration
{
    public class RegistrationVM
    {
        public int RegistrationId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<short> WHTPercent { get; set; }
        public Nullable<short> VATPercent { get; set; }
        public Nullable<decimal> WHTAmount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> OtherDeductionsAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> Payment1 { get; set; }
        public Nullable<decimal> Payment2 { get; set; }
        public Nullable<decimal> Payment3 { get; set; }
        public Nullable<System.DateTime> Payment1Date { get; set; }
        public Nullable<System.DateTime> Payment2Date { get; set; }
        public Nullable<System.DateTime> Payment3Date { get; set; }
        public Nullable<short> Payment1Type { get; set; }
        public Nullable<short> Payment2Type { get; set; }
        public Nullable<short> Payment3Type { get; set; }
        public string CheckNo { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public string TrainingConfirmationID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }


        public string ProductName { get; set; }
        public string CourseName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string TrainerName { get; set; }
    }
}
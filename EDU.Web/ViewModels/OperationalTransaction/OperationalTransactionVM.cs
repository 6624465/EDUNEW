using EDU.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.OperationalTransactionModel
{
    public class OperationalTransactionVM
    {
        public int OperationalTransactionId { get; set; }
        public int CategoryId { get; set; }
        public int ParticularsId { get; set; }
        public Nullable<short> Month { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CategoryIdDesc { get; set; }
        public string ParticularsIdDesc { get; set; }
        public string MonthDesc { get; set; }
    }
}
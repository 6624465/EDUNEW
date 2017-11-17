using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.CustomerPayments
{
    public class CustomerPaymentVM
    {

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string OrderId { get; set; }
        public string CourseName { get; set; }
        public string InvoiceValue { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public string OtherRecievables { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> RecieptDate { get; set; }
        public HttpPostedFileBase ReferenceDocument { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

}
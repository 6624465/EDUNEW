﻿using EDU.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.CustomerPaymentStatusModel
{
    public class CustomerPaymentStatusVM
    {
        public List<CustomerPaymentVM> customerPayment { get; set; }
        public List<TrainingConfirmation> trainingconfList { get; set; }
        public List<EZY.EDU.Contract.CourseVm> courseList { get; set; }
        public List<EZY.EDU.Contract.EduProduct> productList { get; set; }
        public List<EDU.Web.Models.Registration> registrationList { get; set; }
    }

    public class CustomerPaymentVM
    {
        public int CustomerPaymentId { get; set; }
        public int RegistrationId { get; set; }
        public string TrainingConfirmationID { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public Nullable<decimal> OtherReceivablesAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public string ReferenceDoc { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CustomerName { get; set; }
        public string CourseName { get; set; }
        public string ProductName { get; set; }
        public string TrianerName { get; set; }
        public HttpPostedFileBase FileName { get; set; }
        public int? Year { get; set; }
        public Nullable<short> Month { get; set; }
    }
    //public class TrainingConfirmDtl
    //{
    //    public int Id { get; set; }
    //    public string TrainingConfirmationID { get; set; }
    //    public int Product { get; set; }
    //    public int Course { get; set; }

    //    public string ProductName { get; set; }
    //    public string CourseName { get; set; }
    //    public string TrianerName { get; set; }
    //}
}
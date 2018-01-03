using EDU.Web.Models;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.FinancialTransactionModel
{
    public class FinancialTransactionVM
    {
        public List<FinancialTransactionsVM> financialTransaction { get; set; }
    }

    public class FinancialTransactionsVM
    {
        public int FinancialTransactionId { get; set; }
        public string TrainingConfirmationID { get; set; }
        public Nullable<short> Country { get; set; }
        public string CountryName { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<decimal> CurrencyExRate { get; set; }
        public Nullable<decimal> TotalRevenueAmount { get; set; }
        public Nullable<decimal> TotalRevenueLocalAmount { get; set; }
        public string TotalRevenueReferenceDoc { get; set; }
        public string TotalRevenueRemarks { get; set; }
        public Nullable<decimal> TrainerExpensesAmount { get; set; }
        public Nullable<decimal> TrainerExpensesLocalAmount { get; set; }
        public string TrainerExpensesReferenceDoc { get; set; }
        public string TrainerExpensesRemarks { get; set; }
        public Nullable<decimal> TrainerTravelExpensesAmount { get; set; }
        public Nullable<decimal> TrainerTravelExpensesLocalAmount { get; set; }
        public string TrainerTravelExpensesReferenceDoc { get; set; }
        public string TrainerTravelExpensesRemarks { get; set; }
        public Nullable<decimal> LocalExpensesAmount { get; set; }
        public Nullable<decimal> LocalExpensesLocalAmount { get; set; }
        public string LocalExpensesReferenceDoc { get; set; }
        public string LocalExpensesRemarks { get; set; }
        public Nullable<decimal> CoursewareMaterialAmount { get; set; }
        public Nullable<decimal> CoursewareMaterialLocalAmount { get; set; }
        public string CoursewareMaterialReferenceDoc { get; set; }
        public string CoursewareMaterialRemarks { get; set; }
        public Nullable<decimal> MiscExpensesAmount { get; set; }
        public Nullable<decimal> MiscExpensesLocalAmount { get; set; }
        public string MiscExpensesReferenceDoc { get; set; }
        public string MiscExpensesRemarks { get; set; }

        public Nullable<decimal> TrainerClaimsAmount { get; set; }
        public Nullable<decimal> TrainerClaimsLocalAmount { get; set; }
        public string TrainerClaimsReferenceDoc { get; set; }
        public string TrainerClaimsRemarks { get; set; }
        public Nullable<decimal> GrossProfit { get; set; }
        public Nullable<decimal> ProfitAndLossPercent { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsSubmit { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public System.Web.HttpPostedFileBase TotalRevenueFileName { get; set; }
        public System.Web.HttpPostedFileBase TrainerExpensesFileName { get; set; }
        public System.Web.HttpPostedFileBase TrainerTravelExpensesFileName { get; set; }
        public System.Web.HttpPostedFileBase LocalExpensesFileName { get; set; }
        public System.Web.HttpPostedFileBase CoursewareMaterialFileName { get; set; }
        public System.Web.HttpPostedFileBase MiscExpensesFileName { get; set; }
        public System.Web.HttpPostedFileBase TrainerClaimsFileName { get; set; }
        public int? Year { get; set; }
        public Nullable<short> Month { get; set; }
    }
    
}
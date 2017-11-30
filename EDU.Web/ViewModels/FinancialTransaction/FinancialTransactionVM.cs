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
        public FinancialTransaction financialTransaction { get; set; }
        public List<FinancialTransactionDetailVM> financialTransactionDetailList { get; set; }
        public List<Branch> countryList { get; set; }
        public List<EDU.Web.Models.Lookup> currencyList { get; set; }
        public List<TrainingConfirmation> trainingConfirmationList { get; set; }

    }

    public class FinancialTransactionDetailVM
    {
        public int FinancialTransactionDetailId { get; set; }
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

    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }
    }
}
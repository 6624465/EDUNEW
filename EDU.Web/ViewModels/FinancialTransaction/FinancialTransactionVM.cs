using EDU.Web.Models;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.FinancialTransactionModel
{
    public class FinancialTransactionVM
    {
        public FinancialTransaction financialTransaction { get; set; }
        public List<FinancialTransactionDetail> financialTransactionDetailList { get; set; }
        public List<Branch> countryList { get; set; }
        public List<EDU.Web.Models.Lookup> currencyList { get; set; }
        public List<TrainingConfirmation> trainingConfirmationList { get; set; }

    }
}
using EDU.Web.Models;
using EDU.Web.ViewModels.FinancialTransactionModel;
using EZY.EDU.BusinessFactory;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class FinancialTransactionController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();

        [HttpGet]
        public ActionResult FinancialTransactionList(string trainingConfirmationID)
        {
            FinancialTransactionVM financialTransactionVM = new FinancialTransactionVM();
            FinancialTransaction financialTransaction = new FinancialTransaction();
            List<FinancialTransactionDetail> financialTransactionDetails = new List<FinancialTransactionDetail>();

            financialTransaction = dbContext.FinancialTransactions.Where(x => x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID).FirstOrDefault();

            if (financialTransaction == null)
            {
                financialTransaction = new FinancialTransaction();
                financialTransaction.FinancialTransactionId = -1;
                financialTransaction.TrainingConfirmationID = trainingConfirmationID;
            }

            financialTransactionDetails = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == financialTransaction.FinancialTransactionId && x.TrainingConfirmationID == financialTransaction.TrainingConfirmationID).ToList();

            financialTransactionVM.financialTransaction = financialTransaction;
            financialTransactionVM.financialTransactionDetailList = financialTransactionDetails;
            financialTransactionVM.currencyList = dbContext.Lookups.Where(x => x.LookupCategory == "Currency").ToList();
            financialTransactionVM.countryList = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();
            financialTransactionVM.trainingConfirmationList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();


            var FinancialTransactionLookupList = dbContext.Lookups.Where(x => x.LookupCategory == "FinancialTransaction").ToList();


            foreach (EDU.Web.Models.Lookup item in FinancialTransactionLookupList)
            {
                if (financialTransactionVM.financialTransactionDetailList.Where(x => x.DescriptionId == item.LookupID).Count() == 0)
                    financialTransactionVM.financialTransactionDetailList.Add(new FinancialTransactionDetail() { DescriptionId = item.LookupID, Description=item.LookupDescription, FinancialTransactionId = financialTransaction.FinancialTransactionId, TrainingConfirmationID = financialTransaction.TrainingConfirmationID });
            }

            return View(financialTransactionVM);
        }
    }
}
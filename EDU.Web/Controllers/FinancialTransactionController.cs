using EDU.Web.Models;
using EDU.Web.ViewModels.FinancialTransactionModel;
using EZY.EDU.BusinessFactory;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    financialTransactionVM.financialTransactionDetailList.Add(new FinancialTransactionDetail() { DescriptionId = item.LookupID, Description = item.LookupDescription, FinancialTransactionId = financialTransaction.FinancialTransactionId, TrainingConfirmationID = financialTransaction.TrainingConfirmationID });
            }

            return View(financialTransactionVM);
        }

        [HttpGet]
        public PartialViewResult FinancialTransactionDetail(int financialTransactionId, string trainingConfirmationID, short? descriptionId, string description, decimal? currencyExRate)
        {
            ViewData["CurrencyExRate"] = currencyExRate;
            if (financialTransactionId == -1)
            {
                ViewBag.Title = "New Financial Transaction";
                return PartialView(new FinancialTransactionDetail { FinancialTransactionId = -1, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description });
            }
            else
            {
                if (dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == financialTransactionId && x.TrainingConfirmationID == trainingConfirmationID && x.DescriptionId == descriptionId).Count() > 0)
                {
                    ViewBag.Title = "Update Financial Transaction";
                    return PartialView(dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == financialTransactionId && x.TrainingConfirmationID == trainingConfirmationID && x.DescriptionId == descriptionId));
                }
                else
                {
                    ViewBag.Title = "New Financial Transaction";
                    return PartialView(new FinancialTransactionDetail { FinancialTransactionId = financialTransactionId, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description });
                }
            }
        }

        [HttpPost]
        public JsonResult SaveFinancialTransaction(FinancialTransactionVM ftinfo, FinancialTransactionDetail dtl)
        {
            try
            {
                if (ftinfo.financialTransaction.FinancialTransactionId == -1)
                {
                    ftinfo.financialTransaction.CreatedBy = USER_ID;
                    ftinfo.financialTransaction.CreatedOn = UTILITY.SINGAPORETIME;
                    ftinfo.financialTransaction.IsActive = true;
                    dbContext.FinancialTransactions.Add(ftinfo.financialTransaction);


                }
                else
                {
                    FinancialTransactionVM financialTransactionVM = new FinancialTransactionVM();

                    financialTransactionVM.financialTransaction = dbContext.FinancialTransactions.
                       Where(x => x.FinancialTransactionId == ftinfo.financialTransaction.FinancialTransactionId).FirstOrDefault();

                    financialTransactionVM.financialTransaction.Country = ftinfo.financialTransaction.Country;
                    financialTransactionVM.financialTransaction.CurrencyCode = ftinfo.financialTransaction.CurrencyCode;
                    financialTransactionVM.financialTransaction.CurrencyExRate = ftinfo.financialTransaction.CurrencyExRate;
                    financialTransactionVM.financialTransaction.GrossProfit = ftinfo.financialTransaction.GrossProfit;
                    financialTransactionVM.financialTransaction.ProfitAndLossPercent = ftinfo.financialTransaction.ProfitAndLossPercent;
                    financialTransactionVM.financialTransaction.IsActive = true;

                    financialTransactionVM.financialTransaction.ModifiedBy = USER_ID;
                    financialTransactionVM.financialTransaction.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(financialTransactionVM.financialTransaction).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string message = "Saved Successfully.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCurrency(Int16 Id)
        {
            var country = new BranchBO().GetList().Where(x => x.IsActive == true && x.BranchID == Id).FirstOrDefault();
            var currencyCode = dbContext.Lookups.Where(x => x.LookupCategory == "Currency" && x.MappingCode == country.BranchCode).FirstOrDefault();

            return Json(currencyCode == null ? "" : currencyCode.LookupCode, JsonRequestBehavior.AllowGet);

        }
    }
}
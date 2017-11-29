using EDU.Web.Models;
using EDU.Web.ViewModels.FinancialTransactionModel;
using EZY.EDU.BusinessFactory;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == trainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);

            foreach (EDU.Web.Models.Lookup item in FinancialTransactionLookupList)
            {
                if (financialTransactionVM.financialTransactionDetailList.Where(x => x.DescriptionId == item.LookupID).Count() == 0)
                    financialTransactionVM.financialTransactionDetailList.Add(new FinancialTransactionDetail() { DescriptionId = item.LookupID, Description = item.LookupDescription, FinancialTransactionId = financialTransaction.FinancialTransactionId, TrainingConfirmationID = financialTransaction.TrainingConfirmationID, FinancialTransactionDetailId = -1, Amount = (item.LookupID == 1050 ? TotalAmount : null) });
            }
            financialTransactionVM.financialTransactionDetailList = financialTransactionVM.financialTransactionDetailList.OrderBy(x => x.DescriptionId).ToList();
            return View(financialTransactionVM);
        }

        [HttpGet]
        public PartialViewResult FinancialTransactionDetail(int financialTransactionId, string trainingConfirmationID, short? descriptionId, string description, decimal? currencyExRate, int financialTransactionDetailId)
        {
            ViewData["CurrencyExRate"] = currencyExRate;
            decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == trainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);

            if (financialTransactionId == -1)
            {
                ViewBag.Title = "New Financial Transaction";
                return PartialView(new FinancialTransactionDetail { FinancialTransactionId = -1, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description, FinancialTransactionDetailId = -1, Amount = (descriptionId == 1050 ? TotalAmount : null) });
            }
            else
            {
                if (dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionDetailId == financialTransactionDetailId).Count() > 0)
                {
                    FinancialTransactionDetail detailrow = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionDetailId == financialTransactionDetailId).FirstOrDefault();
                    ViewBag.Title = "Update Financial Transaction";
                    return PartialView(detailrow);
                }
                else
                {
                    ViewBag.Title = "New Financial Transaction";
                    return PartialView(new FinancialTransactionDetail { FinancialTransactionId = financialTransactionId, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description, FinancialTransactionDetailId = -1, Amount = (descriptionId == 1050 ? TotalAmount : null) });
                }
            }
        }

        [HttpPost]
        public ActionResult SaveFinancialTransaction(FinancialTransactionVM ftvminfo, FinancialTransactionDetail dtl)
        {
            try
            {
                FinancialTransactionDetail financialTransactiondtl = new FinancialTransactionDetail();

                if (ftvminfo.financialTransaction.FinancialTransactionId == -1)
                {
                    FinancialTransaction financialTransaction = new FinancialTransaction();
                    financialTransaction = ftvminfo.financialTransaction;
                    financialTransaction.CreatedBy = USER_ID;
                    financialTransaction.CreatedOn = UTILITY.SINGAPORETIME;
                    financialTransaction.IsActive = true;
                    int count = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == ftvminfo.financialTransaction.FinancialTransactionId && x.TrainingConfirmationID == ftvminfo.financialTransaction.TrainingConfirmationID && x.DescriptionId == 1050).Count();
                    if (count == 0 && dtl.DescriptionId != 1050)
                    {
                        dbContext.FinancialTransactions.Add(financialTransaction);

                        FinancialTransactionDetail detail = new Models.FinancialTransactionDetail();
                        decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == ftvminfo.financialTransaction.TrainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);
                        detail.Amount = TotalAmount;
                        detail.DescriptionId = 1050;
                        detail.Description = "TOTAL REVENUE";
                        detail.LocalAmount = TotalAmount * ftvminfo.financialTransaction.CurrencyExRate;
                        detail.FinancialTransactionId = financialTransaction.FinancialTransactionId;
                        detail.TrainingConfirmationID = financialTransaction.TrainingConfirmationID;
                        detail.CreatedBy = USER_ID;
                        detail.CreatedOn = UTILITY.SINGAPORETIME;

                        dbContext.FinancialTransactionDetails.Add(detail);


                        //Details Save
                        dtl.FinancialTransactionId = financialTransaction.FinancialTransactionId;
                        dtl.CreatedBy = USER_ID;
                        dtl.CreatedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            dtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            dtl.ReferenceDoc = dtl.FileName.FileName;
                        }
                        else
                            dtl.ReferenceDoc = null;

                        dbContext.FinancialTransactionDetails.Add(dtl);

                        dbContext.SaveChanges();

                        financialTransaction = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == financialTransaction.FinancialTransactionId).FirstOrDefault();
                    }
                    else
                    {
                        //Details Save
                        dtl.FinancialTransactionId = financialTransaction.FinancialTransactionId;
                        dtl.CreatedBy = USER_ID;
                        dtl.CreatedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            dtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            dtl.ReferenceDoc = dtl.FileName.FileName;
                        }
                        else
                            dtl.ReferenceDoc = null;

                        dbContext.FinancialTransactionDetails.Add(dtl);
                    }

                    decimal grossprofit = 0;
                    decimal baseAmount = 0;
                    List<FinancialTransactionDetail> ftdtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == dtl.FinancialTransactionId && x.TrainingConfirmationID == dtl.TrainingConfirmationID).ToList();


                    if (ftdtl.Count() > 0)
                    {
                        foreach (var item in ftdtl)
                        {
                            if (item.DescriptionId != dtl.DescriptionId)
                            {
                                if (item.DescriptionId == 1050)
                                {
                                    baseAmount = item.Amount.Value;
                                    grossprofit += dtl.Amount.Value;
                                }
                                else
                                    grossprofit += item.Amount.Value;
                            }
                        }
                    }
                    else
                    {
                        if (dtl.DescriptionId == 1050)
                            baseAmount = dtl.Amount.Value;
                        else
                            grossprofit += dtl.Amount.Value;
                    }

                    financialTransaction.GrossProfit = baseAmount - grossprofit;
                    financialTransaction.ProfitAndLossPercent = (financialTransaction.GrossProfit / baseAmount) * 100;

                    if (count == 0 && dtl.DescriptionId != 1050)
                        dbContext.Entry(financialTransaction).State = EntityState.Modified;
                    else
                        dbContext.FinancialTransactions.Add(financialTransaction);

                }
                else
                {
                    FinancialTransaction financialTransaction = new FinancialTransaction();
                    financialTransaction = dbContext.FinancialTransactions.
                       Where(x => x.FinancialTransactionId == ftvminfo.financialTransaction.FinancialTransactionId).FirstOrDefault();

                    financialTransaction.Country = ftvminfo.financialTransaction.Country;
                    financialTransaction.CurrencyCode = ftvminfo.financialTransaction.CurrencyCode;
                    financialTransaction.CurrencyExRate = ftvminfo.financialTransaction.CurrencyExRate;
                    financialTransaction.IsActive = true;

                    financialTransaction.ModifiedBy = USER_ID;
                    financialTransaction.ModifiedOn = UTILITY.SINGAPORETIME;

                    decimal grossprofit = 0;
                    decimal baseAmount = 0;
                    List<FinancialTransactionDetail> ftdtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == dtl.FinancialTransactionId && x.TrainingConfirmationID == dtl.TrainingConfirmationID).ToList();


                    if (ftdtl.Count() > 0)
                    {
                        foreach (var item in ftdtl)
                        {
                            if (item.DescriptionId != dtl.DescriptionId)
                            {
                                if (item.DescriptionId == 1050)
                                {
                                    baseAmount = item.Amount.Value;
                                    grossprofit += dtl.Amount.Value;
                                }
                                else
                                    grossprofit += item.Amount.Value;
                            }
                        }
                    }
                    else
                    {
                        if (dtl.DescriptionId == 1050)
                            baseAmount = dtl.Amount.Value;
                        else
                            grossprofit += dtl.Amount.Value;
                    }

                    financialTransaction.GrossProfit = baseAmount - grossprofit;
                    financialTransaction.ProfitAndLossPercent = (financialTransaction.GrossProfit / baseAmount) * 100;
                    dbContext.Entry(financialTransaction).State = EntityState.Modified;



                    //Details Save
                    financialTransactiondtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == dtl.FinancialTransactionId && x.TrainingConfirmationID == dtl.TrainingConfirmationID && x.DescriptionId == dtl.DescriptionId).FirstOrDefault();
                    if (financialTransactiondtl != null)
                    {
                        financialTransactiondtl.Amount = dtl.Amount;
                        financialTransactiondtl.LocalAmount = dtl.LocalAmount;
                        financialTransactiondtl.Remarks = dtl.Remarks;
                        financialTransactiondtl.ReferenceDoc = dtl.ReferenceDoc;

                        financialTransactiondtl.ModifiedBy = USER_ID;
                        financialTransactiondtl.ModifiedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            financialTransactiondtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            financialTransactiondtl.ReferenceDoc = dtl.FileName.FileName;
                        }
                        else
                            financialTransactiondtl.ReferenceDoc = null;

                        dbContext.Entry(financialTransactiondtl).State = EntityState.Modified;

                    }
                    else
                    {

                        dtl.FinancialTransactionId = ftvminfo.financialTransaction.FinancialTransactionId;
                        dtl.CreatedBy = USER_ID;
                        dtl.CreatedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            dtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            dtl.ReferenceDoc = dtl.FileName.FileName;
                        }
                        else
                            dtl.ReferenceDoc = null;

                        dbContext.FinancialTransactionDetails.Add(dtl);
                    }
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("FinancialTransactionList", new { trainingConfirmationID = ftvminfo.financialTransaction.TrainingConfirmationID });
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
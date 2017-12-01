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
            Func<string, short?, string, string, HttpPostedFileBase> createfile = delegate (string TrainingConfirmationID, short? DescriptionId, string filename, string path)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                HttpPostedFileBase objFile = (HttpPostedFileBase)new MemoryPostedFile(bytes);
                return objFile;
            };

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

            financialTransactionVM.financialTransactionDetailList = dbContext.FinancialTransactionDetails
                .Where(x => x.FinancialTransactionId == financialTransaction.FinancialTransactionId && x.TrainingConfirmationID == financialTransaction.TrainingConfirmationID)
                .Select(x => new FinancialTransactionDetailVM
                {
                    FinancialTransactionDetailId = x.FinancialTransactionDetailId,
                    FinancialTransactionId = x.FinancialTransactionId,
                    TrainingConfirmationID = x.TrainingConfirmationID,
                    DescriptionId = x.DescriptionId,
                    Description = x.Description,
                    Amount = x.Amount,
                    LocalAmount = x.LocalAmount,
                    ReferenceDoc = x.ReferenceDoc,
                    Remarks = x.Remarks,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedOn = x.ModifiedOn
                })
                .ToList();


            financialTransactionVM.financialTransaction = financialTransaction;
            //financialTransactionVM.financialTransactionDetailList = financialTransactionDetails;
            financialTransactionVM.currencyList = dbContext.Lookups.Where(x => x.LookupCategory == "Currency").ToList();
            financialTransactionVM.countryList = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();
            financialTransactionVM.trainingConfirmationList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();

            var FinancialTransactionLookupList = dbContext.Lookups.Where(x => x.LookupCategory == "FinancialTransaction").ToList();
            decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == trainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);

            foreach (EDU.Web.Models.Lookup item in FinancialTransactionLookupList)
            {
                if (financialTransactionVM.financialTransactionDetailList.Where(x => x.DescriptionId == item.LookupID).Count() == 0)
                    financialTransactionVM.financialTransactionDetailList.Add(new FinancialTransactionDetailVM() { DescriptionId = item.LookupID, Description = item.LookupDescription, FinancialTransactionId = financialTransaction.FinancialTransactionId, TrainingConfirmationID = financialTransaction.TrainingConfirmationID, FinancialTransactionDetailId = -1, Amount = (item.LookupID == 1050 ? TotalAmount : null) });
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
                return PartialView(new FinancialTransactionDetailVM { FinancialTransactionId = -1, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description, FinancialTransactionDetailId = -1, Amount = (descriptionId == 1050 ? TotalAmount : null) });
            }
            else
            {
                if (dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionDetailId == financialTransactionDetailId).Count() > 0)
                {
                    FinancialTransactionDetailVM detailrow = dbContext.FinancialTransactionDetails
                        .Where(x => x.FinancialTransactionDetailId == financialTransactionDetailId)
                        .Select(x => new FinancialTransactionDetailVM
                        {
                            FinancialTransactionDetailId = x.FinancialTransactionDetailId,
                            FinancialTransactionId = x.FinancialTransactionId,
                            TrainingConfirmationID = x.TrainingConfirmationID,
                            DescriptionId = x.DescriptionId,
                            Description = x.Description,
                            Amount = x.Amount,
                            LocalAmount = x.LocalAmount,
                            ReferenceDoc = x.ReferenceDoc,
                            Remarks = x.Remarks,
                            CreatedBy = x.CreatedBy,
                            CreatedOn = x.CreatedOn,
                            ModifiedBy = x.ModifiedBy,
                            ModifiedOn = x.ModifiedOn
                        }).FirstOrDefault();
                    ViewBag.Title = "Update Financial Transaction";
                    return PartialView(detailrow);
                }
                else
                {
                    ViewBag.Title = "New Financial Transaction";
                    return PartialView(new FinancialTransactionDetailVM { FinancialTransactionId = financialTransactionId, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description, FinancialTransactionDetailId = -1, Amount = (descriptionId == 1050 ? TotalAmount : null) });
                }
            }
        }

        [HttpPost]
        public ActionResult SaveFinancialTransaction(FinancialTransactionVM ftvminfo, FinancialTransactionDetailVM dtl)
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
                        FinancialTransactionDetail detail1 = new Models.FinancialTransactionDetail();

                        detail1.Amount = dtl.Amount;
                        detail1.DescriptionId = dtl.DescriptionId;
                        detail1.Description = dtl.Description;
                        detail1.LocalAmount = dtl.LocalAmount;
                        detail1.FinancialTransactionId = financialTransaction.FinancialTransactionId;
                        detail1.TrainingConfirmationID = dtl.TrainingConfirmationID;
                        detail1.Remarks = dtl.Remarks;

                        detail1.CreatedBy = USER_ID;
                        detail1.CreatedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            dtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            detail1.ReferenceDoc = dtl.FileName.FileName;
                        }
                        else
                            detail1.ReferenceDoc = null;

                        dbContext.FinancialTransactionDetails.Add(detail1);

                        dbContext.SaveChanges();

                        financialTransaction = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == financialTransaction.FinancialTransactionId).FirstOrDefault();
                    }
                    else
                    {
                        //Details Save
                        FinancialTransactionDetail detail = new Models.FinancialTransactionDetail();

                        detail.Amount = dtl.Amount;
                        detail.DescriptionId = dtl.DescriptionId;
                        detail.Description = dtl.Description;
                        detail.LocalAmount = dtl.LocalAmount;
                        detail.FinancialTransactionId = financialTransaction.FinancialTransactionId;
                        detail.TrainingConfirmationID = dtl.TrainingConfirmationID;
                        detail.Remarks = dtl.Remarks;

                        detail.FinancialTransactionId = financialTransaction.FinancialTransactionId;
                        detail.CreatedBy = USER_ID;
                        detail.CreatedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            dtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            detail.ReferenceDoc = dtl.FileName.FileName;
                        }
                        else
                            detail.ReferenceDoc = null;

                        dbContext.FinancialTransactionDetails.Add(detail);
                    }

                    decimal grossprofit = 0;
                    decimal baseAmount = 0;
                    List<FinancialTransactionDetail> ftdtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == financialTransaction.FinancialTransactionId && x.TrainingConfirmationID == financialTransaction.TrainingConfirmationID).ToList();


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
                    List<FinancialTransactionDetail> ftdtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == dtl.FinancialTransactionId && x.TrainingConfirmationID == dtl.TrainingConfirmationID).OrderBy(d => d.DescriptionId).ToList();


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
                            else if (dtl.DescriptionId == 1050)
                            {
                                baseAmount = dtl.Amount.Value;
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
                        //financialTransactiondtl.ReferenceDoc = dtl.ReferenceDoc;

                        financialTransactiondtl.ModifiedBy = USER_ID;
                        financialTransactiondtl.ModifiedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            dtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            financialTransactiondtl.ReferenceDoc = dtl.FileName.FileName;
                        }
                        //else
                        //    financialTransactiondtl.ReferenceDoc = null;

                        dbContext.Entry(financialTransactiondtl).State = EntityState.Modified;

                    }
                    else
                    {

                        FinancialTransactionDetail detail = new FinancialTransactionDetail();

                        detail.Amount = dtl.Amount;
                        detail.DescriptionId = dtl.DescriptionId;
                        detail.Description = dtl.Description;
                        detail.LocalAmount = dtl.LocalAmount;
                        detail.TrainingConfirmationID = dtl.TrainingConfirmationID;
                        detail.Remarks = dtl.Remarks;

                        detail.FinancialTransactionId = ftvminfo.financialTransaction.FinancialTransactionId;
                        detail.CreatedBy = USER_ID;
                        detail.CreatedOn = UTILITY.SINGAPORETIME;

                        if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            dtl.FileName.SaveAs(path + dtl.FileName.FileName);
                            detail.ReferenceDoc = dtl.FileName.FileName;
                        }
                        else
                            detail.ReferenceDoc = null;

                        dbContext.FinancialTransactionDetails.Add(detail);
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


        [HttpPost]
        public JsonResult UpdateFinancialTransactionLocalAmount(FinancialTransaction ft)
        {
            if (ft.FinancialTransactionId == -1)
            {                
                return Json("Please update Financial Transaction Information before click on update button.", JsonRequestBehavior.AllowGet);
            }
            FinancialTransaction ftrans = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == ft.FinancialTransactionId && x.TrainingConfirmationID == ft.TrainingConfirmationID && x.IsActive == true).First();

            ftrans.CurrencyCode = ft.CurrencyCode;
            ftrans.Country = ft.Country;
            ftrans.CurrencyExRate = ft.CurrencyExRate;
            ftrans.ModifiedBy = USER_ID;
            ftrans.ModifiedOn = UTILITY.SINGAPORETIME;
            dbContext.Entry(ftrans).State = EntityState.Modified;

            List<FinancialTransactionDetail> FinancialTransactionDetailList = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == ft.FinancialTransactionId && x.TrainingConfirmationID == ft.TrainingConfirmationID).ToList(); //&& x.CurrencyCode == revenue.CurrencyCode

            try
            {
                foreach (FinancialTransactionDetail ftd in FinancialTransactionDetailList)
                {
                    ftd.LocalAmount = ftd.Amount * ftrans.CurrencyExRate;

                    ftd.ModifiedBy = USER_ID;
                    ftd.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(ftd).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string message = "Local Amount Updated Successfully.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitFinancialTransaction(FinancialTransaction ft)
        {
            FinancialTransaction ftrans = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == ft.FinancialTransactionId && x.TrainingConfirmationID == ft.TrainingConfirmationID && x.IsActive == true).First();

            ftrans.IsSubmit = true;
            ftrans.ModifiedBy = USER_ID;
            ftrans.ModifiedOn = UTILITY.SINGAPORETIME;
            dbContext.Entry(ftrans).State = EntityState.Modified;
            dbContext.SaveChanges();

            string message = "Submitted successfully.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
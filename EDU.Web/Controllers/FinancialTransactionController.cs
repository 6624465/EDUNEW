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
        public ActionResult FinancialTransactionList()
        {
            List<FinancialTransactionsVM> financialTransactionList = new List<FinancialTransactionsVM>();
            List<TrainingConfirmation> trainingConfirmationList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();
            List<FinancialTransaction> financialTransaction1List = dbContext.FinancialTransactions.Where(x => x.IsActive == true).ToList();
            var countryList = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();


            financialTransactionList = financialTransaction1List
                .Select(x => new FinancialTransactionsVM
                {
                    FinancialTransactionId = x.FinancialTransactionId,
                    TrainingConfirmationID = x.TrainingConfirmationID,
                    Country = x.Country,
                    CountryName = countryList.Where(y => y.BranchID == x.Country).FirstOrDefault().BranchName,
                    CurrencyCode = x.CurrencyCode,
                    CurrencyExRate = x.CurrencyExRate,
                    TotalRevenueAmount = x.TotalRevenueAmount,
                    TotalRevenueLocalAmount = x.TotalRevenueLocalAmount,
                    TotalRevenueReferenceDoc = x.TotalRevenueReferenceDoc,
                    TotalRevenueRemarks = x.TotalRevenueRemarks,
                    TrainerExpensesAmount = x.TrainerExpensesAmount,
                    TrainerExpensesLocalAmount = x.TrainerExpensesLocalAmount,
                    TrainerExpensesReferenceDoc = x.TrainerExpensesReferenceDoc,
                    TrainerExpensesRemarks = x.TrainerExpensesRemarks,
                    TrainerTravelExpensesAmount = x.TrainerTravelExpensesAmount,
                    TrainerTravelExpensesLocalAmount = x.TrainerTravelExpensesLocalAmount,
                    TrainerTravelExpensesReferenceDoc = x.TrainerTravelExpensesReferenceDoc,
                    TrainerTravelExpensesRemarks = x.TrainerTravelExpensesRemarks,
                    LocalExpensesAmount = x.LocalExpensesAmount,
                    LocalExpensesLocalAmount = x.LocalExpensesLocalAmount,
                    LocalExpensesReferenceDoc = x.LocalExpensesReferenceDoc,
                    LocalExpensesRemarks = x.LocalExpensesRemarks,
                    CoursewareMaterialAmount = x.CoursewareMaterialAmount,
                    CoursewareMaterialLocalAmount = x.CoursewareMaterialLocalAmount,
                    CoursewareMaterialReferenceDoc = x.CoursewareMaterialReferenceDoc,
                    CoursewareMaterialRemarks = x.CoursewareMaterialRemarks,
                    MiscExpensesAmount = x.MiscExpensesAmount,
                    MiscExpensesLocalAmount = x.MiscExpensesLocalAmount,
                    MiscExpensesReferenceDoc = x.MiscExpensesReferenceDoc,
                    MiscExpensesRemarks = x.MiscExpensesRemarks,
                    GrossProfit = x.GrossProfit,
                    ProfitAndLossPercent = x.ProfitAndLossPercent,
                    IsActive = x.IsActive,
                    IsSubmit = x.IsSubmit,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedOn = x.ModifiedOn
                })
                .ToList();


            foreach (var item in trainingConfirmationList)
            {
                if (financialTransaction1List.Where(x => x.TrainingConfirmationID == item.TrainingConfirmationID).Count() == 0)
                {
                    decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == item.TrainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);


                    financialTransactionList.Add(new FinancialTransactionsVM()
                    {
                        FinancialTransactionId = -1,
                        TrainingConfirmationID = item.TrainingConfirmationID,
                        Country = item.Country,
                        CountryName = countryList.Where(y => y.BranchID == item.Country).FirstOrDefault().BranchName,
                        CurrencyCode = null,
                        CurrencyExRate = null,
                        TotalRevenueAmount = TotalAmount,
                        TotalRevenueLocalAmount = null,
                        TotalRevenueReferenceDoc = null,
                        TotalRevenueRemarks = null,
                        TrainerExpensesAmount = null,
                        TrainerExpensesLocalAmount = null,
                        TrainerExpensesReferenceDoc = null,
                        TrainerExpensesRemarks = null,
                        TrainerTravelExpensesAmount = null,
                        TrainerTravelExpensesLocalAmount = null,
                        TrainerTravelExpensesReferenceDoc = null,
                        TrainerTravelExpensesRemarks = null,
                        LocalExpensesAmount = null,
                        LocalExpensesLocalAmount = null,
                        LocalExpensesReferenceDoc = null,
                        LocalExpensesRemarks = null,
                        CoursewareMaterialAmount = null,
                        CoursewareMaterialLocalAmount = null,
                        CoursewareMaterialReferenceDoc = null,
                        CoursewareMaterialRemarks = null,
                        MiscExpensesAmount = null,
                        MiscExpensesLocalAmount = null,
                        MiscExpensesReferenceDoc = null,
                        MiscExpensesRemarks = null,
                        GrossProfit = null,
                        ProfitAndLossPercent = null,
                        IsActive = true,
                        IsSubmit = null,
                        CreatedBy = null,
                        CreatedOn = null,
                        ModifiedBy = null,
                        ModifiedOn = null
                    });
                }
            }

            return View(financialTransactionList);
        }

        [HttpGet]
        public ActionResult FinancialTransactionDetail(int Id, string trainingConfirmationID, Int16? country, decimal? totalRevenueAmount)
        {
            var currencyList = dbContext.Lookups.Where(x => x.LookupCategory == "Currency").ToList();
            var countryList = new BranchBO().GetList();
            FinancialTransaction financialtransaction = new FinancialTransaction();
            if (Id == -1)
            {
                financialtransaction = new FinancialTransaction { FinancialTransactionId = -1, TrainingConfirmationID = trainingConfirmationID, Country = country, TotalRevenueAmount = totalRevenueAmount };
                ViewBag.Title = "New Financial Transaction";

            }
            else
            {
                ViewBag.Title = "Update Financial Transaction";
                financialtransaction = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == Id && x.IsActive == true).FirstOrDefault();
            }
            ViewData["CurrencyList"] = currencyList;
            ViewData["CountryData"] = countryList;
            return View(financialtransaction);
        }

        [HttpPost]
        public ActionResult SaveFinancialTransactionDetail(FinancialTransaction ftInfo)
        {
            try
            {
                FinancialTransaction financialtransactions = new FinancialTransaction();
                if (ftInfo.FinancialTransactionId == -1)
                {


                    ftInfo.IsActive = true;
                    ftInfo.CreatedBy = USER_ID;
                    ftInfo.CreatedOn = UTILITY.SINGAPORETIME;
                    dbContext.FinancialTransactions.Add(ftInfo);

                }
                else {
                    financialtransactions = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == ftInfo.FinancialTransactionId).FirstOrDefault();


                    financialtransactions.FinancialTransactionId = ftInfo.FinancialTransactionId;
                    financialtransactions.TrainingConfirmationID = ftInfo.TrainingConfirmationID;
                    financialtransactions.Country = ftInfo.Country;
                    financialtransactions.CurrencyCode = ftInfo.CurrencyCode;
                    financialtransactions.CurrencyExRate = ftInfo.CurrencyExRate;
                    financialtransactions.TotalRevenueAmount = ftInfo.TotalRevenueAmount;
                    financialtransactions.TotalRevenueLocalAmount = ftInfo.TotalRevenueLocalAmount;
                    financialtransactions.TotalRevenueReferenceDoc = ftInfo.TotalRevenueReferenceDoc;
                    financialtransactions.TotalRevenueRemarks = ftInfo.TotalRevenueRemarks;
                    financialtransactions.TrainerExpensesAmount = ftInfo.TrainerExpensesAmount;
                    financialtransactions.TrainerExpensesLocalAmount = ftInfo.TrainerExpensesLocalAmount;
                    financialtransactions.TrainerExpensesReferenceDoc = ftInfo.TrainerExpensesReferenceDoc;
                    financialtransactions.TrainerExpensesRemarks = ftInfo.TrainerExpensesRemarks;
                    financialtransactions.TrainerTravelExpensesAmount = ftInfo.TrainerTravelExpensesAmount;
                    financialtransactions.TrainerTravelExpensesLocalAmount = ftInfo.TrainerTravelExpensesLocalAmount;
                    financialtransactions.TrainerTravelExpensesReferenceDoc = ftInfo.TrainerTravelExpensesReferenceDoc;
                    financialtransactions.TrainerTravelExpensesRemarks = ftInfo.TrainerTravelExpensesRemarks;
                    financialtransactions.LocalExpensesAmount = ftInfo.LocalExpensesAmount;
                    financialtransactions.LocalExpensesLocalAmount = ftInfo.LocalExpensesLocalAmount;
                    financialtransactions.LocalExpensesReferenceDoc = ftInfo.LocalExpensesReferenceDoc;
                    financialtransactions.LocalExpensesRemarks = ftInfo.LocalExpensesRemarks;
                    financialtransactions.CoursewareMaterialAmount = ftInfo.CoursewareMaterialAmount;
                    financialtransactions.CoursewareMaterialLocalAmount = ftInfo.CoursewareMaterialLocalAmount;
                    financialtransactions.CoursewareMaterialReferenceDoc = ftInfo.CoursewareMaterialReferenceDoc;
                    financialtransactions.CoursewareMaterialRemarks = ftInfo.CoursewareMaterialRemarks;
                    financialtransactions.MiscExpensesAmount = ftInfo.MiscExpensesAmount;
                    financialtransactions.MiscExpensesLocalAmount = ftInfo.MiscExpensesLocalAmount;
                    financialtransactions.MiscExpensesReferenceDoc = ftInfo.MiscExpensesReferenceDoc;
                    financialtransactions.MiscExpensesRemarks = ftInfo.MiscExpensesRemarks;
                    financialtransactions.GrossProfit = ftInfo.GrossProfit;
                    financialtransactions.ProfitAndLossPercent = ftInfo.ProfitAndLossPercent;
                    financialtransactions.IsActive = true;
                    financialtransactions.IsSubmit = ftInfo.IsSubmit;

                    financialtransactions.ModifiedBy = USER_ID;
                    financialtransactions.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(financialtransactions).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("FinancialTransactionList");
        }

        //[HttpGet]
        //public PartialViewResult FinancialTransactionDetail(int financialTransactionId, string trainingConfirmationID, short? descriptionId, string description, decimal? currencyExRate, int financialTransactionDetailId)
        //{
        //ViewData["CurrencyExRate"] = currencyExRate;
        //decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == trainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);

        //if (financialTransactionId == -1)
        //{
        //    ViewBag.Title = "New Financial Transaction";
        //    return PartialView(new FinancialTransactionDetailVM { FinancialTransactionId = -1, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description, FinancialTransactionDetailId = -1, Amount = (descriptionId == 1050 ? TotalAmount : null) });
        //}
        //else
        //{
        //    if (dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionDetailId == financialTransactionDetailId).Count() > 0)
        //    {
        //        FinancialTransactionDetailVM detailrow = dbContext.FinancialTransactionDetails
        //            .Where(x => x.FinancialTransactionDetailId == financialTransactionDetailId)
        //            .Select(x => new FinancialTransactionDetailVM
        //            {
        //                FinancialTransactionDetailId = x.FinancialTransactionDetailId,
        //                FinancialTransactionId = x.FinancialTransactionId,
        //                TrainingConfirmationID = x.TrainingConfirmationID,
        //                DescriptionId = x.DescriptionId,
        //                Description = x.Description,
        //                Amount = x.Amount,
        //                LocalAmount = x.LocalAmount,
        //                ReferenceDoc = x.ReferenceDoc,
        //                Remarks = x.Remarks,
        //                CreatedBy = x.CreatedBy,
        //                CreatedOn = x.CreatedOn,
        //                ModifiedBy = x.ModifiedBy,
        //                ModifiedOn = x.ModifiedOn
        //            }).FirstOrDefault();
        //        ViewBag.Title = "Update Financial Transaction";
        //        return PartialView(detailrow);
        //    }
        //    else
        //    {
        //        ViewBag.Title = "New Financial Transaction";
        //        return PartialView(new FinancialTransactionDetailVM { FinancialTransactionId = financialTransactionId, TrainingConfirmationID = trainingConfirmationID, DescriptionId = descriptionId, Description = description, FinancialTransactionDetailId = -1, Amount = (descriptionId == 1050 ? TotalAmount : null) });
        //    }
        //}
        //}

        //[HttpPost]
        //public ActionResult SaveFinancialTransaction(FinancialTransactionVM ftvminfo, FinancialTransactionDetailVM dtl)
        //{
        //try
        //{
        //    FinancialTransactionDetail financialTransactiondtl = new FinancialTransactionDetail();

        //    if (ftvminfo.financialTransaction.FinancialTransactionId == -1)
        //    {
        //        FinancialTransaction financialTransaction = new FinancialTransaction();
        //        financialTransaction = ftvminfo.financialTransaction;
        //        financialTransaction.CreatedBy = USER_ID;
        //        financialTransaction.CreatedOn = UTILITY.SINGAPORETIME;
        //        financialTransaction.IsActive = true;
        //        int count = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == ftvminfo.financialTransaction.FinancialTransactionId && x.TrainingConfirmationID == ftvminfo.financialTransaction.TrainingConfirmationID && x.DescriptionId == 1050).Count();
        //        if (count == 0 && dtl.DescriptionId != 1050)
        //        {
        //            dbContext.FinancialTransactions.Add(financialTransaction);

        //            FinancialTransactionDetail detail = new Models.FinancialTransactionDetail();
        //            decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == ftvminfo.financialTransaction.TrainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);
        //            detail.Amount = TotalAmount;
        //            detail.DescriptionId = 1050;
        //            detail.Description = "TOTAL REVENUE";
        //            detail.LocalAmount = TotalAmount * ftvminfo.financialTransaction.CurrencyExRate;
        //            detail.FinancialTransactionId = financialTransaction.FinancialTransactionId;
        //            detail.TrainingConfirmationID = financialTransaction.TrainingConfirmationID;
        //            detail.CreatedBy = USER_ID;
        //            detail.CreatedOn = UTILITY.SINGAPORETIME;

        //            dbContext.FinancialTransactionDetails.Add(detail);


        //            //Details Save
        //            FinancialTransactionDetail detail1 = new Models.FinancialTransactionDetail();

        //            detail1.Amount = dtl.Amount;
        //            detail1.DescriptionId = dtl.DescriptionId;
        //            detail1.Description = dtl.Description;
        //            detail1.LocalAmount = dtl.LocalAmount;
        //            detail1.FinancialTransactionId = financialTransaction.FinancialTransactionId;
        //            detail1.TrainingConfirmationID = dtl.TrainingConfirmationID;
        //            detail1.Remarks = dtl.Remarks;

        //            detail1.CreatedBy = USER_ID;
        //            detail1.CreatedOn = UTILITY.SINGAPORETIME;

        //            if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
        //            {
        //                string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }

        //                dtl.FileName.SaveAs(path + dtl.FileName.FileName);
        //                detail1.ReferenceDoc = dtl.FileName.FileName;
        //            }
        //            else
        //                detail1.ReferenceDoc = null;

        //            dbContext.FinancialTransactionDetails.Add(detail1);

        //            dbContext.SaveChanges();

        //            financialTransaction = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == financialTransaction.FinancialTransactionId).FirstOrDefault();
        //        }
        //        else
        //        {
        //            //Details Save
        //            FinancialTransactionDetail detail = new Models.FinancialTransactionDetail();

        //            detail.Amount = dtl.Amount;
        //            detail.DescriptionId = dtl.DescriptionId;
        //            detail.Description = dtl.Description;
        //            detail.LocalAmount = dtl.LocalAmount;
        //            detail.FinancialTransactionId = financialTransaction.FinancialTransactionId;
        //            detail.TrainingConfirmationID = dtl.TrainingConfirmationID;
        //            detail.Remarks = dtl.Remarks;

        //            detail.FinancialTransactionId = financialTransaction.FinancialTransactionId;
        //            detail.CreatedBy = USER_ID;
        //            detail.CreatedOn = UTILITY.SINGAPORETIME;

        //            if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
        //            {
        //                string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }

        //                dtl.FileName.SaveAs(path + dtl.FileName.FileName);
        //                detail.ReferenceDoc = dtl.FileName.FileName;
        //            }
        //            else
        //                detail.ReferenceDoc = null;

        //            dbContext.FinancialTransactionDetails.Add(detail);
        //        }

        //        decimal grossprofit = 0;
        //        decimal baseAmount = 0;
        //        List<FinancialTransactionDetail> ftdtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == financialTransaction.FinancialTransactionId && x.TrainingConfirmationID == financialTransaction.TrainingConfirmationID).ToList();


        //        if (ftdtl.Count() > 0)
        //        {
        //            foreach (var item in ftdtl)
        //            {
        //                if (item.DescriptionId != dtl.DescriptionId)
        //                {
        //                    if (item.DescriptionId == 1050)
        //                    {
        //                        baseAmount = item.Amount.Value;
        //                        grossprofit += dtl.Amount.Value;
        //                    }
        //                    else
        //                        grossprofit += item.Amount.Value;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (dtl.DescriptionId == 1050)
        //                baseAmount = dtl.Amount.Value;
        //            else
        //                grossprofit += dtl.Amount.Value;
        //        }

        //        financialTransaction.GrossProfit = baseAmount - grossprofit;
        //        financialTransaction.ProfitAndLossPercent = (financialTransaction.GrossProfit / baseAmount) * 100;

        //        if (count == 0 && dtl.DescriptionId != 1050)
        //            dbContext.Entry(financialTransaction).State = EntityState.Modified;
        //        else
        //            dbContext.FinancialTransactions.Add(financialTransaction);

        //    }
        //    else
        //    {
        //        FinancialTransaction financialTransaction = new FinancialTransaction();
        //        financialTransaction = dbContext.FinancialTransactions.
        //           Where(x => x.FinancialTransactionId == ftvminfo.financialTransaction.FinancialTransactionId).FirstOrDefault();

        //        financialTransaction.Country = ftvminfo.financialTransaction.Country;
        //        financialTransaction.CurrencyCode = ftvminfo.financialTransaction.CurrencyCode;
        //        financialTransaction.CurrencyExRate = ftvminfo.financialTransaction.CurrencyExRate;
        //        financialTransaction.IsActive = true;

        //        financialTransaction.ModifiedBy = USER_ID;
        //        financialTransaction.ModifiedOn = UTILITY.SINGAPORETIME;

        //        decimal grossprofit = 0;
        //        decimal baseAmount = 0;
        //        List<FinancialTransactionDetail> ftdtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == dtl.FinancialTransactionId && x.TrainingConfirmationID == dtl.TrainingConfirmationID).OrderBy(d => d.DescriptionId).ToList();


        //        if (ftdtl.Count() > 0)
        //        {
        //            foreach (var item in ftdtl)
        //            {
        //                if (item.DescriptionId != dtl.DescriptionId)
        //                {
        //                    if (item.DescriptionId == 1050)
        //                    {
        //                        baseAmount = item.Amount.Value;
        //                        grossprofit += dtl.Amount.Value;
        //                    }
        //                    else
        //                        grossprofit += item.Amount.Value;
        //                }
        //                else if (dtl.DescriptionId == 1050)
        //                {
        //                    baseAmount = dtl.Amount.Value;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (dtl.DescriptionId == 1050)
        //                baseAmount = dtl.Amount.Value;
        //            else
        //                grossprofit += dtl.Amount.Value;
        //        }

        //        financialTransaction.GrossProfit = baseAmount - grossprofit;
        //        financialTransaction.ProfitAndLossPercent = (financialTransaction.GrossProfit / baseAmount) * 100;
        //        dbContext.Entry(financialTransaction).State = EntityState.Modified;



        //        //Details Save
        //        financialTransactiondtl = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == dtl.FinancialTransactionId && x.TrainingConfirmationID == dtl.TrainingConfirmationID && x.DescriptionId == dtl.DescriptionId).FirstOrDefault();
        //        if (financialTransactiondtl != null)
        //        {
        //            financialTransactiondtl.Amount = dtl.Amount;
        //            financialTransactiondtl.LocalAmount = dtl.LocalAmount;
        //            financialTransactiondtl.Remarks = dtl.Remarks;
        //            //financialTransactiondtl.ReferenceDoc = dtl.ReferenceDoc;

        //            financialTransactiondtl.ModifiedBy = USER_ID;
        //            financialTransactiondtl.ModifiedOn = UTILITY.SINGAPORETIME;

        //            if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
        //            {
        //                string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }
        //                dtl.FileName.SaveAs(path + dtl.FileName.FileName);
        //                financialTransactiondtl.ReferenceDoc = dtl.FileName.FileName;
        //            }
        //            //else
        //            //    financialTransactiondtl.ReferenceDoc = null;

        //            dbContext.Entry(financialTransactiondtl).State = EntityState.Modified;

        //        }
        //        else
        //        {

        //            FinancialTransactionDetail detail = new FinancialTransactionDetail();

        //            detail.Amount = dtl.Amount;
        //            detail.DescriptionId = dtl.DescriptionId;
        //            detail.Description = dtl.Description;
        //            detail.LocalAmount = dtl.LocalAmount;
        //            detail.TrainingConfirmationID = dtl.TrainingConfirmationID;
        //            detail.Remarks = dtl.Remarks;

        //            detail.FinancialTransactionId = ftvminfo.financialTransaction.FinancialTransactionId;
        //            detail.CreatedBy = USER_ID;
        //            detail.CreatedOn = UTILITY.SINGAPORETIME;

        //            if (dtl.FileName != null && dtl.FileName.ContentLength > 0)
        //            {
        //                string path = Server.MapPath("~/Uploads/" + dtl.TrainingConfirmationID + "/" + dtl.DescriptionId + "/");
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }

        //                dtl.FileName.SaveAs(path + dtl.FileName.FileName);
        //                detail.ReferenceDoc = dtl.FileName.FileName;
        //            }
        //            else
        //                detail.ReferenceDoc = null;

        //            dbContext.FinancialTransactionDetails.Add(detail);
        //        }
        //    }

        //    dbContext.SaveChanges();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}

        //return RedirectToAction("FinancialTransactionList", new { trainingConfirmationID = ftvminfo.financialTransaction.TrainingConfirmationID });
        //}

        [HttpGet]
        public JsonResult GetCurrency(Int16 Id)
        {
            var country = new BranchBO().GetList().Where(x => x.IsActive == true && x.BranchID == Id).FirstOrDefault();
            var currencyCode = dbContext.Lookups.Where(x => x.LookupCategory == "Currency" && x.MappingCode == country.BranchCode).FirstOrDefault();

            return Json(currencyCode == null ? "" : currencyCode.LookupCode, JsonRequestBehavior.AllowGet);

        }


        //[HttpPost]
        //public JsonResult UpdateFinancialTransactionLocalAmount(FinancialTransaction ft)
        //{
        //    if (ft.FinancialTransactionId == -1)
        //    {
        //        return Json("Please update Financial Transaction Information before click on update button.", JsonRequestBehavior.AllowGet);
        //    }
        //    FinancialTransaction ftrans = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == ft.FinancialTransactionId && x.TrainingConfirmationID == ft.TrainingConfirmationID && x.IsActive == true).First();

        //    ftrans.CurrencyCode = ft.CurrencyCode;
        //    ftrans.Country = ft.Country;
        //    ftrans.CurrencyExRate = ft.CurrencyExRate;
        //    ftrans.ModifiedBy = USER_ID;
        //    ftrans.ModifiedOn = UTILITY.SINGAPORETIME;
        //    dbContext.Entry(ftrans).State = EntityState.Modified;

        //    List<FinancialTransactionDetail> FinancialTransactionDetailList = dbContext.FinancialTransactionDetails.Where(x => x.FinancialTransactionId == ft.FinancialTransactionId && x.TrainingConfirmationID == ft.TrainingConfirmationID).ToList(); //&& x.CurrencyCode == revenue.CurrencyCode

        //    try
        //    {
        //        foreach (FinancialTransactionDetail ftd in FinancialTransactionDetailList)
        //        {
        //            ftd.LocalAmount = ftd.Amount * ftrans.CurrencyExRate;

        //            ftd.ModifiedBy = USER_ID;
        //            ftd.ModifiedOn = UTILITY.SINGAPORETIME;

        //            dbContext.Entry(ftd).State = EntityState.Modified;
        //        }

        //        dbContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    string message = "Local Amount Updated Successfully.";
        //    return Json(message, JsonRequestBehavior.AllowGet);
        //}

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
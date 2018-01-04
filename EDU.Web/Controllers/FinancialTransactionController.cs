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
        public ActionResult FinancialTransactionList(short? month, int year)
        {
            try
            {
                List<FinancialTransactionsVM> financialTransactionList = new List<FinancialTransactionsVM>();
                List<TrainingConfirmation> trainingConfirmationList = new List<TrainingConfirmation>();
                if (month == 0)
                    trainingConfirmationList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true && x.Year == year).ToList();
                else
                    trainingConfirmationList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true && x.Year == year && x.Month == month).ToList();

                List<string> list = trainingConfirmationList.Select(x => x.TrainingConfirmationID).ToList();

                List<string> reglist = dbContext.Registrations
                                        .Where(x => x.IsActive == true && list.Contains(x.TrainingConfirmationID))
                                        .Select(x => x.TrainingConfirmationID).Distinct().ToList();

                List<FinancialTransaction> financialTransaction1List = dbContext.FinancialTransactions
                                                                         .Where(x => x.IsActive == true && reglist.Contains(x.TrainingConfirmationID)).ToList();
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
                        TrainerClaimsAmount = x.TrainerClaimsAmount,
                        TrainerClaimsLocalAmount = x.TrainerClaimsLocalAmount,
                        TrainerClaimsReferenceDoc = x.TrainerClaimsReferenceDoc,
                        TrainerClaimsRemarks = x.TrainerClaimsRemarks,
                        GrossProfit = x.GrossProfit,
                        ProfitAndLossPercent = x.ProfitAndLossPercent,
                        IsActive = x.IsActive,
                        IsSubmit = x.IsSubmit,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedOn = x.ModifiedOn,
                        Year = year,
                        Month = month
                    })
                    .ToList();


                foreach (var item in reglist)
                {
                    if (financialTransaction1List.Where(x => x.TrainingConfirmationID == item).Count() == 0)
                    {
                        decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == item && x.IsActive == true).Sum(y => y.TotalAmount);
                        TrainingConfirmation trainingConfirmation = dbContext.TrainingConfirmations.Where(x => x.IsActive == true && x.TrainingConfirmationID == item).FirstOrDefault();


                        financialTransactionList.Add(new FinancialTransactionsVM()
                        {
                            FinancialTransactionId = -1,
                            TrainingConfirmationID = item,
                            Country = trainingConfirmation.Country,
                            CountryName = countryList.Where(y => y.BranchID == trainingConfirmation.Country).FirstOrDefault().BranchName,
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
                            TrainerClaimsAmount = null,
                            TrainerClaimsLocalAmount = null,
                            TrainerClaimsReferenceDoc = null,
                            TrainerClaimsRemarks = null,
                            GrossProfit = null,
                            ProfitAndLossPercent = null,
                            IsActive = true,
                            IsSubmit = null,
                            CreatedBy = null,
                            CreatedOn = null,
                            ModifiedBy = null,
                            ModifiedOn = null,
                            Year = year,
                            Month = month
                        });
                    }
                }

                long revenue = 0;
                long trainerFee = 0;
                long trainerAirfare = 0;
                long hotelBookings = 0;
                long trainerClaims = 0;
                long RegionalExp = 0;
                long coursewareMaterial = 0;
                long grossProfit = 0;

                foreach (var item in financialTransactionList)
                {
                    revenue += Convert.ToInt64(item.TotalRevenueAmount == null ? 0 : item.TotalRevenueAmount.Value);
                    trainerFee += Convert.ToInt64(item.TrainerExpensesAmount == null ? 0 : item.TrainerExpensesAmount.Value);
                    trainerAirfare += Convert.ToInt64(item.TrainerTravelExpensesAmount == null ? 0 : item.TrainerTravelExpensesAmount.Value);
                    hotelBookings += Convert.ToInt64(item.LocalExpensesAmount == null ? 0 : item.LocalExpensesAmount.Value);
                    trainerClaims += Convert.ToInt64(item.TrainerClaimsAmount == null ? 0 : item.TrainerClaimsAmount.Value);
                    RegionalExp += Convert.ToInt64(item.MiscExpensesAmount == null ? 0 : item.MiscExpensesAmount.Value);
                    coursewareMaterial += Convert.ToInt64(item.CoursewareMaterialAmount == null ? 0 : item.CoursewareMaterialAmount.Value);
                    grossProfit += Convert.ToInt64(item.GrossProfit == null ? 0 : item.GrossProfit.Value);                   
                }

                List<decimal?> summary = new List<decimal?>();
                summary.Add(revenue);
                summary.Add(trainerFee);
                summary.Add(trainerAirfare);
                summary.Add(hotelBookings);
                summary.Add(trainerClaims);
                summary.Add(RegionalExp);
                summary.Add(coursewareMaterial);
                summary.Add(grossProfit);
                ViewData["Summary"] = summary;

                return View(financialTransactionList.OrderBy(x => x.TrainingConfirmationID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult FinancialTransactionDetail(int Id, string trainingConfirmationID, Int16? country, decimal? totalRevenueAmount, short? month, int? year)
        {
            var currencyList = dbContext.Lookups.Where(x => x.LookupCategory == "Currency").ToList();
            var countryList = new BranchBO().GetList();
            FinancialTransactionsVM financialtransaction = new FinancialTransactionsVM();
            if (Id == -1)
            {
                financialtransaction = new FinancialTransactionsVM { FinancialTransactionId = -1, TrainingConfirmationID = trainingConfirmationID, Country = country, TotalRevenueAmount = totalRevenueAmount, TotalRevenueLocalAmount = totalRevenueAmount, Month = month, Year = year, CurrencyCode = "USD", CurrencyExRate = 1 };
                ViewBag.Title = "New Financial Transaction";

            }
            else
            {
                ViewBag.Title = "Update Financial Transaction";
                financialtransaction = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == Id && x.IsActive == true)
                                       .Select(x => new FinancialTransactionsVM()
                                       {
                                           FinancialTransactionId = x.FinancialTransactionId,
                                           TrainingConfirmationID = x.TrainingConfirmationID,
                                           Country = x.Country,
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
                                           TrainerClaimsAmount = x.TrainerClaimsAmount,
                                           TrainerClaimsLocalAmount = x.TrainerClaimsLocalAmount,
                                           TrainerClaimsReferenceDoc = x.TrainerClaimsReferenceDoc,
                                           TrainerClaimsRemarks = x.TrainerClaimsRemarks,
                                           GrossProfit = x.GrossProfit,
                                           ProfitAndLossPercent = x.ProfitAndLossPercent,
                                           Year = x.Year,
                                           Month = x.Month,
                                           IsActive = true,
                                           IsSubmit = x.IsSubmit,

                                           ModifiedBy = USER_ID,
                                           ModifiedOn = UTILITY.SINGAPORETIME,
                                       }).FirstOrDefault();
            }
            ViewData["CurrencyList"] = currencyList;
            ViewData["CountryData"] = countryList;
            return View(financialtransaction);
        }

        [HttpPost]
        public ActionResult SaveFinancialTransactionDetail(FinancialTransactionsVM ftInfo)
        {
            try
            {
                FinancialTransaction financialtransactions = new FinancialTransaction();
                if (ftInfo.FinancialTransactionId == -1)
                {
                    financialtransactions.FinancialTransactionId = ftInfo.FinancialTransactionId;
                    financialtransactions.TrainingConfirmationID = ftInfo.TrainingConfirmationID;
                    financialtransactions.Country = ftInfo.Country;
                    financialtransactions.CurrencyCode = ftInfo.CurrencyCode;
                    financialtransactions.CurrencyExRate = ftInfo.CurrencyExRate;

                    financialtransactions.TotalRevenueAmount = ftInfo.TotalRevenueAmount;
                    financialtransactions.TotalRevenueLocalAmount = ftInfo.TotalRevenueLocalAmount;

                    if (ftInfo.TotalRevenueFileName != null && ftInfo.TotalRevenueFileName.ContentLength > 0)
                    {
                        financialtransactions.TotalRevenueReferenceDoc = ftInfo.TotalRevenueFileName.FileName;
                    }
                    else
                        financialtransactions.TotalRevenueReferenceDoc = null;
                    financialtransactions.TotalRevenueRemarks = ftInfo.TotalRevenueRemarks;

                    financialtransactions.TrainerExpensesAmount = ftInfo.TrainerExpensesAmount;
                    financialtransactions.TrainerExpensesLocalAmount = ftInfo.TrainerExpensesLocalAmount;
                    if (ftInfo.TrainerExpensesFileName != null && ftInfo.TrainerExpensesFileName.ContentLength > 0)
                    {
                        financialtransactions.TrainerExpensesReferenceDoc = ftInfo.TrainerExpensesFileName.FileName;
                    }
                    else
                        financialtransactions.TrainerExpensesReferenceDoc = null;
                    financialtransactions.TrainerExpensesRemarks = ftInfo.TrainerExpensesRemarks;

                    financialtransactions.TrainerTravelExpensesAmount = ftInfo.TrainerTravelExpensesAmount;
                    financialtransactions.TrainerTravelExpensesLocalAmount = ftInfo.TrainerTravelExpensesLocalAmount;
                    if (ftInfo.TrainerTravelExpensesFileName != null && ftInfo.TrainerTravelExpensesFileName.ContentLength > 0)
                    {
                        financialtransactions.TrainerTravelExpensesReferenceDoc = ftInfo.TrainerTravelExpensesFileName.FileName;
                    }
                    else
                        financialtransactions.TrainerTravelExpensesReferenceDoc = null;
                    financialtransactions.TrainerTravelExpensesRemarks = ftInfo.TrainerTravelExpensesRemarks;

                    financialtransactions.LocalExpensesAmount = ftInfo.LocalExpensesAmount;
                    financialtransactions.LocalExpensesLocalAmount = ftInfo.LocalExpensesLocalAmount;
                    if (ftInfo.LocalExpensesFileName != null && ftInfo.LocalExpensesFileName.ContentLength > 0)
                    {
                        financialtransactions.LocalExpensesReferenceDoc = ftInfo.LocalExpensesFileName.FileName;
                    }
                    else
                        financialtransactions.LocalExpensesReferenceDoc = null;
                    financialtransactions.LocalExpensesRemarks = ftInfo.LocalExpensesRemarks;

                    financialtransactions.CoursewareMaterialAmount = ftInfo.CoursewareMaterialAmount;
                    financialtransactions.CoursewareMaterialLocalAmount = ftInfo.CoursewareMaterialLocalAmount;
                    if (ftInfo.CoursewareMaterialFileName != null && ftInfo.CoursewareMaterialFileName.ContentLength > 0)
                    {
                        financialtransactions.CoursewareMaterialReferenceDoc = ftInfo.CoursewareMaterialFileName.FileName;
                    }
                    else
                        financialtransactions.CoursewareMaterialReferenceDoc = null;
                    financialtransactions.CoursewareMaterialRemarks = ftInfo.CoursewareMaterialRemarks;

                    financialtransactions.MiscExpensesAmount = ftInfo.MiscExpensesAmount;
                    financialtransactions.MiscExpensesLocalAmount = ftInfo.MiscExpensesLocalAmount;
                    if (ftInfo.MiscExpensesFileName != null && ftInfo.MiscExpensesFileName.ContentLength > 0)
                    {
                        financialtransactions.MiscExpensesReferenceDoc = ftInfo.MiscExpensesFileName.FileName;
                    }
                    else
                        financialtransactions.MiscExpensesReferenceDoc = null;
                    financialtransactions.MiscExpensesRemarks = ftInfo.MiscExpensesRemarks;

                    financialtransactions.TrainerClaimsAmount = ftInfo.TrainerClaimsAmount;
                    financialtransactions.TrainerClaimsLocalAmount = ftInfo.TrainerClaimsLocalAmount;
                    if (ftInfo.TrainerClaimsFileName != null && ftInfo.TrainerClaimsFileName.ContentLength > 0)
                    {
                        financialtransactions.TrainerClaimsReferenceDoc = ftInfo.TrainerClaimsFileName.FileName;
                    }
                    else
                        financialtransactions.TrainerClaimsReferenceDoc = null;
                    financialtransactions.TrainerClaimsRemarks = ftInfo.TrainerClaimsRemarks;


                    financialtransactions.GrossProfit = ftInfo.GrossProfit;
                    financialtransactions.ProfitAndLossPercent = ftInfo.ProfitAndLossPercent;
                    financialtransactions.Year = ftInfo.Year;
                    financialtransactions.Month = ftInfo.Month;
                    financialtransactions.IsActive = true;
                    financialtransactions.IsSubmit = ftInfo.IsSubmit;

                    financialtransactions.CreatedBy = USER_ID;
                    financialtransactions.CreatedOn = UTILITY.SINGAPORETIME;
                    dbContext.FinancialTransactions.Add(financialtransactions);
                    dbContext.SaveChanges();

                    if (ftInfo.TotalRevenueFileName != null && ftInfo.TotalRevenueFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + financialtransactions.FinancialTransactionId + "/TotalRevenue/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TotalRevenueFileName.SaveAs(path + ftInfo.TotalRevenueFileName.FileName);
                    }
                    if (ftInfo.TrainerExpensesFileName != null && ftInfo.TrainerExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + financialtransactions.FinancialTransactionId + "/TrainerExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TrainerExpensesFileName.SaveAs(path + ftInfo.TrainerExpensesFileName.FileName);
                    }
                    if (ftInfo.TrainerTravelExpensesFileName != null && ftInfo.TrainerTravelExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + financialtransactions.FinancialTransactionId + "/TrainerTravelExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TrainerTravelExpensesFileName.SaveAs(path + ftInfo.TrainerTravelExpensesFileName.FileName);
                    }
                    if (ftInfo.LocalExpensesFileName != null && ftInfo.LocalExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + financialtransactions.FinancialTransactionId + "/LocalExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.LocalExpensesFileName.SaveAs(path + ftInfo.LocalExpensesFileName.FileName);
                    }
                    if (ftInfo.CoursewareMaterialFileName != null && ftInfo.CoursewareMaterialFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + financialtransactions.FinancialTransactionId + "/CoursewareMaterial/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.CoursewareMaterialFileName.SaveAs(path + ftInfo.CoursewareMaterialFileName.FileName);
                    }
                    if (ftInfo.MiscExpensesFileName != null && ftInfo.MiscExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + financialtransactions.FinancialTransactionId + "/MiscExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.MiscExpensesFileName.SaveAs(path + ftInfo.MiscExpensesFileName.FileName);
                    }

                    if (ftInfo.TrainerClaimsFileName != null && ftInfo.TrainerClaimsFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + financialtransactions.FinancialTransactionId + "/TrainerClaims/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TrainerClaimsFileName.SaveAs(path + ftInfo.TrainerClaimsFileName.FileName);
                    }
                }
                else
                {
                    financialtransactions = dbContext.FinancialTransactions.Where(x => x.FinancialTransactionId == ftInfo.FinancialTransactionId).FirstOrDefault();


                    financialtransactions.FinancialTransactionId = ftInfo.FinancialTransactionId;
                    financialtransactions.TrainingConfirmationID = ftInfo.TrainingConfirmationID;
                    financialtransactions.Country = ftInfo.Country;
                    financialtransactions.Year = ftInfo.Year;
                    financialtransactions.Month = ftInfo.Month;
                    financialtransactions.CurrencyCode = ftInfo.CurrencyCode;
                    financialtransactions.CurrencyExRate = ftInfo.CurrencyExRate;

                    financialtransactions.TotalRevenueAmount = ftInfo.TotalRevenueAmount;
                    financialtransactions.TotalRevenueLocalAmount = ftInfo.TotalRevenueLocalAmount;
                    if (ftInfo.TotalRevenueFileName != null && ftInfo.TotalRevenueFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + ftInfo.FinancialTransactionId + "/TotalRevenue/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TotalRevenueFileName.SaveAs(path + ftInfo.TotalRevenueFileName.FileName);
                        financialtransactions.TotalRevenueReferenceDoc = ftInfo.TotalRevenueFileName.FileName;
                    }
                    financialtransactions.TotalRevenueRemarks = ftInfo.TotalRevenueRemarks;

                    financialtransactions.TrainerExpensesAmount = ftInfo.TrainerExpensesAmount;
                    financialtransactions.TrainerExpensesLocalAmount = ftInfo.TrainerExpensesLocalAmount;
                    if (ftInfo.TrainerExpensesFileName != null && ftInfo.TrainerExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + ftInfo.FinancialTransactionId + "/TrainerExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TrainerExpensesFileName.SaveAs(path + ftInfo.TrainerExpensesFileName.FileName);
                        financialtransactions.TrainerExpensesReferenceDoc = ftInfo.TrainerExpensesFileName.FileName;
                    }
                    financialtransactions.TrainerExpensesRemarks = ftInfo.TrainerExpensesRemarks;

                    financialtransactions.TrainerTravelExpensesAmount = ftInfo.TrainerTravelExpensesAmount;
                    financialtransactions.TrainerTravelExpensesLocalAmount = ftInfo.TrainerTravelExpensesLocalAmount;
                    if (ftInfo.TrainerTravelExpensesFileName != null && ftInfo.TrainerTravelExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + ftInfo.FinancialTransactionId + "/TrainerTravelExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TrainerTravelExpensesFileName.SaveAs(path + ftInfo.TrainerTravelExpensesFileName.FileName);
                        financialtransactions.TrainerTravelExpensesReferenceDoc = ftInfo.TrainerTravelExpensesFileName.FileName;
                    }
                    financialtransactions.TrainerTravelExpensesRemarks = ftInfo.TrainerTravelExpensesRemarks;

                    financialtransactions.LocalExpensesAmount = ftInfo.LocalExpensesAmount;
                    financialtransactions.LocalExpensesLocalAmount = ftInfo.LocalExpensesLocalAmount;
                    if (ftInfo.LocalExpensesFileName != null && ftInfo.LocalExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + ftInfo.FinancialTransactionId + "/LocalExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.LocalExpensesFileName.SaveAs(path + ftInfo.LocalExpensesFileName.FileName);
                        financialtransactions.LocalExpensesReferenceDoc = ftInfo.LocalExpensesFileName.FileName;
                    }
                    financialtransactions.LocalExpensesRemarks = ftInfo.LocalExpensesRemarks;

                    financialtransactions.CoursewareMaterialAmount = ftInfo.CoursewareMaterialAmount;
                    financialtransactions.CoursewareMaterialLocalAmount = ftInfo.CoursewareMaterialLocalAmount;
                    if (ftInfo.CoursewareMaterialFileName != null && ftInfo.CoursewareMaterialFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + ftInfo.FinancialTransactionId + "/CoursewareMaterial/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.CoursewareMaterialFileName.SaveAs(path + ftInfo.CoursewareMaterialFileName.FileName);
                        financialtransactions.CoursewareMaterialReferenceDoc = ftInfo.CoursewareMaterialFileName.FileName;
                    }
                    financialtransactions.CoursewareMaterialRemarks = ftInfo.CoursewareMaterialRemarks;

                    financialtransactions.MiscExpensesAmount = ftInfo.MiscExpensesAmount;
                    financialtransactions.MiscExpensesLocalAmount = ftInfo.MiscExpensesLocalAmount;
                    if (ftInfo.MiscExpensesFileName != null && ftInfo.MiscExpensesFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + ftInfo.FinancialTransactionId + "/MiscExpenses/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.MiscExpensesFileName.SaveAs(path + ftInfo.MiscExpensesFileName.FileName);
                        financialtransactions.MiscExpensesReferenceDoc = ftInfo.MiscExpensesFileName.FileName;
                    }
                    financialtransactions.MiscExpensesRemarks = ftInfo.MiscExpensesRemarks;

                    financialtransactions.TrainerClaimsAmount = ftInfo.TrainerClaimsAmount;
                    financialtransactions.TrainerClaimsLocalAmount = ftInfo.TrainerClaimsLocalAmount;
                    if (ftInfo.TrainerClaimsFileName != null && ftInfo.TrainerClaimsFileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/ft_" + ftInfo.FinancialTransactionId + "/TrainerClaims/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        ftInfo.TrainerClaimsFileName.SaveAs(path + ftInfo.TrainerClaimsFileName.FileName);
                        financialtransactions.TrainerClaimsReferenceDoc = ftInfo.TrainerClaimsFileName.FileName;
                    }
                    financialtransactions.TrainerClaimsRemarks = ftInfo.TrainerClaimsRemarks;

                    financialtransactions.GrossProfit = ftInfo.GrossProfit;
                    financialtransactions.ProfitAndLossPercent = ftInfo.ProfitAndLossPercent;
                    financialtransactions.IsActive = true;
                    financialtransactions.IsSubmit = ftInfo.IsSubmit;

                    financialtransactions.ModifiedBy = USER_ID;
                    financialtransactions.ModifiedOn = UTILITY.SINGAPORETIME;
                    dbContext.Entry(financialtransactions).State = EntityState.Modified;
                    dbContext.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("FinancialTransactionList", new { month = ftInfo.Month, year = ftInfo.Year });
        }

        [HttpGet]
        public JsonResult GetCurrency(Int16 Id)
        {
            var country = new BranchBO().GetList().Where(x => x.IsActive == true && x.BranchID == Id).FirstOrDefault();
            var currencyCode = dbContext.Lookups.Where(x => x.LookupCategory == "Currency" && x.MappingCode == country.BranchCode).FirstOrDefault();

            return Json(currencyCode == null ? "" : currencyCode.LookupCode, JsonRequestBehavior.AllowGet);

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
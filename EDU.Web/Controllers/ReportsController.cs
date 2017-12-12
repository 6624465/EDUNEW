using EDU.Web;
using EDU.Web.Models;
using EDU.Web.ViewModels.OperationalTransaction;
using EDU.Web.ViewModels.ReportsModel;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace EDU.Web.Reports.Controllers
{
    [SessionFilter]
    public class ReportsController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();

        public ActionResult Achievement()
        {
            return View();
        }
        public ActionResult Operationaltransaction(Int16 country, int month, int year)
        {
            try
            {
                var list = dbContext.usp_OperationalTransactionReport(year).ToList();

                OperationalTransactionReportVM reportList = new OperationalTransactionReportVM();
                if (list != null)
                {
                    reportList.otReportByMonth = list.Where(m => m.Month == month && m.Country == country)
                        .Select(x => new OperationalTransactionReportByMonth
                        {
                            ParticularName = x.ParticularName,
                            Amount = x.Amount,
                            Month = x.Month,
                            Country = x.Country,
                            Year = year,
                            CountryName = x.CountryName
                        }).ToList();

                    reportList.otReportByYear = list.Select(x => new OperationalTransactionReportByYear
                    {
                        Country = x.Country,
                        CountryName = x.CountryName,
                        JanAmount = list.Where(y => y.Month == 1 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 1 && y.Country == country).Sum(z => z.Amount) : 0,
                        FebAmount = list.Where(y => y.Month == 2 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 2 && y.Country == country).Sum(z => z.Amount) : 0,
                        MarAmount = list.Where(y => y.Month == 3 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 3 && y.Country == country).Sum(z => z.Amount) : 0,
                        AprAmount = list.Where(y => y.Month == 4 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 4 && y.Country == country).Sum(z => z.Amount) : 0,
                        MayAmount = list.Where(y => y.Month == 5 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 5 && y.Country == country).Sum(z => z.Amount) : 0,
                        JunAmount = list.Where(y => y.Month == 6 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 6 && y.Country == country).Sum(z => z.Amount) : 0,
                        JulAmount = list.Where(y => y.Month == 7 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 7 && y.Country == country).Sum(z => z.Amount) : 0,
                        AugAmount = list.Where(y => y.Month == 8 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 8 && y.Country == country).Sum(z => z.Amount) : 0,
                        SepAmount = list.Where(y => y.Month == 9 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 9 && y.Country == country).Sum(z => z.Amount) : 0,
                        OctAmount = list.Where(y => y.Month == 10 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 10 && y.Country == country).Sum(z => z.Amount) : 0,
                        NovAmount = list.Where(y => y.Month == 11 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 11 && y.Country == country).Sum(z => z.Amount) : 0,
                        DecAmount = list.Where(y => y.Month == 12 && y.Country == country).Count() > 0 ? list.Where(y => y.Month == 12 && y.Country == country).Sum(z => z.Amount) : 0,
                    }).FirstOrDefault();
                }

                ViewData["CountryData"] = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();

                if (reportList.otReportByYear == null)
                    reportList.otReportByYear = new OperationalTransactionReportByYear();
                if (reportList.otReportByMonth == null)
                    reportList.otReportByMonth = new List<OperationalTransactionReportByMonth>();
                return View(reportList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult OperationalExpenses(int year)
        {
            try
            {
                var list = dbContext.usp_OperationalTransactionReport(year).ToList();

                List<OperationalTransactionReportByYear> reportListbyYear = new List<OperationalTransactionReportByYear>();
                var countrylist = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();

                foreach (var item in countrylist)
                {
                    if (list.Where(x => x.Country == item.BranchID).Count() == 0)
                    {
                        //reportListbyYear.Add(new OperationalTransactionReportByYear()
                        //{
                        //    Country = item.BranchID,
                        //    CountryName = item.BranchName,
                        //    JanAmount = 0,
                        //    FebAmount = 0,
                        //    MarAmount = 0,
                        //    AprAmount = 0,
                        //    MayAmount = 0,
                        //    JunAmount = 0,
                        //    JulAmount = 0,
                        //    AugAmount = 0,
                        //    SepAmount = 0,
                        //    OctAmount = 0,
                        //    NovAmount = 0,
                        //    DecAmount = 0
                        //});
                    }
                    else
                    {
                        reportListbyYear.Add(new OperationalTransactionReportByYear()
                        {
                            Country = item.BranchID,
                            CountryName = item.BranchName,
                            Year = year,
                            JanAmount = list.Where(y => y.Month == 1 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 1 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            FebAmount = list.Where(y => y.Month == 2 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 2 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            MarAmount = list.Where(y => y.Month == 3 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 3 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            AprAmount = list.Where(y => y.Month == 4 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 4 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            MayAmount = list.Where(y => y.Month == 5 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 5 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            JunAmount = list.Where(y => y.Month == 6 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 6 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            JulAmount = list.Where(y => y.Month == 7 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 7 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            AugAmount = list.Where(y => y.Month == 8 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 8 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            SepAmount = list.Where(y => y.Month == 9 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 9 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            OctAmount = list.Where(y => y.Month == 10 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 10 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            NovAmount = list.Where(y => y.Month == 11 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 11 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                            DecAmount = list.Where(y => y.Month == 12 && y.Country == item.BranchID).Count() > 0 ? list.Where(y => y.Month == 12 && y.Country == item.BranchID).Sum(z => z.Amount) : 0,
                        });
                    }
                }

                return View(reportListbyYear);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DashboardOperationaltransaction(Int16 month, int year)
        {
            try
            {
                var list = dbContext.usp_OperationalTransactionReport(year).ToList();

                List<OperationalTransactionReportByMonth> reportListbyMonth = new List<OperationalTransactionReportByMonth>();

                var countrylist = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();

                foreach (var item in countrylist)
                {
                    if (list.Where(x => x.Country == item.BranchID && x.Month == month).Count() == 0)
                    {
                        reportListbyMonth.Add(new OperationalTransactionReportByMonth()
                        {
                            ParticularName = "",
                            Amount = 0,
                            Month = month,
                            Country = item.BranchID,
                            Year = year,
                            CountryName = item.BranchName
                        });
                    }
                    else
                    {
                        foreach (var it in list.Where(x => x.Country == item.BranchID && x.Month == month))
                        {
                            reportListbyMonth.Add(new OperationalTransactionReportByMonth()
                            {
                                ParticularName = it.ParticularName,
                                Amount = it.Amount,
                                Month = month,
                                Country = item.BranchID,
                                Year = year,
                                CountryName = item.BranchName
                            });
                        }
                    }
                }

                ViewData["CountryData"] = countrylist;
                return View(reportListbyMonth);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult TotalRevenue(int year)
        {
            List<Revenue> RevenueList = dbContext.Revenues.Where(x => x.IsActive == true && x.Year == year).ToList();
            var countrylist = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();
            List<TotalRevenueVM> list = new List<TotalRevenueVM>();

            foreach (var item in countrylist)
            {
                if (RevenueList.Where(x => x.Country == item.BranchID).Count() == 0)
                {
                    list.Add(new TotalRevenueVM()
                    {
                        Year = year,
                        Country = item.BranchID,
                        CountryName = item.BranchName,
                        TotalRevenue = 0,
                        AchievedRevenue = 0,
                        YetToAchieveRevenue = 0
                    });
                }
                else
                {
                    decimal? totalrevenue = 0;
                    decimal? achievedRevenue = 0;
                    decimal? yetToAchieveRevenue = 0;

                    totalrevenue = RevenueList.Where(x => x.Country == item.BranchID).Sum(a=>a.YearlyTarget);                    

                    foreach (var tcitem in dbContext.TrainingConfirmations.Where(x => x.Country == item.BranchID && x.StartDate.Value.Year == year && x.IsActive == true).ToList())
                    {
                        foreach (var regitem in dbContext.Registrations.Where(x => x.TrainingConfirmationID == tcitem.TrainingConfirmationID && x.IsActive == true).ToList())
                        {
                            achievedRevenue += (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3);
                        }
                    }
                    yetToAchieveRevenue = totalrevenue - achievedRevenue;

                    list.Add(new TotalRevenueVM()
                    {
                        Year = year,
                        Country = item.BranchID,
                        CountryName = item.BranchName,
                        TotalRevenue = totalrevenue,
                        AchievedRevenue = achievedRevenue,
                        YetToAchieveRevenue = yetToAchieveRevenue
                    });
                }
            }
            ViewData["CountryData"] = countrylist;
            return View(list);
        }
        public ActionResult DashBoard()
        {
            return View();
        }

    }

}
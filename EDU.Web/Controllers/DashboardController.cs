using EDU.Web;
using EDU.Web.Models;
using EDU.Web.ViewModels.FinancialTransactionModel;
using EDU.Web.ViewModels.OperationalTransaction;
using EDU.Web.ViewModels.DashboardModel;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace EDU.Web.Dashboard.Controllers
{
    [SessionFilter]
    public class DashboardController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();

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
                        reportListbyYear.Add(new OperationalTransactionReportByYear()
                        {
                            Country = item.BranchID,
                            CountryName = item.BranchName,
                            JanAmount = 0,
                            FebAmount = 0,
                            MarAmount = 0,
                            AprAmount = 0,
                            MayAmount = 0,
                            JunAmount = 0,
                            JulAmount = 0,
                            AugAmount = 0,
                            SepAmount = 0,
                            OctAmount = 0,
                            NovAmount = 0,
                            DecAmount = 0
                        });
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

                return View(reportListbyYear.OrderBy(x => x.CountryName));
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

                var countrylist = new BranchBO().GetList().Where(x => x.IsActive == true).OrderBy(x => x.BranchName).ToList();

                foreach (var item in countrylist)
                {
                    if (list.Where(x => x.Country == item.BranchID && x.Month == month).Count() == 0)
                    {
                        //reportListbyMonth.Add(new OperationalTransactionReportByMonth()
                        //{
                        //    ParticularName = "",
                        //    Amount = 0,
                        //    Month = month,
                        //    Country = item.BranchID,
                        //    Year = year,
                        //    CountryName = item.BranchName
                        //});
                    }
                    else
                    {
                        foreach (var it in list.Where(x => x.Country == item.BranchID && x.Month == month))
                        {
                            reportListbyMonth.Add(new OperationalTransactionReportByMonth()
                            {
                                ParticularId = it.ParticularsId,
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


                var ParticularsList = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars").OrderBy(y => y.LookupID).ToList();

                foreach (var item in countrylist)
                {
                    foreach (var pitem in ParticularsList)
                    {
                        if (reportListbyMonth.Where(x => x.ParticularName == pitem.LookupDescription && x.Country == item.BranchID).Count() == 0)
                        {
                            reportListbyMonth.Add(new OperationalTransactionReportByMonth()
                            {
                                ParticularId = pitem.LookupID,
                                ParticularName = pitem.LookupDescription,
                                Amount = 0,
                                Month = month,
                                Country = item.BranchID,
                                Year = year,
                                CountryName = item.BranchName
                            });
                        }
                    }
                }


                ViewData["CountryData"] = countrylist;
                ViewData["ParticularsData"] = ParticularsList;
                return View(reportListbyMonth.OrderBy(x => x.ParticularId));
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
                switch (item.BranchName.ToUpper())
                {
                    case "BANGLADESH":
                        item.BranchCode = "BD";
                        break;
                    case "CAMBODIA":
                        item.BranchCode = "KH";
                        break;
                    case "INDIA":
                        item.BranchCode = "IN";
                        break;
                    case "LAOS":
                        item.BranchCode = "LA";
                        break;
                    case "MYANMAR":
                        item.BranchCode = "MM";
                        break;
                    case "PAKISTAN":
                        item.BranchCode = "PK";
                        break;
                    case "PHILLIPINES":
                        item.BranchCode = "PH";
                        break;
                    case "SINGAPORE":
                        item.BranchCode = "SG";
                        break;
                    case "SRILANKA":
                        item.BranchCode = "SL";
                        break;
                    case "VIETNAM":
                        item.BranchCode = "VN";
                        break;
                    default:
                        break;
                }

                if (RevenueList.Where(x => x.Country == item.BranchID).Count() == 0)
                {
                    list.Add(new TotalRevenueVM()
                    {
                        Year = year,
                        Country = item.BranchID,
                        CountryName = item.BranchCode,
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

                    totalrevenue = RevenueList.Where(x => x.Country == item.BranchID).Sum(a => a.YearlyTarget);

                    foreach (var tcitem in dbContext.TrainingConfirmations.Where(x => x.Country == item.BranchID && x.Year == year && x.IsActive == true).ToList())
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
                        CountryName = item.BranchCode,
                        TotalRevenue = totalrevenue,
                        AchievedRevenue = achievedRevenue,
                        YetToAchieveRevenue = yetToAchieveRevenue
                    });
                }
            }
            ViewData["CountryData"] = countrylist;

            List<blist> bcList = new List<blist>() { new blist() { id = 1, code = "BD" },
                                                    new blist() { id = 2, code = "KH" },
                                                    new blist() { id = 3, code = "MM" },
                                                    new blist() { id = 4, code = "SL" },
                                                    new blist() { id = 5, code = "PK" },
                                                    new blist() { id = 6, code = "PH" },
                                                    new blist() { id = 7, code = "VN" },
                                                    new blist() { id = 8, code = "LA" },
                                                    new blist() { id = 9, code = "SG" },
                                                    new blist() { id = 10, code = "IN" }};
            list= list.Join(bcList, a => a.CountryName, b => b.code, (a, b) => new TotalRevenueVM() {Year= a.Year, Country=a.Country, CountryName=a.CountryName, TotalRevenue=a.TotalRevenue, AchievedRevenue=a.AchievedRevenue, YetToAchieveRevenue=a.YetToAchieveRevenue, id=b.id }).OrderBy(x=>x.id).ToList<TotalRevenueVM>();
            

            return View(list);
        }
        public ActionResult DashBoard(Int16 country, int month, int year)
        {
            List<Revenue> RevenueList = dbContext.Revenues.Where(x => x.IsActive == true && x.Year == year && x.Country == country).ToList();
            var countrylist = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();
            var productList = new EduProductBO().GetList().Where(x => x.IsActive == true).ToList();

            ProductRevenueReportVM report = new ProductRevenueReportVM();
            List<ProductTotalRevenueByMonthVM> prodTotalRevenueByMonth = new List<ProductTotalRevenueByMonthVM>();
            List<ProductRevenueByMonthVM> prodRevenueByMonth = new List<ProductRevenueByMonthVM>();
            List<ProductRevenueByMonthVM> prodRevenuToAchieve = new List<ProductRevenueByMonthVM>();

            foreach (var item in productList)
            {
                string cname = countrylist.Where(x => x.BranchID == country).FirstOrDefault().BranchName;
                var revenueitem = RevenueList.Where(x => x.Country == country && x.Product == item.Id).FirstOrDefault();

                decimal? janamount = 0;
                decimal? febamount = 0;
                decimal? maramount = 0;
                decimal? apramount = 0;
                decimal? mayamount = 0;
                decimal? juneamount = 0;
                decimal? julyamount = 0;
                decimal? augamount = 0;
                decimal? sepamount = 0;
                decimal? octamount = 0;
                decimal? novamount = 0;
                decimal? decamount = 0;

                foreach (var tcitem in dbContext.TrainingConfirmations.Where(x => x.Country == country && x.Year == year && x.IsActive == true && x.Product == item.Id).ToList())
                {
                    foreach (var regitem in dbContext.Registrations.Where(x => x.TrainingConfirmationID == tcitem.TrainingConfirmationID && x.IsActive == true).ToList())
                    {
                        janamount += tcitem.Month == 1 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        febamount += tcitem.Month == 2 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        maramount += tcitem.Month == 3 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        apramount += tcitem.Month == 4 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        mayamount += tcitem.Month == 5 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        juneamount += tcitem.Month == 6 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        julyamount += tcitem.Month == 7 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        augamount += tcitem.Month == 8 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        sepamount += tcitem.Month == 9 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        octamount += tcitem.Month == 10 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        novamount += tcitem.Month == 11 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                        decamount += tcitem.Month == 12 ? (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3) : 0;
                    }
                }

                decimal? achieved = janamount + febamount + maramount + apramount + mayamount + juneamount +
                                    julyamount + augamount + sepamount + octamount + novamount + decamount;

                decimal? total = revenueitem != null ? revenueitem.YearlyTarget : 0;
                decimal? monthly = revenueitem != null ? revenueitem.MonthlyTarget : 0;
                short cMonth = Convert.ToInt16(DateTime.Now.Month);
                decimal? yetToAchieve = cMonth == 12 ? monthly : (total - achieved) / (12 - cMonth);

                prodRevenueByMonth.Add(new ProductRevenueByMonthVM()
                {
                    Country = country,
                    CountryName = cname,
                    ProductName = item.ProductName,
                    Year = year,
                    TotalRevenue = total,
                    MonthlyTarget = yetToAchieve,
                    JanAmount = janamount,
                    FebAmount = febamount,
                    MarAmount = maramount,
                    AprAmount = apramount,
                    MayAmount = mayamount,
                    JuneAmount = juneamount,
                    JulyAmount = julyamount,
                    AugAmount = augamount,
                    SepAmount = sepamount,
                    OctAmount = octamount,
                    NovAmount = novamount,
                    DecAmount = decamount
                });



                if (RevenueList.Where(x => x.Country == country && x.Product == item.Id).Count() == 0)
                {
                    prodTotalRevenueByMonth.Add(new ProductTotalRevenueByMonthVM()
                    {
                        Year = year,
                        Country = country,
                        CountryName = cname,
                        ProductName = item.ProductName,
                        TotalRevenue = 0,
                        AchievedRevenue = 0
                    });
                }
                else
                {
                    decimal? achievedRevenue = 0;
                    foreach (var tcitem in dbContext.TrainingConfirmations.Where(x => x.Country == country && x.Year == year && x.Month == month && x.IsActive == true && x.Product == item.Id).ToList())
                    {
                        foreach (var regitem in dbContext.Registrations.Where(x => x.TrainingConfirmationID == tcitem.TrainingConfirmationID && x.IsActive == true).ToList())
                        {
                            achievedRevenue += (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3);
                        }
                    }

                    prodTotalRevenueByMonth.Add(new ProductTotalRevenueByMonthVM()
                    {
                        Year = year,
                        Country = country,
                        CountryName = revenueitem == null ? "" : revenueitem.CountryName,
                        ProductName = item.ProductName,
                        TotalRevenue = revenueitem == null ? 0 : revenueitem.MonthlyTarget,
                        AchievedRevenue = achievedRevenue
                    });
                }
            }


            foreach (var item in prodRevenueByMonth)
            {
                decimal? achieved = item.JanAmount + item.FebAmount + item.MarAmount + item.AprAmount + item.MayAmount + item.JuneAmount +
                                    item.JulyAmount + item.AugAmount + item.SepAmount + item.OctAmount + item.NovAmount + item.DecAmount;

                decimal? total = item.TotalRevenue;
                short cMonth = Convert.ToInt16(DateTime.Now.Month);
                decimal? yetToAchieve = cMonth == 12 ? 0 : (total - achieved) / (12 - cMonth);

                prodRevenuToAchieve.Add(new ProductRevenueByMonthVM()
                {
                    Month = cMonth,
                    Country = item.Country,
                    CountryName = item.CountryName,
                    ProductName = item.ProductName,
                    Year = item.Year,
                    TotalRevenue = item.TotalRevenue,
                    MonthlyTarget = item.MonthlyTarget,
                    JanAmount = item.JanAmount,
                    FebAmount = (cMonth >= 2 ? item.FebAmount : yetToAchieve),
                    MarAmount = (cMonth >= 3 ? item.MarAmount : yetToAchieve),
                    AprAmount = (cMonth >= 4 ? item.AprAmount : yetToAchieve),
                    MayAmount = (cMonth >= 5 ? item.MayAmount : yetToAchieve),
                    JuneAmount = (cMonth >= 6 ? item.JuneAmount : yetToAchieve),
                    JulyAmount = (cMonth >= 7 ? item.JulyAmount : yetToAchieve),
                    AugAmount = (cMonth >= 8 ? item.AugAmount : yetToAchieve),
                    SepAmount = (cMonth >= 9 ? item.SepAmount : yetToAchieve),
                    OctAmount = (cMonth >= 10 ? item.OctAmount : yetToAchieve),
                    NovAmount = (cMonth >= 11 ? item.NovAmount : yetToAchieve),
                    DecAmount = (cMonth >= 12 ? item.DecAmount : yetToAchieve)
                });
            }

            report.productTotalRevenueByMonth = prodTotalRevenueByMonth;
            report.productRevenueByMonth = prodRevenueByMonth;
            report.productRevenueToAchieve = prodRevenuToAchieve;

            ViewData["CountryData"] = countrylist;
            return View(report);
        }
        public ActionResult Achievement(int year)
        {
            List<Revenue> RevenueList = dbContext.Revenues.Where(x => x.IsActive == true && x.Year == year).ToList();

            var countrylist = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();

            List<ProductTotalRevenueVM> list = new List<ProductTotalRevenueVM>();

            foreach (var item in RevenueList)
            {
                decimal? achievedRevenue = 0;
                foreach (var tcitem in dbContext.TrainingConfirmations.Where(x => x.Country == item.Country && x.Year == year && x.IsActive == true && x.Product == item.Product).ToList())
                {
                    foreach (var regitem in dbContext.Registrations.Where(x => x.TrainingConfirmationID == tcitem.TrainingConfirmationID && x.IsActive == true).ToList())
                    {
                        achievedRevenue += (regitem.Payment1 == null ? 0 : regitem.Payment1) + (regitem.Payment2 == null ? 0 : regitem.Payment2) + (regitem.Payment3 == null ? 0 : regitem.Payment3);
                    }
                }

                list.Add(new ProductTotalRevenueVM()
                {
                    Year = year,
                    Country = item.Country,
                    CountryName = item.CountryName,
                    ProductId = item.Product,
                    ProductName = item.ProductName,
                    TotalRevenue = item.YearlyTarget,
                    AchievedRevenue = achievedRevenue
                });
            }


            var productList = new EduProductBO().GetList().Where(x => x.IsActive == true).ToList();
            foreach (var item in countrylist)
            {
                foreach (var pitem in productList)
                {
                    if (list.Where(x => x.ProductId == pitem.Id && x.Country == item.BranchID).Count() == 0)
                    {
                        list.Add(new ProductTotalRevenueVM()
                        {
                            Year = year,
                            Country = item.BranchID,
                            CountryName = item.BranchName,
                            ProductId = pitem.Id,
                            ProductName = pitem.ProductName,
                            TotalRevenue = 0,
                            AchievedRevenue = 0
                        });
                    }
                }
            }

            ViewData["CountryData"] = countrylist;
            ViewData["ProductData"] = productList;
            return View(list.OrderBy(x => x.ProductId));
        }

        public ActionResult ProfitAndLoss(Int16 month, int year, Int16 country)
        {
            try
            {
                List<FinancialTransactionsVM> financialTransactionReport = new List<FinancialTransactionsVM>();
                List<TrainingConfirmation> trainingConfirmationList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true && x.Year == year && x.Month == month && x.Country == country).ToList();

                List<string> list = trainingConfirmationList.Select(x => x.TrainingConfirmationID).ToList();
                List<FinancialTransaction> financialTransaction1List = dbContext.FinancialTransactions
                                                                         .Where(x => x.IsActive == true && list.Contains(x.TrainingConfirmationID)).ToList();
                var countryList = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();

                financialTransactionReport = financialTransaction1List
                    .Select(x => new FinancialTransactionsVM
                    {
                        TrainingConfirmationID = x.TrainingConfirmationID,
                        Country = x.Country,
                        CountryName = countryList.Where(y => y.BranchID == x.Country).FirstOrDefault().BranchName,
                        TotalRevenueAmount = x.TotalRevenueAmount == null ? 0 : x.TotalRevenueAmount,
                        TrainerExpensesAmount = x.TrainerExpensesAmount == null ? 0 : x.TrainerExpensesAmount,
                        TrainerTravelExpensesAmount = x.TrainerTravelExpensesAmount == null ? 0 : x.TrainerTravelExpensesAmount,
                        LocalExpensesAmount = x.LocalExpensesAmount == null ? 0 : x.LocalExpensesAmount,
                        CoursewareMaterialAmount = x.CoursewareMaterialAmount == null ? 0 : x.CoursewareMaterialAmount,
                        MiscExpensesAmount = x.MiscExpensesAmount == null ? 0 : x.MiscExpensesAmount,
                        TrainerClaimsAmount = x.TrainerClaimsAmount == null ? 0 : x.TrainerClaimsAmount,
                        GrossProfit = x.GrossProfit == null ? 0 : x.GrossProfit,
                        ProfitAndLossPercent = x.ProfitAndLossPercent == null ? 0 : x.ProfitAndLossPercent,
                        Year = year,
                        Month = month
                    })
                    .ToList();


                foreach (var item in trainingConfirmationList)
                {
                    if (financialTransaction1List.Where(x => x.TrainingConfirmationID == item.TrainingConfirmationID).Count() == 0)
                    {
                        decimal? TotalAmount = dbContext.Registrations.Where(x => x.TrainingConfirmationID == item.TrainingConfirmationID && x.IsActive == true).Sum(y => y.TotalAmount);

                        financialTransactionReport.Add(new FinancialTransactionsVM()
                        {
                            TrainingConfirmationID = item.TrainingConfirmationID,
                            Country = item.Country,
                            CountryName = countryList.Where(y => y.BranchID == item.Country).FirstOrDefault().BranchName,
                            TotalRevenueAmount = TotalAmount == null ? 0 : TotalAmount,
                            TrainerExpensesAmount = 0,
                            TrainerTravelExpensesAmount = 0,
                            LocalExpensesAmount = 0,
                            CoursewareMaterialAmount = 0,
                            MiscExpensesAmount = 0,
                            TrainerClaimsAmount = 0,
                            GrossProfit = 0,
                            ProfitAndLossPercent = 0,
                            Year = year,
                            Month = month
                        });
                    }
                }

                ViewData["CountryData"] = countryList;
                return View(financialTransactionReport.OrderBy(x => x.TrainingConfirmationID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class blist
    {
        public int id { get; set; }
        public string code { get; set; }
    }
}
using EDU.Web;
using EDU.Web.Models;
using EDU.Web.ViewModels.OperationalTransaction;
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
            var list = dbContext.usp_OperationalTransactionReport(year).ToList();

            OperationalTransactionReportVM reportList = new OperationalTransactionReportVM();
            if (list != null)
            {
                reportList.otReportByMonth = list.Where(m => m.Month == month && m.Country == country)
                    .Select(x => new OperationalTransactionReportByMonth
                    {
                        ParticularName = x.ParticularName,
                        Amount = x.Amount,
                        Month=x.Month,
                        Country=x.Country,
                        Year = year
                    }).ToList();

                reportList.otReportByYear = list.Select(x => new OperationalTransactionReportByYear
                {
                    JanAmount = list.Where(y => y.Month == 1).Count() > 0 ? list.Where(y => y.Month == 1).Sum(z => z.Amount) : 0,
                    FebAmount = list.Where(y => y.Month == 2).Count() > 0 ? list.Where(y => y.Month == 2).Sum(z => z.Amount) : 0,
                    MarAmount = list.Where(y => y.Month == 3).Count() > 0 ? list.Where(y => y.Month == 3).Sum(z => z.Amount) : 0,
                    AprAmount = list.Where(y => y.Month == 4).Count() > 0 ? list.Where(y => y.Month == 4).Sum(z => z.Amount) : 0,
                    MayAmount = list.Where(y => y.Month == 5).Count() > 0 ? list.Where(y => y.Month == 5).Sum(z => z.Amount) : 0,
                    JunAmount = list.Where(y => y.Month == 6).Count() > 0 ? list.Where(y => y.Month == 6).Sum(z => z.Amount) : 0,
                    JulAmount = list.Where(y => y.Month == 7).Count() > 0 ? list.Where(y => y.Month == 7).Sum(z => z.Amount) : 0,
                    AugAmount = list.Where(y => y.Month == 8).Count() > 0 ? list.Where(y => y.Month == 8).Sum(z => z.Amount) : 0,
                    SepAmount = list.Where(y => y.Month == 9).Count() > 0 ? list.Where(y => y.Month == 9).Sum(z => z.Amount) : 0,
                    OctAmount = list.Where(y => y.Month == 10).Count() > 0 ? list.Where(y => y.Month == 10).Sum(z => z.Amount) : 0,
                    NovAmount = list.Where(y => y.Month == 11).Count() > 0 ? list.Where(y => y.Month == 11).Sum(z => z.Amount) : 0,
                    DecAmount = list.Where(y => y.Month == 12).Count() > 0 ? list.Where(y => y.Month == 12).Sum(z => z.Amount) : 0,
                }).FirstOrDefault();
            }

            ViewData["CountryData"] = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();

            return View(reportList);
        }
        public ActionResult OperationalExpenses()
        {
            return View();
        }
        public ActionResult DashboardOperationaltransaction()
        {
            return View();
        }
        public ActionResult TotalRevenue()
        {
            return View();
        }
        public ActionResult DashBoard()
        {
            return View();
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    public class ReportController : Controller
    {
        public static string REPORTSUBFOLDER = WebConfigurationManager.AppSettings["ReportPath"].ToString();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ViewReport()
        { 
            //string branchID, string dateFrom, string dateTo, string Url
            var url = "DNeX.DeclarationK1Chit"; // Url; 
            ViewBag.BranchID = "1001001";
            ViewBag.DeclarationNo = "324523453245";
            //ViewBag.DateFrom = dateFrom;
            //ViewBag.DateTo = dateTo;
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }
        public ActionResult VendorsAndTrainersReport()
        {
            var url = "Edu.VendorsAndTrainersReport";
            ViewBag.Title = "Vendors And Trainers Report";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }

        public ActionResult ProductsReport()
        {
            var url = "Edu.ProductReport";
            ViewBag.Title = "Products Report";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }

        public ActionResult CoursesReport()
        {
            var url = "Edu.CourseReport";
            ViewBag.Title = "Courses Report";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }

        public ActionResult RegionalLeadsReport(int year, int? month)
        {
            var url = "Edu.RegionalLeads";
            ViewBag.Title = "Regional Leads Report";
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }

        public ActionResult EventConfirmationReport(int year, int? month)
        {
            var url = "Edu.EventConfirmation";
            ViewBag.Title = "Event Confirmation Report";
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }

        public ActionResult RevenueReport(int year, int country)
        {
            var url = "Edu.RevenueReport";
            ViewBag.Title = "Revenue Report";
            ViewBag.year = year;
            ViewBag.country = country;
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }

        public ActionResult ProfitAndLossReport(int country, int year, int? month)
        {
            var url = "Edu.ProfitAndLossReport";
            ViewBag.Title = "Profit And Loss Report";
            ViewBag.country = country;
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }

        public ActionResult DashboardOperationalTransactionReport(int? country, int year, int month)
        {
            var url = "Edu.DashboardOperationalTransaction";
            ViewBag.Title = "Dashboard Operational Transaction Report";
            ViewBag.country = country;
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, url);
            return PartialView("ViewReport");
        }
    }
}
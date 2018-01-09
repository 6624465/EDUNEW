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
            return View();
        }


    }
}
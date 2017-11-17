using EDU.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace EDU.Web.Reports.Controllers
{
    //[RouteArea("Reports")]
    //[ActionFilters.SessionFilter]
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult MaterialInward()
        {
            ViewBag.LoginID = BRANCH_ID;
            return View();
        }
        //public ActionResult MaterialInwards(string dateFrom, string dateTo)
        //{
        //    ViewBag.DateFrom = "01/09/2017";
        //    ViewBag.DateTo = "25/09/2017";
        //    ViewBag.ReportSource = "MaterialInwardReport";
        //    ViewBag.BranchID = BRANCH_ID;
        //    ViewBag.Url = string.Format("{0}{1}{2}", "http://sql5002.smarterasp.net/ReportServer", UTILITY.REPORTSUBFOLDER,"/RMAReports/MaterialInwardSSRSReport?BranchID=10&DateFrom=08/09/2017&DateTo=09/25/2017");
        //    var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
        //    return PartialView(path);
        //}
        public ActionResult MaterialInwards(int branchID, string dateFrom, string dateTo)
        {
            string url = "MaterialInwardSSRSReport";
            NetworkCredential nwc = new NetworkCredential("ragsarma-001", "n0ki@3310", "ifc");
            WebClient client = new WebClient();
            client.Credentials = nwc;
            string reportURL = string.Format("{0}?{1}{2}&rs:Command=Render&rs:Format=PDF&rc:Toolbar=false&rc:Parameters=false&BranchID={3}&DateFrom={4}&DateTo={5}", "http://sql5002.smarterasp.net/ReportServer",
                "/ragsarma-001/RMAReports/", url, branchID, dateFrom, dateTo);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Report.pdf",
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(client.DownloadData(reportURL), "application/pdf");
        }
        public ActionResult ManagementReports()
        {
            return View();
        }

        public ActionResult Achievement()
        {
            return View();
        }
        public ActionResult Operationaltransaction()
        {
            return View();
        }
        public ActionResult OperationalExpenses()
        {
            return View();
        }

    }
}
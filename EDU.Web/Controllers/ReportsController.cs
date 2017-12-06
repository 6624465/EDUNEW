using EDU.Web;
using EDU.Web.Models;
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
        public ActionResult Operationaltransaction()
        {
            return View();
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
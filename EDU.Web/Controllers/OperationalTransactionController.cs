using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    public class OperationalTransactionController : Controller
    {
        // GET: OperationalTransaction
        public ActionResult OperationalTransactionList(short? month)
        {
            return View();
        }

        public PartialViewResult OperationalTransaction()
        {
            return PartialView();
        }
    }
}
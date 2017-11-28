using EDU.Web.Models;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
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
            ViewData["CountryData"] = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();
            ViewData["CurrencyList"] = new LookupBO().GetList().Where(x => x.LookupCategory == "Currency").ToList();
            ViewData["TrainingConfirmation"] = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();

            return View();
        }
    }
}
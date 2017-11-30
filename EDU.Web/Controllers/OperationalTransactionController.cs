﻿using EDU.Web.Models;
using EDU.Web.ViewModels.OperationalTransactionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class OperationalTransactionController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();

        // GET: OperationalTransaction
        public ActionResult OperationalTransactionList(short? month)
        {
            List<OperationalTransactionVM> list = dbContext.OperationalTransactions
                .Where(x => x.IsActive == true && x.Month == month)
                .Select(y=> new OperationalTransactionVM {
                    OperationalTransactionId = y.OperationalTransactionId,
                    CategoryId = y.CategoryId,
                    ParticularsId = y.ParticularsId,
                    Month = y.Month,
                    Amount = y.Amount,
                    IsActive = y.IsActive,
                    CreatedBy = y.CreatedBy,
                    CreatedOn = y.CreatedOn,
                    ModifiedBy = y.ModifiedBy,
                    ModifiedOn = y.ModifiedOn,
                    CategoryIdDesc = dbContext.Lookups.Where(c => c.LookupID == y.CategoryId).FirstOrDefault().LookupDescription,
                    ParticularsIdDesc = dbContext.Lookups.Where(c => c.LookupID == y.ParticularsId).FirstOrDefault().LookupDescription,
                    MonthDesc = dbContext.Lookups.Where(c => c.LookupID == y.CategoryId).FirstOrDefault().LookupDescription
                })
                .ToList();
            return View();
        }

        public PartialViewResult OperationalTransaction(int operationalTransactionId)
        {

            ViewData["CategoryData"] = dbContext.Lookups.Where(x => x.LookupCategory == "OperationalTransaction").ToList();
            ViewData["ParticularsData"] = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars").ToList();
            if (operationalTransactionId == -1)
            {
                ViewBag.Title = "New Operational Transaction";
                return PartialView(new OperationalTransaction { OperationalTransactionId = -1, IsActive = true });
            }
            else
            {
                ViewBag.Title = "Update Operational Transaction";
                return PartialView(dbContext.OperationalTransactions.Where(x=>x.OperationalTransactionId== operationalTransactionId));
            }
        }
    }
}
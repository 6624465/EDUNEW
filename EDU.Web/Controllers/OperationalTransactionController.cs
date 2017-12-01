using EDU.Web.Models;
using EDU.Web.ViewModels.OperationalTransactionModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                .Select(y => new OperationalTransactionVM
                {
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
                    ParticularsIdDesc = dbContext.Lookups.Where(c => c.LookupID == y.ParticularsId).FirstOrDefault().LookupDescription
                    //,
                    //MonthDesc = dbContext.Lookups.Where(c => c.LookupID == y.CategoryId).FirstOrDefault().LookupDescription
                })
                .ToList();
            return View(list);
        }

        public PartialViewResult OperationalTransaction(int? operationalTransactionId, short? month)
        {

            ViewData["CategoryData"] = dbContext.Lookups.Where(x => x.LookupCategory == "OperationalTransaction").ToList();
            //ViewData["ParticularsData"] = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars").ToList();
            if (operationalTransactionId == -1)
            {
                ViewBag.Title = "New Operational Transaction";
                ViewData["ParticularsData"] = null;
                return PartialView(new OperationalTransaction { OperationalTransactionId = -1, IsActive = true, Month = month });
            }
            else
            {
                ViewBag.Title = "Update Operational Transaction";
                OperationalTransaction operationalTransaction = dbContext.OperationalTransactions.Where(x => x.OperationalTransactionId == operationalTransactionId).FirstOrDefault();
                string categorymappingCode = dbContext.Lookups.Where(x => x.LookupCategory == "OperationalTransaction" && x.LookupID == operationalTransaction.CategoryId).FirstOrDefault().LookupCode;
                ViewData["ParticularsData"] = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars" && x.MappingCode == categorymappingCode).ToList();
                return PartialView(operationalTransaction);
            }
        }

        [HttpGet]
        public JsonResult GetParticularsByCategory(int CategoryId)
        {
            string categorymappingCode = dbContext.Lookups.Where(x => x.LookupID == CategoryId).FirstOrDefault().LookupCode;
            ViewData["ParticularsData"] = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars" && x.MappingCode == categorymappingCode).ToList();

            return Json(ViewData["ParticularsData"], JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveOperationalTransaction(OperationalTransaction operationalTransaction)
        {
            try
            {
                if (operationalTransaction.OperationalTransactionId == -1)
                {
                    operationalTransaction.CreatedBy = USER_ID;
                    operationalTransaction.CreatedOn = UTILITY.SINGAPORETIME;
                    operationalTransaction.IsActive = true;

                    dbContext.OperationalTransactions.Add(operationalTransaction);
                }

                else
                {
                    OperationalTransaction operationalTransactionInfo = dbContext.OperationalTransactions.
                        Where(x => x.OperationalTransactionId == operationalTransaction.OperationalTransactionId).FirstOrDefault();

                    operationalTransactionInfo.CategoryId = operationalTransaction.CategoryId;
                    operationalTransactionInfo.ParticularsId = operationalTransaction.ParticularsId;
                    operationalTransactionInfo.Month = operationalTransaction.Month;
                    operationalTransactionInfo.Amount = operationalTransaction.Amount;

                    operationalTransactionInfo.IsActive = true;

                    operationalTransactionInfo.ModifiedBy = USER_ID;
                    operationalTransactionInfo.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(operationalTransactionInfo).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("OperationalTransactionList", new { month = operationalTransaction.Month });
        }

        [HttpPost]
        public ActionResult DeleteOperationalTransaction(int? operationalTransactionId)
        {
            OperationalTransaction operationalTransactioninfo = dbContext.OperationalTransactions.
                   Where(x => x.OperationalTransactionId == operationalTransactionId).FirstOrDefault();
            if (operationalTransactioninfo != null)
            {
                operationalTransactioninfo.IsActive = false;
                dbContext.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
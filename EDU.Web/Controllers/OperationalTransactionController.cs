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
        public ActionResult OperationalTransactionList(short? month, int year)
        {
            List<OperationalTransactionVM> list = dbContext.OperationalTransactions
                .Where(x => x.IsActive == true && x.Month == month && x.Year == year)
                .Select(y => new OperationalTransactionVM
                {
                    OperationalTransactionId = y.OperationalTransactionId,
                    CategoryId = y.CategoryId,
                    ParticularsId = y.ParticularsId,
                    Country = y.Country,
                    CountryName=y.CountryName,
                    Month = y.Month,
                    Year = y.Year,
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

        public PartialViewResult OperationalTransaction(int? operationalTransactionId, short? month, int? year)
        {

            ViewData["CategoryData"] = dbContext.Lookups.Where(x => x.LookupCategory == "OperationalTransaction").ToList();
            //ViewData["ParticularsData"] = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars").ToList();
            if (operationalTransactionId == -1)
            {
                ViewBag.Title = "New Operational Transaction";
                ViewData["ParticularsData"] = null;
                return PartialView(new OperationalTransaction { OperationalTransactionId = -1, IsActive = true, Month = month, Year = year });
            }
            else
            {
                ViewBag.Title = "Update Operational Transaction";
                OperationalTransaction operationalTransaction = dbContext.OperationalTransactions.Where(x => x.OperationalTransactionId == operationalTransactionId).FirstOrDefault();
                string categorymappingCode = dbContext.Lookups.Where(x => x.LookupCategory == "OperationalTransaction" && x.LookupID == operationalTransaction.CategoryId).FirstOrDefault().LookupCode;

                var list = dbContext.OperationalTransactions.Where(x => x.Month == month && x.Year == year && x.OperationalTransactionId != operationalTransaction.OperationalTransactionId && x.IsActive == true).ToList();

                var result = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars" && x.MappingCode == categorymappingCode).ToList();
                foreach (var item in list)
                {
                    result = result.Where(x => x.LookupID != item.ParticularsId).ToList();

                }
                ViewData["ParticularsData"] = result;

                return PartialView(operationalTransaction);
            }
        }

        [HttpGet]
        public JsonResult GetParticularsByCategory(int CategoryId, short? month, int? year)
        {

            string categorymappingCode = dbContext.Lookups.Where(x => x.LookupID == CategoryId).FirstOrDefault().LookupCode;
            var result = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars" && x.MappingCode == categorymappingCode).ToList();
            var list = dbContext.OperationalTransactions.Where(x => x.Month == month && x.Year == year && x.IsActive == true).ToList();
            foreach (var item in list)
            {
                result = result.Where(x => x.LookupID != item.ParticularsId).ToList();

            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
                    operationalTransactionInfo.Year = operationalTransaction.Year;
                    operationalTransactionInfo.Amount = operationalTransaction.Amount;
                    operationalTransactionInfo.Country = operationalTransaction.Country;
                    operationalTransactionInfo.CountryName = operationalTransaction.CountryName;

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
            return RedirectToAction("OperationalTransactionList", new { month = operationalTransaction.Month, year = operationalTransaction.Year });
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
            return RedirectToAction("OperationalTransactionList", new { month = operationalTransactioninfo.Month, year = operationalTransactioninfo.Year });
        }


        [HttpGet]
        public ActionResult OperationalTransactionReport(int year)
        {
            try
            {
                int month = DateTime.Now.Month;
                int fYear;
                int cYear;

                if (month < 4)
                {
                    cYear = year - 1;
                    fYear = year;
                }
                else
                {
                    fYear = year + 1;
                    cYear = year;
                }


                List<OperationalTransactionReportVM> list = dbContext.OperationalTransactions
                    .Where(x => x.IsActive == true)
                    .Select(y => new OperationalTransactionReportVM
                    {
                        CategoryId = y.CategoryId,
                        ParticularsId = y.ParticularsId,
                        Country = y.Country,
                        CountryName = y.CountryName,
                        AprAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 4 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 4 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        MayAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 5 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 5 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        JuneAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 6 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 6 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        JulyAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 7 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 7 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        AugAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 8 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 8 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        SepAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 9 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 9 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        OctAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 10 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 10 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        NovAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 11 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 11 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        DecAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 12 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == cYear && x.Month == 12 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,  //fYear
                    JanAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == fYear && x.Month == 1 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == fYear && x.Month == 1 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        FebAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == fYear && x.Month == 2 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == fYear && x.Month == 2 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        MarAmount = dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == fYear && x.Month == 3 && x.ParticularsId == y.ParticularsId).FirstOrDefault() == null ? 0 : dbContext.OperationalTransactions.Where(x => x.IsActive == true && x.Year == fYear && x.Month == 3 && x.ParticularsId == y.ParticularsId).FirstOrDefault().Amount,
                        CategoryIdDesc = dbContext.Lookups.Where(c => c.LookupID == y.CategoryId).FirstOrDefault().LookupDescription,
                        ParticularsIdDesc = dbContext.Lookups.Where(c => c.LookupID == y.ParticularsId).FirstOrDefault().LookupDescription

                    }).Distinct()
                    .ToList();
                List<OperationalTransactionReportVM> list1 = new List<OperationalTransactionReportVM>();
                foreach (var item in list)
                {
                    item.YTD = item.AprAmount + item.MayAmount + item.JuneAmount + item.JulyAmount + item.AugAmount + item.SepAmount +
                              item.OctAmount + item.NovAmount + item.DecAmount + item.JanAmount + item.FebAmount + item.MarAmount;

                    list1.Add(item);
                }

                List<decimal?> summary = new List<decimal?>();

                summary.Add(list1.Sum(x => x.AprAmount));
                summary.Add(list1.Sum(x => x.MayAmount));
                summary.Add(list1.Sum(x => x.JuneAmount));
                summary.Add(list1.Sum(x => x.JulyAmount));
                summary.Add(list1.Sum(x => x.AugAmount));
                summary.Add(list1.Sum(x => x.SepAmount));
                summary.Add(list1.Sum(x => x.OctAmount));
                summary.Add(list1.Sum(x => x.NovAmount));
                summary.Add(list1.Sum(x => x.DecAmount));
                summary.Add(list1.Sum(x => x.JanAmount));
                summary.Add(list1.Sum(x => x.FebAmount));
                summary.Add(list1.Sum(x => x.MarAmount));
                summary.Add(list1.Sum(x => x.YTD));
                ViewData["Summary"] = summary;

                return View(list1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
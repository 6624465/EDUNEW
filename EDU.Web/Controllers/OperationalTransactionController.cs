using EDU.Web.Models;
using EDU.Web.ViewModels.OperationalTransactionModel;
using EZY.EDU.BusinessFactory;
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
        public ActionResult OperationalTransactionList(short? month, int year, int country)
        {
            List<OperationalTransactionVM> list = dbContext.OperationalTransactions
                .Where(x => x.IsActive == true && x.Month == month && x.Year == year && x.Country == country)
                .Select(y => new OperationalTransactionVM
                {
                    OperationalTransactionId = y.OperationalTransactionId,
                    CategoryId = y.CategoryId,
                    ParticularsId = y.ParticularsId,
                    Country = y.Country,
                    CountryName = y.CountryName,
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
                })
                .ToList();

            decimal? totalAmount = list.Sum(x => x.Amount);


            ViewData["totalAmount"] = totalAmount;

            ViewData["CountryData"] = new BranchBO().GetList();
            return View(list);
        }

        [HttpGet]
        public ActionResult OperationalTransaction(int? operationId, short? month, int? year, short country)
        {
            OTVM operationalTransactionvm = new OTVM();
            if (operationId == -1)
            {
                ViewBag.Title = "New Operational Transaction";
                operationalTransactionvm.OperationalTransactionId = -1;
                operationalTransactionvm.OperationId = -1;

                operationalTransactionvm.OperationalExpId = 1100;
                operationalTransactionvm.OtherExpId = 1101;
                operationalTransactionvm.Year = year;
                operationalTransactionvm.Month = month;

                operationalTransactionvm.SalariesId = 1200;
                operationalTransactionvm.SalariesAmount = 0;

                operationalTransactionvm.TravellingexpId = 1201;
                operationalTransactionvm.TravellingexpAmount = 0;

                operationalTransactionvm.RentalId = 1202;
                operationalTransactionvm.RentalAmount = 0;

                operationalTransactionvm.TelephoneexpId = 1203;
                operationalTransactionvm.TelephoneexpAmount = 0;

                operationalTransactionvm.CourierchargesId = 1204;
                operationalTransactionvm.CourierchargesAmount = 0;

                operationalTransactionvm.InsuranceId = 1205;
                operationalTransactionvm.InsuranceAmount = 0;

                operationalTransactionvm.UtilityId = 1206;
                operationalTransactionvm.UtilityAmount = 0;

                operationalTransactionvm.MarketingexpId = 1207;
                operationalTransactionvm.MarketingexpAmount = 0;

                operationalTransactionvm.DepreciationId = 1208;
                operationalTransactionvm.DepreciationAmount = 0;

                operationalTransactionvm.LegalexpId = 1300;
                operationalTransactionvm.LegalexpAmount = 0;

                operationalTransactionvm.RepairmaintenanceId = 1301;
                operationalTransactionvm.RepairmaintenanceAmount = 0;

                operationalTransactionvm.BankchargesId = 1302;
                operationalTransactionvm.BankchargesAmount = 0;

                operationalTransactionvm.PrintingstationeryId = 1303;
                operationalTransactionvm.PrintingstationeryAmount = 0;

                operationalTransactionvm.StaffwelfareId = 1304;
                operationalTransactionvm.StaffwelfareAmount = 0;

                operationalTransactionvm.OtherexpensesincomeId = 1305;
                operationalTransactionvm.OtherexpensesincomeAmount = 0;

                operationalTransactionvm.Country = country;
            }
            else
            {
                ViewBag.Title = "Update Financial Transaction";

            }

            ViewData["CountryData"] = new BranchBO().GetList();
            return View(operationalTransactionvm);
        }

        [HttpPost]
        public ActionResult SaveOperationalTransaction(OTVM otvm)
        {
            try
            {
                string countryname = otvm.Country > 0 ? new BranchBO().GetList().Where(x => x.BranchID == otvm.Country).FirstOrDefault().BranchName : "";
                OperationalTransaction operationalTransactionInfo = new OperationalTransaction();

                if (otvm.OperationId == -1)
                {
                    operationalTransactionInfo.OperationId = Convert.ToInt32(otvm.Year.ToString() + otvm.Month.ToString());
                    operationalTransactionInfo.Country = otvm.Country;
                    operationalTransactionInfo.CountryName = countryname;
                    operationalTransactionInfo.Year = otvm.Year;
                    operationalTransactionInfo.Month = otvm.Month;

                    operationalTransactionInfo.CreatedBy = USER_ID;
                    operationalTransactionInfo.CreatedOn = UTILITY.SINGAPORETIME;
                    operationalTransactionInfo.IsActive = true;

                    Dictionary<int, decimal?> PerticularsList = new Dictionary<int, decimal?>();
                    PerticularsList.Add(otvm.SalariesId, otvm.SalariesAmount);
                    PerticularsList.Add(otvm.TravellingexpId, otvm.TravellingexpAmount);
                    PerticularsList.Add(otvm.RentalId, otvm.RentalAmount);
                    PerticularsList.Add(otvm.TelephoneexpId, otvm.TelephoneexpAmount);
                    PerticularsList.Add(otvm.CourierchargesId, otvm.CourierchargesAmount);
                    PerticularsList.Add(otvm.InsuranceId, otvm.InsuranceAmount);
                    PerticularsList.Add(otvm.UtilityId, otvm.UtilityAmount);
                    PerticularsList.Add(otvm.MarketingexpId, otvm.MarketingexpAmount);
                    PerticularsList.Add(otvm.DepreciationId, otvm.DepreciationAmount);
                    PerticularsList.Add(otvm.LegalexpId, otvm.LegalexpAmount);
                    PerticularsList.Add(otvm.RepairmaintenanceId, otvm.RepairmaintenanceAmount);
                    PerticularsList.Add(otvm.BankchargesId, otvm.BankchargesAmount);
                    PerticularsList.Add(otvm.PrintingstationeryId, otvm.PrintingstationeryAmount);
                    PerticularsList.Add(otvm.StaffwelfareId, otvm.StaffwelfareAmount);
                    PerticularsList.Add(otvm.OtherexpensesincomeId, otvm.OtherexpensesincomeAmount);

                    foreach (KeyValuePair<int, decimal?> perticulars in PerticularsList)
                    {
                        operationalTransactionInfo.CategoryId = (perticulars.Key >= 1200 && perticulars.Key < 1300) ? 1100 : 1101;
                        operationalTransactionInfo.ParticularsId = perticulars.Key;
                        operationalTransactionInfo.Amount = perticulars.Value.Value;

                        dbContext.OperationalTransactions.Add(operationalTransactionInfo);

                        dbContext.SaveChanges();
                    }
                }

                else
                {
                    //operationalTransactionInfo = dbContext.OperationalTransactions.
                    //    Where(x => x.OperationalTransactionId == operationalTransaction.OperationalTransactionId).FirstOrDefault();

                    //operationalTransactionInfo.CountryName = countryname;
                    //operationalTransactionInfo.CategoryId = operationalTransaction.CategoryId;
                    //operationalTransactionInfo.ParticularsId = operationalTransaction.ParticularsId;
                    //operationalTransactionInfo.Month = operationalTransaction.Month;
                    //operationalTransactionInfo.Year = operationalTransaction.Year;
                    //operationalTransactionInfo.Amount = operationalTransaction.Amount;
                    //operationalTransactionInfo.Country = operationalTransaction.Country;
                    //operationalTransactionInfo.CountryName = operationalTransaction.CountryName;

                    //operationalTransactionInfo.IsActive = true;

                    //operationalTransactionInfo.ModifiedBy = USER_ID;
                    //operationalTransactionInfo.ModifiedOn = UTILITY.SINGAPORETIME;

                    //dbContext.Entry(operationalTransactionInfo).State = EntityState.Modified;
                }
                //dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("OperationalTransactionList", new { month = otvm.Month, year = otvm.Year, country = otvm.Country });
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
            return RedirectToAction("OperationalTransactionList", new { month = operationalTransactioninfo.Month, year = operationalTransactioninfo.Year, country = operationalTransactioninfo.Country });
        }


        [HttpGet]
        public ActionResult OperationalTransactionReport(int year, int country)
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
                    .Where(x => x.IsActive == true && x.Country == country && x.Year == year)
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
                ViewData["CountryData"] = new BranchBO().GetList();
                return View(list1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
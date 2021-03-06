﻿using EDU.Web.Models;
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
                    OperationId = y.OperationId,
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
                ViewBag.Title = "Update Operational Transaction";
                //operationalTransactionvm.OperationalTransactionId = -1;
                List<OperationalTransaction> otList = dbContext.OperationalTransactions.Where(x => x.OperationId == operationId && x.Country == country && x.IsActive == true).ToList();

                operationalTransactionvm.OperationId = operationId;

                operationalTransactionvm.OperationalExpId = 1100;
                operationalTransactionvm.OtherExpId = 1101;
                operationalTransactionvm.Year = year;
                operationalTransactionvm.Month = month;

                operationalTransactionvm.SalariesId = 1200;
                operationalTransactionvm.SalariesAmount = otList.Where(x => x.ParticularsId == 1200).FirstOrDefault().Amount;

                operationalTransactionvm.TravellingexpId = 1201;
                operationalTransactionvm.TravellingexpAmount = otList.Where(x => x.ParticularsId == 1201).FirstOrDefault().Amount;

                operationalTransactionvm.RentalId = 1202;
                operationalTransactionvm.RentalAmount = otList.Where(x => x.ParticularsId == 1202).FirstOrDefault().Amount;

                operationalTransactionvm.TelephoneexpId = 1203;
                operationalTransactionvm.TelephoneexpAmount = otList.Where(x => x.ParticularsId == 1203).FirstOrDefault().Amount;

                operationalTransactionvm.CourierchargesId = 1204;
                operationalTransactionvm.CourierchargesAmount = otList.Where(x => x.ParticularsId == 1204).FirstOrDefault().Amount;

                operationalTransactionvm.InsuranceId = 1205;
                operationalTransactionvm.InsuranceAmount = otList.Where(x => x.ParticularsId == 1205).FirstOrDefault().Amount;

                operationalTransactionvm.UtilityId = 1206;
                operationalTransactionvm.UtilityAmount = otList.Where(x => x.ParticularsId == 1206).FirstOrDefault().Amount;

                operationalTransactionvm.MarketingexpId = 1207;
                operationalTransactionvm.MarketingexpAmount = otList.Where(x => x.ParticularsId == 1207).FirstOrDefault().Amount;

                operationalTransactionvm.DepreciationId = 1208;
                operationalTransactionvm.DepreciationAmount = otList.Where(x => x.ParticularsId == 1208).FirstOrDefault().Amount;

                operationalTransactionvm.LegalexpId = 1300;
                operationalTransactionvm.LegalexpAmount = otList.Where(x => x.ParticularsId == 1300).FirstOrDefault().Amount;

                operationalTransactionvm.RepairmaintenanceId = 1301;
                operationalTransactionvm.RepairmaintenanceAmount = otList.Where(x => x.ParticularsId == 1301).FirstOrDefault().Amount;

                operationalTransactionvm.BankchargesId = 1302;
                operationalTransactionvm.BankchargesAmount = otList.Where(x => x.ParticularsId == 1302).FirstOrDefault().Amount;

                operationalTransactionvm.PrintingstationeryId = 1303;
                operationalTransactionvm.PrintingstationeryAmount = otList.Where(x => x.ParticularsId == 1303).FirstOrDefault().Amount;

                operationalTransactionvm.StaffwelfareId = 1304;
                operationalTransactionvm.StaffwelfareAmount = otList.Where(x => x.ParticularsId == 1304).FirstOrDefault().Amount;

                operationalTransactionvm.OtherexpensesincomeId = 1305;
                operationalTransactionvm.OtherexpensesincomeAmount = otList.Where(x => x.ParticularsId == 1305).FirstOrDefault().Amount;

                operationalTransactionvm.Country = country;
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
                List<OperationalTransaction> otList = new List<OperationalTransaction>();

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
                    operationalTransactionInfo.OperationId = otvm.OperationId;
                    operationalTransactionInfo.Country = otvm.Country;
                    operationalTransactionInfo.CountryName = countryname;
                    operationalTransactionInfo.Year = otvm.Year;
                    operationalTransactionInfo.Month = otvm.Month;

                    operationalTransactionInfo.ModifiedBy = USER_ID;
                    operationalTransactionInfo.ModifiedOn = UTILITY.SINGAPORETIME;
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

                        OperationalTransaction otInfo = dbContext.OperationalTransactions.
                            Where(x => x.OperationId == otvm.OperationId && x.ParticularsId == perticulars.Key && x.Month == otvm.Month && x.Year == otvm.Year && x.Country == otvm.Country).FirstOrDefault();

                        if (otInfo != null)
                        {
                            otInfo.ModifiedBy = USER_ID;
                            otInfo.ModifiedOn = UTILITY.SINGAPORETIME;
                            otInfo.IsActive = true;

                            otInfo.Amount = perticulars.Value.Value;
                            dbContext.Entry(otInfo).State = EntityState.Modified;
                        }
                        else
                        {
                            otInfo.OperationId = operationalTransactionInfo.OperationId;
                            otInfo.Country = operationalTransactionInfo.Country;
                            otInfo.CountryName = countryname;
                            otInfo.Year = operationalTransactionInfo.Year;
                            otInfo.Month = operationalTransactionInfo.Month;

                            otInfo.CategoryId = (perticulars.Key >= 1200 && perticulars.Key < 1300) ? 1100 : 1101;
                            otInfo.ParticularsId = perticulars.Key;
                            otInfo.Amount = perticulars.Value.Value;

                            otInfo.CreatedBy = USER_ID;
                            otInfo.CreatedOn = UTILITY.SINGAPORETIME;
                            otInfo.IsActive = true;

                            dbContext.OperationalTransactions.Add(otInfo);
                        }
                        dbContext.SaveChanges();
                    }
                }
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
                int fYear= year;
                int cYear= year;

                List<OperationalTransaction> otlist = dbContext.OperationalTransactions
                    .Where(x => x.IsActive == true && x.Country == country && x.Year == year).ToList();

                if (Session["AccessRights"].ToString() == "false")
                {
                    otlist = otlist.Where(x => x.Country == Convert.ToUInt16(Session["BranchId"])).ToList();
                }

                var ParticularsList = dbContext.Lookups.Where(x => x.LookupCategory == "Particulars").OrderBy(y => y.LookupID).ToList();

                List<OperationalTransactionReportVM> list = new List<OperationalTransactionReportVM>();
                foreach (var item in ParticularsList)
                {
                    list.Add(new OperationalTransactionReportVM()
                    {
                        CategoryId = dbContext.Lookups.Where(c => c.LookupCode == item.MappingCode).FirstOrDefault().LookupID,
                        ParticularsId = item.LookupID,
                        //Country = otlist.FirstOrDefault().Country,
                        //CountryName = otlist.FirstOrDefault().CountryName,
                        JanAmount = otlist.Where(x => x.Month == 1 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 1 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        FebAmount = otlist.Where(x => x.Month == 2 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 2 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        MarAmount = otlist.Where(x => x.Month == 3 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 3 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        AprAmount = otlist.Where(x => x.Month == 4 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 4 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        MayAmount = otlist.Where(x => x.Month == 5 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 5 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        JuneAmount = otlist.Where(x => x.Month == 6 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 6 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        JulyAmount = otlist.Where(x => x.Month == 7 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 7 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        AugAmount = otlist.Where(x => x.Month == 8 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 8 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        SepAmount = otlist.Where(x => x.Month == 9 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 9 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        OctAmount = otlist.Where(x => x.Month == 10 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 10 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        NovAmount = otlist.Where(x => x.Month == 11 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 11 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        DecAmount = otlist.Where(x => x.Month == 12 && x.ParticularsId == item.LookupID).FirstOrDefault() == null ? 0 : otlist.Where(x => x.Month == 12 && x.ParticularsId == item.LookupID).FirstOrDefault().Amount,
                        CategoryIdDesc = dbContext.Lookups.Where(c => c.LookupCode == item.MappingCode).FirstOrDefault().LookupDescription,
                        ParticularsIdDesc = item.LookupDescription

                    });
                }

                List<OperationalTransactionReportVM> list1 = new List<OperationalTransactionReportVM>();
                foreach (var item in list)
                {
                    item.YTD = item.JanAmount + item.FebAmount + item.MarAmount + item.AprAmount + item.MayAmount + item.JuneAmount + item.JulyAmount + item.AugAmount + item.SepAmount +
                              item.OctAmount + item.NovAmount + item.DecAmount;

                    list1.Add(item);
                }

                List<decimal?> summary = new List<decimal?>();

                summary.Add(list.Sum(x => x.JanAmount));
                summary.Add(list.Sum(x => x.FebAmount));
                summary.Add(list.Sum(x => x.MarAmount));
                summary.Add(list.Sum(x => x.AprAmount));
                summary.Add(list.Sum(x => x.MayAmount));
                summary.Add(list.Sum(x => x.JuneAmount));
                summary.Add(list.Sum(x => x.JulyAmount));
                summary.Add(list.Sum(x => x.AugAmount));
                summary.Add(list.Sum(x => x.SepAmount));
                summary.Add(list.Sum(x => x.OctAmount));
                summary.Add(list.Sum(x => x.NovAmount));
                summary.Add(list.Sum(x => x.DecAmount));
                summary.Add(list.Sum(x => x.YTD));
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
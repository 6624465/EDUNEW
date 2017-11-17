using EDU.Web.Models;
using EDU.Web.ViewModels.RevenueModel;
using EZY.EDU.BusinessFactory;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    public class RevenueController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();

        [HttpPost]
        public JsonResult SaveRevenueInfo(Revenue revenue)
        {
            revenue.Year = revenue.Year == 0 ? DateTime.Now.Year : revenue.Year;

            revenue.HalfYearlyTarget = revenue.YearlyTarget / 2;
            revenue.QuarterlyTarget = revenue.YearlyTarget / 4;
            revenue.MonthlyTarget = revenue.YearlyTarget / 12;

            revenue.YearlyTargetAmt = revenue.YearlyTarget * revenue.CurrencyExRate;

            revenue.HalfYearlyTargetAmt = revenue.YearlyTargetAmt / 2;
            revenue.QuarterlyTargetAmt = revenue.YearlyTargetAmt / 4;
            revenue.MonthlyTargetAmt = revenue.YearlyTargetAmt / 12;
            revenue.IsActive = true;

            try
            {
                if (revenue.RevenueId == -1)
                {
                    revenue.CreatedBy = USER_ID;
                    revenue.CreatedOn = UTILITY.SINGAPORETIME;
                    dbContext.Revenues.Add(revenue);
                }
                else
                {
                    Revenue revenueInfo = dbContext.Revenues.
                       Where(x => x.RevenueId == revenue.RevenueId).FirstOrDefault();

                    revenueInfo.CurrencyExRate = revenue.CurrencyExRate;

                    revenueInfo.YearlyTarget = revenue.YearlyTarget;
                    revenueInfo.HalfYearlyTarget = revenue.HalfYearlyTarget;
                    revenueInfo.QuarterlyTarget = revenue.QuarterlyTarget;
                    revenueInfo.MonthlyTarget = revenue.MonthlyTarget;

                    revenueInfo.YearlyTargetAmt = revenue.YearlyTargetAmt;

                    revenueInfo.HalfYearlyTargetAmt = revenue.HalfYearlyTargetAmt;
                    revenueInfo.QuarterlyTargetAmt = revenue.QuarterlyTargetAmt;
                    revenueInfo.MonthlyTargetAmt = revenue.MonthlyTargetAmt;
                    revenueInfo.IsActive = true;

                    revenueInfo.ModifiedBy = USER_ID;
                    revenueInfo.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(revenueInfo).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string message = "Saved Successfully.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult updateRevenueInfoByExRate(Revenue revenue)
        {
            List<Revenue> RevenueList = dbContext.Revenues.Where(x => x.IsActive == true && x.Country == revenue.Country && x.Year == revenue.Year).ToList(); //&& x.CurrencyCode == revenue.CurrencyCode

            try
            {
                foreach (Revenue rv in RevenueList)
                {
                    rv.CurrencyExRate = revenue.CurrencyExRate;

                    rv.YearlyTargetAmt = rv.YearlyTarget * revenue.CurrencyExRate;

                    rv.HalfYearlyTargetAmt = rv.YearlyTargetAmt/2;
                    rv.QuarterlyTargetAmt = rv.YearlyTargetAmt/4;
                    rv.MonthlyTargetAmt = rv.YearlyTargetAmt/12;
                    rv.IsActive = true;

                    rv.ModifiedBy = USER_ID;
                    rv.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(rv).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string message = "Currency Ex. Rate Updated Successfully.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetCurrency(Int16 Id)
        {
            var country = new BranchBO().GetList().Where(x => x.IsActive == true && x.BranchID == Id).FirstOrDefault();
            var currencyCode = new LookupBO().GetList().Where(x => x.LookupCategory == "Currency" && x.MappingCode == country.BranchCode).FirstOrDefault();

            return Json(currencyCode == null ? "" : currencyCode.LookupCode, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult RevenueList(Int16 country, int year)
        {
            List<Revenue> List = GetList(country, year);
            return View(List);
        }

        private List<Revenue> GetList(short country, int year)
        {
            List<Revenue> RevenueList = dbContext.Revenues.Where(x => x.IsActive == true && x.Country == country && x.Year == year).ToList();
            var productList = new EduProductBO().GetList().Where(x => x.IsActive == true).ToList();

            ViewData["CountryData"] = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();
            ViewData["CurrencyList"] = new LookupBO().GetList().Where(x => x.LookupCategory == "Currency").ToList();

            var countryrow = ((List<Branch>)ViewData["CountryData"]).Where(x => x.IsActive == true && x.BranchID == country).FirstOrDefault();
            var currencyCode = ((List<Lookup>)ViewData["CurrencyList"]).Where(x => x.LookupCategory == "Currency" && x.MappingCode == countryrow.BranchCode).FirstOrDefault();

            foreach (EduProduct ep in productList)
            {
                if (RevenueList.Where(x => x.Product == ep.Id).Count() == 0)
                    RevenueList.Add(new Revenue() { Product = ep.Id, ProductName = ep.ProductName, RevenueId = -1, CurrencyCode = currencyCode == null ? "" : currencyCode.LookupCode });
            }
            return RevenueList;
        }
    }
}
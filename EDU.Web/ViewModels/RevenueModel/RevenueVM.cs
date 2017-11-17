using EDU.Web.Models;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.RevenueModel
{
    public class RevenueVM
    {
        public Revenue revenueInfo { get; set; }
        public IEnumerable<Branch> countryList { get; set; }
        public IEnumerable<EduProduct> productList { get; set; }
        public IEnumerable<Lookup> currencyList { get; set; }

    }

    public class Revenue2
    {
        public int? RevenueId { get; set; }
        public short? Country { get; set; }
        public string CountryName { get; set; }
        public int? Product { get; set; }
        public string ProductName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? CurrencyExRate { get; set; }
        public int? Year { get; set; }
        public int? YearlyTarget { get; set; }
        public int? HalfYearlyTarget { get; set; }
        public int? QuarterlyTarget { get; set; }
        public int? MonthlyTarget { get; set; }
        public decimal? YearlyTargetAmt { get; set; }
        public decimal? HalfYearlyTargetAmt { get; set; }
        public decimal? QuarterlyTargetAmt { get; set; }
        public decimal? MonthlyTargetAmt { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

}
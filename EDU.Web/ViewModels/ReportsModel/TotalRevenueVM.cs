using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.ReportsModel
{
    public class TotalRevenueVM
    {
        public int Year { get; set; }
        public Int16 Country { get; set; }
        public string CountryName { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? AchievedRevenue { get; set; }
        public decimal? YetToAchieveRevenue { get; set; }
    }
}
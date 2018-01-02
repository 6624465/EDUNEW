using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.DashboardModel
{
    public class ProductRevenueReportVM
    {
        public List<ProductTotalRevenueByMonthVM> productTotalRevenueByMonth { get; set; }
        public List<ProductRevenueByMonthVM> productRevenueByMonth { get; set; }
        public List<ProductRevenueByMonthVM> productRevenueToAchieve { get; set; }
    }

    public class TotalRevenueVM
    {
        public int Year { get; set; }
        public Int16 Country { get; set; }
        public string CountryName { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? AchievedRevenue { get; set; }
        public decimal? YetToAchieveRevenue { get; set; }
    }


    public class ProductTotalRevenueVM
    {
        public int Year { get; set; }
        public Int16 Country { get; set; }
        public string CountryName { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? AchievedRevenue { get; set; }
    }


    public class ProductTotalRevenueByMonthVM
    {
        public short Month { get; set; }
        public int Year { get; set; }
        public Int16 Country { get; set; }
        public string CountryName { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? AchievedRevenue { get; set; }
    }


    public class ProductRevenueByMonthVM
    {
        public short Month { get; set; }
        public int Year { get; set; }
        public string ProductName { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
        public decimal? MonthlyTarget { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? JanAmount { get; set; }
        public decimal? FebAmount { get; set; }
        public decimal? MarAmount { get; set; }
        public decimal? AprAmount { get; set; }
        public decimal? MayAmount { get; set; }
        public decimal? JuneAmount { get; set; }
        public decimal? JulyAmount { get; set; }
        public decimal? AugAmount { get; set; }
        public decimal? SepAmount { get; set; }
        public decimal? OctAmount { get; set; }
        public decimal? NovAmount { get; set; }
        public decimal? DecAmount { get; set; }

    }
}
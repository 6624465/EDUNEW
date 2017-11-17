using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZY.EDU.BusinessFactory;
using EZY.EDU.Contract;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Administration()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult Transactions()
        {
            return View();
        }
        public ActionResult Masters()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Modules()
        {
            return View("Modules");
        }

        [HttpGet]
        public ActionResult Education()
        {
            return View("Education");
        }
        //[HttpGet]
        //public ActionResult Dashboard(Int64 BRANCHID = 10, int MONTH = 4, int YEAR = 2017)
        //{ 
        //    DashBoardData dt = new DashBoardData(); 
        //     try
        //    {
        //        System.Data.IDataReader reader = new DashBoardBO().GetOrderCount(BRANCHID, MONTH, YEAR);

        //        while (reader.Read())
        //        {
        //            dt.MaterialsInwardCount = (int)reader["MaterialsInward"];
        //            dt.year = (int)reader["Year"];
        //            dt.Month = (int)reader["Month"];
        //            dt.Branch = (int)reader["Branch"];
        //        }
        //        //var data = new test();
        //        //data.name = "test";
        //        //data.list = new List<tets1>();
        //        //tets1 t = new tets1();
        //        //t.count = 5;
        //        //data.list.Add(t);
        //        //data.list.Add(t);
        //        //data.list.Add(t);
        //        //data.list.Add(t);
        //        //data.list.Add(t);
        //        ViewBag.ChartData = dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    return View("Dashboard");
        //}
        //[HttpGet]
        //public ActionResult Dashboard()
        //{
        //    short BranchID = BRANCH_ID;
        //    var dashBaordDTO = new DashBoardDTO()
        //    {
        //        InwardCount = new DashBoardBO().GetMaterialInwardCount(BranchID),
        //        OutwardCount = new DashBoardBO().GetMaterialOutwardCount(BranchID),
        //        RMAInwardCount = new DashBoardBO().GetRMAInwardCount(BranchID),
        //        RMAOutwardCount = new DashBoardBO().GetRMAOutwardCount(BranchID),
        //        InwardAndOutward = new DashBoardBO().GetMaterialInAndOutwardCount(BranchID),
        //        RMAInwardAndOutward = new DashBoardBO().GetRMAInAndOutwardCount(BranchID)
        //    };
        //    return View(dashBaordDTO);
        //}
        //[HttpGet]
        //public ActionResult Dashboard1()
        //{
        //    short BranchID = BRANCH_ID;
        //    var dashboardInandOutDTO = new DashboardInandOutDTO()
        //    {
        //        InwardAndOutward = new DashBoardBO().GetMaterialInAndOutwardCount(BranchID),
        //        RMAInwardAndOutward = new DashBoardBO().GetRMAInAndOutwardCount(BranchID)
        //    };
        //    return View(dashboardInandOutDTO);
        //}
        //[HttpGet]
        //public ActionResult Dashboard2()
        //{
        //    short BranchID = BRANCH_ID;
        //    var dashboardForQtyandPriceDTO = new DashboardForQtyandPriceDTO()
        //    {
        //        QtyandPriceDashboardForMaterialIn = new DashBoardBO().GetMaterialInwardSumQtyPrice(BranchID),
        //        QtyandPriceDashboardForMaterialOut = new DashBoardBO().GetMaterialOutwardSumQtyPrice(BranchID),
        //        QtyandPriceDashboardForRMAInward = new DashBoardBO().GetRMAInwardSumQtyPrice(BranchID),
        //        QtyandPriceDashboardForRMAOutward = new DashBoardBO().GetRMAOutwardSumQtyPrice(BranchID)
        //    };
        //    return View(dashboardForQtyandPriceDTO);
        //}
        //[HttpGet]
        //public ActionResult Dashboard3()
        //{
        //    short BranchID = BRANCH_ID;
        //    var dashboardForQtyandPriceDTO = new DashboardForProductandPriceDTO()
        //    {
        //        ProductandPriceDashboardForMaterialIn = new DashBoardBO().GetMaterialInwardProductandPrice(BranchID),
        //        ProductandPriceDashboardForMaterialOut = new DashBoardBO().GetMaterialOutwardProductandPrice(BranchID),
        //        ProductandPriceDashboardForRMAInward = new DashBoardBO().GetRMAInwardProductandPrice(BranchID),
        //        ProductandPriceDashboardForRMAOutward = new DashBoardBO().GetRMAOutwardProductandPrice(BranchID)
        //    };
        //    return View(dashboardForQtyandPriceDTO);
        //}
        [HttpGet]
        public ActionResult Dashboard4()
        {

            return View();
        }
        //[HttpGet]
        //public JsonResult Dashboarddata()
        //{
        //    short BranchID = BRANCH_ID;
        //    var DashBoardForTotalCountofProduct = new DashBoardForTotalCountofProduct()
        //    {
        //        DashboardForTotalListProductwise = new DashBoardBO().TotalMaterialInwardListProductwise(BranchID),
        //        DashBoardForProductListCountMonthWise = new DashBoardBO().MaterialInwardProductListCountMonthWise(BranchID),
        //        TotalproductCount=new DashBoardBO().MaterialInwardProductCount(BranchID),
        //        DashboardForTotalProductsMonthWiseReport=new DashBoardBO().MaterialInwardProductCountMonthWise(BranchID)
        //    };

        //    var distinctCategories = DashBoardForTotalCountofProduct.DashBoardForProductListCountMonthWise
        //                                .Select(x => x.CategoryDescription)
        //                                .Distinct()
        //                                .ToArray();


        //    var finalData = new List<DashBoard4>();
        //    for (var i = 0; i < distinctCategories.Count(); i++)
        //    {
        //        var dashboard4 = new DashBoard4();
        //        dashboard4.name = distinctCategories[i];
        //        dashboard4.id = distinctCategories[i];
        //        var data = (DashBoardForTotalCountofProduct.DashBoardForProductListCountMonthWise
        //                        .Where(x => x.CategoryDescription == distinctCategories[i])
        //                        .Select(x => new DashBoard4Data
        //                        {
        //                            InvoiceMonth = x.InvoiceMonth,
        //                            ProductCode = x.ProductCode
        //                        }).ToDictionary(x => x.InvoiceMonth,
        //                                        y => y.ProductCode));

        //        dashboard4.data = data;
        //        finalData.Add(dashboard4);
        //    }

        //    DashBoardForTotalCountofProduct.data = finalData;
        //    return Json(DashBoardForTotalCountofProduct, JsonRequestBehavior.AllowGet);
        //}
        public class test
        {
            public string name { get; set; }
            public List<tets1> list { get; set; }
        }

        public class tets1
        {
            public int count { get; set; }
        }

        public class Title
        {
            public string text { get; set; }
        }

        public class XAxis
        {
            public List<string> categories { get; set; }
        }

        public class Series
        {
            public List<double> data { get; set; }
        }

        public class RootObject
        {
            public Title title { get; set; }
            public XAxis xAxis { get; set; }
            public List<Series> series { get; set; }
        }
    }
}

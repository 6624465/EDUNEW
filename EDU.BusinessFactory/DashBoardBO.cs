using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
   public class DashBoardBO
    {
        DashboardDAL dashBoardDAL;
        public DashBoardBO()
        {
            dashBoardDAL = new DashboardDAL();
        }
        public System.Data.IDataReader GetOrderCount(Int64 BRANCHID,int MONTH,int YEAR)
        {
            return dashBoardDAL.GetOrderMaterialInwardCount(BRANCHID, MONTH, YEAR);
        }
        public List<MaterialInwardDashboardCount> GetMaterialInwardCount(short BranchID)
        {
            return dashBoardDAL.GetOrderMaterialInwardCount(BranchID);
        }
        public List<MaterialOutwardDashboardCount> GetMaterialOutwardCount(short BranchID)
        {
            return dashBoardDAL.GetOrderMaterialOutwardCount(BranchID);
        }
        public List<RMAInwardDashboardCount> GetRMAInwardCount(short BranchID)
        {
            return dashBoardDAL.GetRMAInwardCount(BranchID);
        }
        public List<RMAOutwardDashboardCount> GetRMAOutwardCount(short BranchID)
        {
            return dashBoardDAL.GetRMAOutwardCount(BranchID);
        }
        public List<MaterialInandOutCountList> GetMaterialInAndOutwardCount(short BranchID)
        {
            return dashBoardDAL.GetMaterialInAndOutwardCount(BranchID);
        }
        public List<RMAInwardandOutwardDashboard> GetRMAInAndOutwardCount(short BranchID)
        {
            return dashBoardDAL.GetRMAInAndOutwardCount(BranchID);
        }
        public List<QtyandPriceDashboardForMaterialInandOut> GetMaterialInwardSumQtyPrice(short BranchID)
        {
            return dashBoardDAL.GetMaterialInwardSumQtyPrice(BranchID);
        }
        public List<QtyandPriceDashboardForMaterialInandOut> GetMaterialOutwardSumQtyPrice(short BranchID)
        {
            return dashBoardDAL.GetMaterialOutwardSumQtyPrice(BranchID);
        }
        public List<QtyandPriceDashboardForRMAInward> GetRMAInwardSumQtyPrice(short BranchID)
        {
            return dashBoardDAL.GetRMAInwardSumQtyPrice(BranchID);
        }
        public List<QtyandPriceDashboardForRMAOutward> GetRMAOutwardSumQtyPrice(short BranchID)
        {
            return dashBoardDAL.GetRMAOutwardSumQtyPrice(BranchID);
        }

        public List<ProductandPriceDashboardForMaterialInandOut> GetMaterialInwardProductandPrice(short BranchID)
        {
            return dashBoardDAL.GetMaterialInwardProductandPrice(BranchID);
        }
        public List<ProductandPriceDashboardForMaterialInandOut> GetMaterialOutwardProductandPrice(short BranchID)
        {
            return dashBoardDAL.GetMaterialOutwardProductandPrice(BranchID);
        }
        public List<ProductandPriceDashboardForRMAInward> GetRMAInwardProductandPrice(short BranchID)
        {
            return dashBoardDAL.GetRMAInwardProductandPrice(BranchID);
        }
        public List<ProductandPriceDashboardForRMAOutward> GetRMAOutwardProductandPrice(short BranchID)
        {
            return dashBoardDAL.GetRMAOutwardProductandPrice(BranchID);
        }
        public List<DashboardForTotalListProductwise> TotalMaterialInwardListProductwise(short BranchID)
        {
            return dashBoardDAL.GetTotalMaterialInwardListProductwise(BranchID);
        }
        public List<DashBoardForProductListCountMonthWise> MaterialInwardProductListCountMonthWise(short BranchID)
        {
            return dashBoardDAL.GetMaterialInwardProductListCountMonthWise(BranchID);
        }
        public int MaterialInwardProductCount(short BranchID)
        {
            return dashBoardDAL.GetMaterialInwardProductCount(BranchID);
        }
        public List<DashboardForTotalProductsMonthWiseReport> MaterialInwardProductCountMonthWise(short BranchID)
        {
            return dashBoardDAL.GetMaterialInwardProductCountMonthWise(BranchID);
        }
    }
}

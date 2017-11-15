using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.EDU.Contract;

namespace EZY.EDU.DataFactory
{
    public class DashboardDAL
    {
        private Database db;

        public DashboardDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }
        public List<MaterialInwardDashboardCount> GetOrderMaterialInwardCount(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALINWARDDASHBOARDCOUNT,
                                           MapBuilder<MaterialInwardDashboardCount>.BuildAllProperties(),BranchID).ToList();
        }
        public List<MaterialOutwardDashboardCount> GetOrderMaterialOutwardCount(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALOUTWARDDASHBOARDCOUNT,
                                           MapBuilder<MaterialOutwardDashboardCount>.BuildAllProperties(), BranchID).ToList();
        }
        public List<RMAInwardDashboardCount> GetRMAInwardCount(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAINWARDDASHBOARDCOUNT,
                                           MapBuilder<RMAInwardDashboardCount>.BuildAllProperties(), BranchID).ToList();
        }
        public List<RMAOutwardDashboardCount> GetRMAOutwardCount(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAOUTWARDDASHBOARDCOUNT,
                                           MapBuilder<RMAOutwardDashboardCount>.BuildAllProperties(), BranchID).ToList();
        }
        public List<MaterialInandOutCountList> GetMaterialInAndOutwardCount(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALINANDOUTDASHBOARDCOUNT,
                                           MapBuilder<MaterialInandOutCountList>.BuildAllProperties(), BranchID).ToList();
        }
        public List<RMAInwardandOutwardDashboard> GetRMAInAndOutwardCount(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAINANDOUTDASHBOARDCOUNT,
                                           MapBuilder<RMAInwardandOutwardDashboard>.BuildAllProperties(), BranchID).ToList();
        }
        public List<QtyandPriceDashboardForMaterialInandOut> GetMaterialInwardSumQtyPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALINSUMOFQTYANDPRICE,
                                           MapBuilder<QtyandPriceDashboardForMaterialInandOut>.BuildAllProperties(), BranchID).ToList();
        }
        public List<QtyandPriceDashboardForMaterialInandOut> GetMaterialOutwardSumQtyPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALOUTSUMOFQTYANDPRICE,
                                           MapBuilder<QtyandPriceDashboardForMaterialInandOut>.BuildAllProperties(), BranchID).ToList();
        }
        public List<QtyandPriceDashboardForRMAInward> GetRMAInwardSumQtyPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAINSUMOFQTYANDPRICE,
                                           MapBuilder<QtyandPriceDashboardForRMAInward>.BuildAllProperties(), BranchID).ToList();
        }
        public List<QtyandPriceDashboardForRMAOutward> GetRMAOutwardSumQtyPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAOUTSUMOFQTYANDPRICE,
                                           MapBuilder<QtyandPriceDashboardForRMAOutward>.BuildAllProperties(), BranchID).ToList();
        }
        public List<ProductandPriceDashboardForMaterialInandOut> GetMaterialInwardProductandPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALINSUMOFPRODUCTANDPRICE,
                                           MapBuilder<ProductandPriceDashboardForMaterialInandOut>.BuildAllProperties(), BranchID).ToList();
        }
        public List<ProductandPriceDashboardForMaterialInandOut> GetMaterialOutwardProductandPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALOUTSUMOFPRODUCTANDPRICE,
                                           MapBuilder<ProductandPriceDashboardForMaterialInandOut>.BuildAllProperties(), BranchID).ToList();
        }
        public List<ProductandPriceDashboardForRMAInward> GetRMAInwardProductandPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAINSUMOFPRODUCTANDPRICE,
                                           MapBuilder<ProductandPriceDashboardForRMAInward>.BuildAllProperties(), BranchID).ToList();
        }
        public List<ProductandPriceDashboardForRMAOutward> GetRMAOutwardProductandPrice(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAOUTSUMOFPRODUCTANDPRICE,
                                           MapBuilder<ProductandPriceDashboardForRMAOutward>.BuildAllProperties(), BranchID).ToList();
        }
        public List<DashboardForTotalListProductwise> GetTotalMaterialInwardListProductwise(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.TOTALMATERIALINWARDLISTPRODUCTWISE,
                                           MapBuilder<DashboardForTotalListProductwise>.BuildAllProperties(), BranchID).ToList();
        }
        public List<DashBoardForProductListCountMonthWise> GetMaterialInwardProductListCountMonthWise(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.TOTALMATERIALINWARDMONTHLISTPRODUCTWISE,
                                           MapBuilder<DashBoardForProductListCountMonthWise>.BuildAllProperties(), BranchID).ToList();
        }
        public int GetMaterialInwardProductCount(short BranchID)
        {
            int result = 0;
            var recordcommand = db.GetStoredProcCommand(DBRoutine.TOTALMATERIALINWARDPRODUCTWISECOUNT, BranchID);
            result = Convert.ToInt32(db.ExecuteScalar(recordcommand));
            return result;
        }
        public List<DashboardForTotalProductsMonthWiseReport> GetMaterialInwardProductCountMonthWise(short BranchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.TOTALMATERIALINWARDMONTHLIST,
                                           MapBuilder<DashboardForTotalProductsMonthWiseReport>.BuildAllProperties(), BranchID).ToList();
        }
        public System.Data.IDataReader GetOrderMaterialInwardCount(Int64 BRANCHID, int MONTH, int YEAR)
        {
          //  var orderCommand = db.GetStoredProcCommand(DBRoutine.ORDERCOUNTDASHBOARD);
            //db.AddInParameter(orderCommand, "@BranchID", System.Data.DbType.Int64, BRANCHID);
            // db.AddInParameter(orderCommand, "@Month", System.Data.DbType.Int32, MONTH);
            // db.AddInParameter(orderCommand, "@Year", System.Data.DbType.Int32, YEAR);
            //var orderCount = db.ExecuteReader(orderCommand);

            System.Data.SqlClient.SqlDataReader reader;

            var searchcommand = db.GetStoredProcCommand(DBRoutine.ORDERCOUNTDASHBOARD);
            try
            {
                reader = ((RefCountingDataReader)db.ExecuteReader(searchcommand)).InnerReader as System.Data.SqlClient.SqlDataReader;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return reader;

        }
    }
}

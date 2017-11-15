using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.EDU.Contract;

namespace EZY.EDU.DataFactory
{
    public class CourseDetailDAL
    {
        private Database db;
        public CourseDetailDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }

        public List<CourseDetail> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTEDUCOURSEDETAIL , MapBuilder<CourseDetail>.BuildAllProperties()).ToList();
        }

        public List<CourseDetail> GetProductTableDataList(DataTableObject Obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTEDUCOURSEDETAILTABLEDATA,
                                                       MapBuilder<CourseDetail>.MapAllProperties()
                                                       //.DoNotMap(p => p.ProductCategoryDescription)
                                                       .Build(), Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType).ToList();
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var courseDetail = (CourseDetail)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEEDUCOURSEDETAIL);

                db.AddInParameter(savecommand, "Id", System.Data.DbType.Int32, courseDetail.Id);
                db.AddInParameter(savecommand, "Country", System.Data.DbType.String, courseDetail.Country);
                db.AddInParameter(savecommand, "Month", System.Data.DbType.Int16, courseDetail.Month);
                db.AddInParameter(savecommand, "StartDate", System.Data.DbType.DateTime, courseDetail.StartDate);
                db.AddInParameter(savecommand, "EndDate", System.Data.DbType.DateTime, courseDetail.EndDate);
                db.AddInParameter(savecommand, "CustomerName", System.Data.DbType.String, courseDetail.CustomerName);
                db.AddInParameter(savecommand, "Product", System.Data.DbType.Int32, courseDetail.Product);
                db.AddInParameter(savecommand, "Course", System.Data.DbType.Int32, courseDetail.Course);
                db.AddInParameter(savecommand, "Particulars", System.Data.DbType.String, courseDetail.Particulars);
                db.AddInParameter(savecommand, "Days", System.Data.DbType.Int16, courseDetail.Days);
                db.AddInParameter(savecommand, "NoPax", System.Data.DbType.Int16, courseDetail.NoPax);
                db.AddInParameter(savecommand, "LabRental", System.Data.DbType.Decimal, courseDetail.LabRental);
                db.AddInParameter(savecommand, "SalesRev ", System.Data.DbType.Decimal, courseDetail.SalesRev);
                db.AddInParameter(savecommand, "TrainerName", System.Data.DbType.String, courseDetail.TrainerName);
                db.AddInParameter(savecommand, "DeliveredBy", System.Data.DbType.String, courseDetail.DeliveredBy);
                db.AddInParameter(savecommand, "Airfare", System.Data.DbType.Decimal, courseDetail.Airfare);
                db.AddInParameter(savecommand, "Hotel", System.Data.DbType.String, courseDetail.Hotel);
                db.AddInParameter(savecommand, "TrainerFee ", System.Data.DbType.Decimal, courseDetail.TrainerFee);
                db.AddInParameter(savecommand, "MISCLocal", System.Data.DbType.String, courseDetail.MISCLocal);
                db.AddInParameter(savecommand, "RegionalExpenses", System.Data.DbType.Decimal, courseDetail.RegionalExpenses);
                db.AddInParameter(savecommand, "CourseMaterial", System.Data.DbType.String, courseDetail.CourseMaterial);
                db.AddInParameter(savecommand, "TotalExp", System.Data.DbType.Decimal, courseDetail.TotalExp);
                db.AddInParameter(savecommand, "Profit", System.Data.DbType.Decimal, courseDetail.Profit);
                db.AddInParameter(savecommand, "Percentage", System.Data.DbType.Decimal, courseDetail.Percentage);
                db.AddInParameter(savecommand, "RegionInvoice", System.Data.DbType.String, courseDetail.RegionInvoice);
                db.AddInParameter(savecommand, "PaymentStatus", System.Data.DbType.String, courseDetail.PaymentStatus);
                db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, courseDetail.IsActive);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, courseDetail.CreatedBy);
                db.AddInParameter(savecommand, "CreatedOn", System.Data.DbType.DateTime, courseDetail.CreatedOn);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, courseDetail.ModifiedBy);
                db.AddInParameter(savecommand, "ModifiedOn", System.Data.DbType.DateTime, courseDetail.ModifiedOn);

                result = db.ExecuteNonQuery(savecommand, transaction);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var courseDetail = (CourseDetail)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEEDUCOURSEDETAIL);

                db.AddInParameter(deleteCommand, "Id", System.Data.DbType.String, courseDetail.Id);
                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return result;
        }

        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = (CourseDetail)lookupItem;


            var productItem = db.ExecuteSprocAccessor(DBRoutine.SELECTEDUCOURSEDETAIL,
                                                    MapBuilder<CourseDetail>.BuildAllProperties(),
                                                    item.Id).FirstOrDefault();
            return productItem;
        }
    }
}

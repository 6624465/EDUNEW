using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.EDU.Contract;
using System.Data;

namespace EZY.EDU.DataFactory
{
    public class CourseDAL : IDataFactory
    {
        private Database db;
        public CourseDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }

        public List<CourseVm> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTEDUCOURSE, MapBuilder<CourseVm>.BuildAllProperties()).ToList();
        }

        public List<Course> GetProductTableDataList(DataTableObject Obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTEDUCOURSETABLEDATA,
                                                       MapBuilder<Course>.MapAllProperties()
                                                       //.DoNotMap(p => p.ProductCategoryDescription)
                                                       .Build(), Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType).ToList();
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var course = (Course)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEEDUCOURSE);

                db.AddInParameter(savecommand, "Id", System.Data.DbType.Int32, course.Id);
                db.AddInParameter(savecommand, "Product", System.Data.DbType.String, course.Product);
                db.AddInParameter(savecommand, "CourseDescription", System.Data.DbType.String, course.CourseDescription);
                db.AddInParameter(savecommand, "CourseName", System.Data.DbType.String, course.CourseName);
                db.AddInParameter(savecommand, "NoOfDays", System.Data.DbType.Int16, course.NoOfDays);
                db.AddInParameter(savecommand, "Country", System.Data.DbType.String, course.Country);
                db.AddInParameter(savecommand, "AvailableSeats", System.Data.DbType.Int16, course.AvailableSeats);
                db.AddInParameter(savecommand, "PublicPrice", System.Data.DbType.Decimal, course.PublicPrice);
                db.AddInParameter(savecommand, "PrivatePrice", System.Data.DbType.Decimal, course.PrivatePrice);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, course.CreatedBy);
                db.AddInParameter(savecommand, "CreatedOn", System.Data.DbType.DateTime, course.CreatedOn);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, course.ModifiedBy);
                db.AddInParameter(savecommand, "ModifiedOn", System.Data.DbType.DateTime, course.ModifiedOn);
                db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, course.IsActive);

                result = db.ExecuteNonQuery(savecommand, transaction);

                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var course = (Course)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEEDUCOURSE);

                db.AddInParameter(deleteCommand, "Id", System.Data.DbType.String, course.Id);
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


        public bool IsEduCourseExists<T>(T item) where T : IContract
        {
            var course = (Course)(object)item;
            var result = 0;

            var command = db.GetStoredProcCommand(DBRoutine.COURSECOUNT);
            db.AddInParameter(command, "CourseName", DbType.String, course.CourseName);
            db.AddInParameter(command, "Country", DbType.String, course.Country);
            db.AddInParameter(command, "Product", DbType.Int32, course.Product);

            result = Convert.ToInt32(db.ExecuteScalar(command));

            return (result > 0 ? true : false);
        }

        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = (Course)lookupItem;


            var courseItem = db.ExecuteSprocAccessor(DBRoutine.SELECTEDUCOURSE,
                                                    MapBuilder<Course>.BuildAllProperties(),
                                                    item.Id).FirstOrDefault();
            return courseItem;
        }

        public List<Course> GetCoursesByProduct(int Id)
        {
            var courseItem = db.ExecuteSprocAccessor(DBRoutine.COURSESBYPRODUCT,
                                                    MapBuilder<Course>.BuildAllProperties(),
                                                    Id).ToList();
            return courseItem;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.EDU.Contract;

namespace EZY.EDU.DataFactory
{
    public class CourseSalesMasterDAL
    {
        private Database db;
        public CourseSalesMasterDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }

        public List<CourseSalesMasterViewModel> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTEDUCOURSESALESMASTER, MapBuilder<CourseSalesMasterViewModel>.BuildAllProperties()).ToList();
        }

        public List<CourseSalesMaster> GetProductTableDataList(DataTableObject Obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTEDUCOURSESALESMASTERTABLEDATA,
                MapBuilder<CourseSalesMaster>.MapAllProperties()
                //.DoNotMap(p => p.ProductCategoryDescription)
                .Build(), Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType).ToList();
        }

        public Int32 Save<T>(T item) where T : IContract
        {
            var result = 0;

            var courseSalesMaster = (CourseSalesMaster)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEEDUCOURSESALESMASTER);

                db.AddInParameter(savecommand, "Id", System.Data.DbType.Int32, courseSalesMaster.Id);
                db.AddInParameter(savecommand, "Country", System.Data.DbType.String, courseSalesMaster.Country);
                db.AddInParameter(savecommand, "Product", System.Data.DbType.Int32, courseSalesMaster.Product);
                db.AddInParameter(savecommand, "Course", System.Data.DbType.Int32, courseSalesMaster.Course);
                db.AddInParameter(savecommand, "NoOfDays", System.Data.DbType.Int16, courseSalesMaster.NoOfDays);
                db.AddInParameter(savecommand, "Month", System.Data.DbType.Int16, courseSalesMaster.Month);
                db.AddInParameter(savecommand, "StartDate", System.Data.DbType.DateTime, courseSalesMaster.StartDate);
                db.AddInParameter(savecommand, "EndDate", System.Data.DbType.DateTime, courseSalesMaster.EndDate);
                db.AddInParameter(savecommand, "MinimumReg", System.Data.DbType.Int16, courseSalesMaster.MinimumReg);
                db.AddInParameter(savecommand, "LeadsOnHead", System.Data.DbType.Int16, courseSalesMaster.LeadsOnHead);
                db.AddInParameter(savecommand, "Registered", System.Data.DbType.Int16, courseSalesMaster.Registered);
                db.AddInParameter(savecommand, "AvailableSeats", System.Data.DbType.Int16, courseSalesMaster.AvailableSeats);
                db.AddInParameter(savecommand, "RegClosingDate ", System.Data.DbType.DateTime, courseSalesMaster.RegClosingDate);
                db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, courseSalesMaster.IsActive);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, courseSalesMaster.CreatedBy);
                db.AddInParameter(savecommand, "CreatedOn", System.Data.DbType.DateTime, courseSalesMaster.CreatedOn);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, courseSalesMaster.ModifiedBy);
                db.AddInParameter(savecommand, "ModifiedOn ", System.Data.DbType.DateTime, courseSalesMaster.ModifiedOn);
                db.AddInParameter(savecommand, "LeadsOnHeadRemarks", System.Data.DbType.String, courseSalesMaster.LeadsOnHeadRemarks);
                db.AddInParameter(savecommand, "RegisteredRemarks ", System.Data.DbType.String, courseSalesMaster.RegisteredRemarks);
                db.AddInParameter(savecommand, "TOD", System.Data.DbType.Boolean, courseSalesMaster.TOD);
                db.AddInParameter(savecommand, "LVC", System.Data.DbType.Boolean, courseSalesMaster.LVC);
                db.AddInParameter(savecommand, "ILT", System.Data.DbType.Boolean, courseSalesMaster.ILT);
                db.AddInParameter(savecommand, "IsOpen", System.Data.DbType.Boolean, courseSalesMaster.IsOpen == null ? false : courseSalesMaster.IsOpen);
                db.AddInParameter(savecommand, "IsDrop", System.Data.DbType.Boolean, courseSalesMaster.IsDrop == null ? false : courseSalesMaster.IsDrop);
                db.AddInParameter(savecommand, "IsConfirm", System.Data.DbType.Boolean, courseSalesMaster.IsConfirm == null ? false : courseSalesMaster.IsConfirm);
                db.AddInParameter(savecommand, "ConfirmOrDropDate", System.Data.DbType.DateTime, (courseSalesMaster.IsOpen == null || courseSalesMaster.IsOpen == false) ? courseSalesMaster.ConfirmOrDropDate : null);
                db.AddOutParameter(savecommand, "NewId", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);

                if (result != 0)
                {
                    courseSalesMaster.Id = Convert.ToInt32(savecommand.Parameters["@NewId"].Value);
                }

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return courseSalesMaster.Id;

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var courseSalesMaster = (CourseSalesMaster)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEEDUCOURSESALESMASTER);

                db.AddInParameter(deleteCommand, "Id", System.Data.DbType.String, courseSalesMaster.Id);
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
            var item = (CourseSalesMaster)lookupItem;


            var courseSalesMasterItem = db.ExecuteSprocAccessor(DBRoutine.SELECTEDUCOURSESALESMASTER,
                                                    MapBuilder<CourseSalesMaster>.BuildAllProperties(),
                                                    item.Id).FirstOrDefault();
            return courseSalesMasterItem;
        }


        //public bool Confirm<T>(T item) where T : IContract
        //{
        //    var result = false;
        //    var courseSalesMaster = (CourseSalesMaster)(object)item;

        //    var connnection = db.CreateConnection();
        //    connnection.Open();

        //    var transaction = connnection.BeginTransaction();

        //    try
        //    {
        //        var confirmCommand = db.GetStoredProcCommand(DBRoutine.CONFIRMEDUCOURSESALESMASTER);

        //        db.AddInParameter(confirmCommand, "Id", System.Data.DbType.Int32, courseSalesMaster.Id);
        //        db.AddInParameter(confirmCommand, "Registered", System.Data.DbType.Int16, courseSalesMaster.Registered);
        //        db.AddInParameter(confirmCommand, "RegisteredRemarks", System.Data.DbType.Int32, courseSalesMaster.RegisteredRemarks);
        //        result = Convert.ToBoolean(db.ExecuteNonQuery(confirmCommand, transaction));

        //        transaction.Commit();

        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        throw ex;
        //    }

        //    return result;
        //}
    }
}

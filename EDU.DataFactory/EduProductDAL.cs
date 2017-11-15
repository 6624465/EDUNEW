using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.EDU.Contract;
using System.Data;

namespace EZY.EDU.DataFactory
{
    public class EduProductDAL : IDataFactory
    {
        private Database db;
        public EduProductDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }

        public List<EduProduct> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTEDUPRODUCT, MapBuilder<EduProduct>.BuildAllProperties()).ToList();
        }

        public List<EduProduct> GetProductTableDataList(DataTableObject Obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTEDUPRODUCTTABLEDATA,
                                                       MapBuilder<EduProduct>.MapAllProperties()
                                                       //.DoNotMap(p => p.ProductCategoryDescription)
                                                       .Build(), Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType).ToList();
        }

        public bool IsEduProductExists<T>(T item) where T : IContract
        {
            var product = (EduProduct)(object)item;
            var result = 0;

            var command = db.GetStoredProcCommand(DBRoutine.PRODUCTCOUNT);
            db.AddInParameter(command, "ProductName", DbType.String, product.ProductName);

            result = Convert.ToInt32(db.ExecuteScalar(command));

            return (result > 0 ? true : false);
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var product = (EduProduct)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEEDUPRODUCT);

                db.AddInParameter(savecommand, "Id", System.Data.DbType.Int32, product.Id);
                db.AddInParameter(savecommand, "ProductName", System.Data.DbType.String, product.ProductName);
                db.AddInParameter(savecommand, "ProductDescription", System.Data.DbType.String, product.ProductDescription);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, product.CreatedBy);
                db.AddInParameter(savecommand, "CreatedOn", System.Data.DbType.DateTime, product.CreatedOn);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, product.ModifiedBy);
                db.AddInParameter(savecommand, "ModifiedOn", System.Data.DbType.DateTime, product.ModifiedOn);
                db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, product.IsActive);

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
            var product = (EduProduct)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEEDUPRODUCT);

                db.AddInParameter(deleteCommand, "Id", System.Data.DbType.String, product.Id);
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
            var item = (EduProduct)lookupItem;


            var productItem = db.ExecuteSprocAccessor(DBRoutine.SELECTEDUPRODUCT,
                                                    MapBuilder<EduProduct>.BuildAllProperties(),
                                                    item.Id).FirstOrDefault();
            return productItem;
        }


    }
}

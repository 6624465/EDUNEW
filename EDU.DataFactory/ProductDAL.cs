using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using EZY.EDU.Contract;

namespace EZY.EDU.DataFactory
{
    public class ProductDAL : IDataFactory
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<Product> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTPRODUCT, MapBuilder<Product>.BuildAllProperties()).ToList();
        }

        public List<Product> GetProductTableDataList(DataTableObject Obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTPRODUCTTABLEDATA,
                                                       MapBuilder<Product>.MapAllProperties()
                                                       //.DoNotMap(p => p.ProductCategoryDescription)
                                                       .Build(), Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType).ToList();
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var product = (Product)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEPRODUCT);

                db.AddInParameter(savecommand, "ProductCode", System.Data.DbType.String, product.ProductCode);
                db.AddInParameter(savecommand, "ModelNo", System.Data.DbType.String, product.ModelNo);
                db.AddInParameter(savecommand, "Description", System.Data.DbType.String, product.Description);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, product.Status);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, product.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, product.ModifiedBy);
                db.AddInParameter(savecommand, "ProductCategory", System.Data.DbType.Int16, product.ProductCategory);





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
            var product = (Product)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEPRODUCT);

                db.AddInParameter(deleteCommand, "ProductCode", System.Data.DbType.String, product.ProductCode);
                db.AddInParameter(deleteCommand, "ModelNo", System.Data.DbType.String, product.ModelNo);
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
            var item = (Product)lookupItem;


            var productItem = db.ExecuteSprocAccessor(DBRoutine.SELECTPRODUCT,
                                                    MapBuilder<Product>.BuildAllProperties(),
                                                    item.ProductCode, item.ModelNo).FirstOrDefault();
            return productItem;
        }

        #endregion






    }
}


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
    public class ProductCategoryDAL : IDataFactory
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductCategoryDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<ProductCategoryMaster> GetList( )
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTPRODUCTCATEGORY, MapBuilder<ProductCategoryMaster>.BuildAllProperties() ).ToList();
        }

        
        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var productcategorymaster = (ProductCategoryMaster)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEPRODUCTCATEGORY);

               // db.AddInParameter(savecommand, "ProductCategory", System.Data.DbType.Int16, productcategorymaster.ProductCategory);
                db.AddInParameter(savecommand, "Code", System.Data.DbType.String, productcategorymaster.Code);
                db.AddInParameter(savecommand, "Description", System.Data.DbType.String, productcategorymaster.Description);
                db.AddInParameter(savecommand, "CategoryGroup", System.Data.DbType.Int16, productcategorymaster.CategoryGroup);
                 




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
            var productcategorymaster = (ProductCategoryMaster)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEPRODUCTCATEGORY);

                db.AddInParameter(deleteCommand, "ProductCategory", System.Data.DbType.Int16, productcategorymaster.ProductCategory);
                db.AddInParameter(deleteCommand, "Code", System.Data.DbType.String, productcategorymaster.Code);

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

            var item = (ProductCategoryMaster)lookupItem;

            var productcategoryItem = db.ExecuteSprocAccessor(DBRoutine.SELECTPRODUCTCATEGORY,
                                                    MapBuilder<ProductCategoryMaster>.BuildAllProperties(),
                                                    item.ProductCategory, item.Code).FirstOrDefault();



            return productcategoryItem;
        }

        #endregion






    }
}


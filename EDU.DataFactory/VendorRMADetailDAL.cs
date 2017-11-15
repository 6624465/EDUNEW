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
       public  class VendorRMADetailDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public VendorRMADetailDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }

        #region IDataFactory Members
        public bool SaveList<T>(List<T> items, DbTransaction parentTransaction) where T : IContract
        {
            var result = true;

            if (items.Count == 0)
                result = true;

            foreach (var item in items)
            {
                result = Save(item, parentTransaction);
                if (result == false) break;
            }


            return result;

        }
        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }



        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var rmadetail = (VendorRMADetails)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMADETAIL);

                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, rmadetail.DocumentNo);
                db.AddInParameter(savecommand, "VendorInvoiceNo", System.Data.DbType.String, rmadetail.VendorInvoiceNo);
                db.AddInParameter(savecommand, "VendorName", System.Data.DbType.String, rmadetail.VendorName);
                db.AddInParameter(savecommand, "SerialNo", System.Data.DbType.String, rmadetail.SerialNo);
                db.AddInParameter(savecommand, "WarrantyExpiryDate", System.Data.DbType.DateTime, rmadetail.VendorWarrantyExpiryDate);
                db.AddInParameter(savecommand, "IsValid", System.Data.DbType.Boolean, rmadetail.IsValid);
                db.AddInParameter(savecommand, "CustomerWarrantyExpiryDate", System.Data.DbType.DateTime, rmadetail.WarrantyExpiryDate);
                db.AddInParameter(savecommand, "RMAIsValid", System.Data.DbType.Boolean, rmadetail.RMAIsValid);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, rmadetail.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, rmadetail.ModifiedBy);

                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw ex;
            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var vendorrmadetails = (VendorRMADetails)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEINVOICEDETAIL);

                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, vendorrmadetails.DocumentNo);
 
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

            //var item = (VendorRMADetails)lookupItem;

            //var vendorinvoicedetailItem = db.ExecuteSprocAccessor(DBRoutine.select,
            //                                        MapBuilder<VendorRMADetails>.BuildAllProperties(),
            //                                        item.DocumentNo).FirstOrDefault();
            //return vendorinvoicedetailItem;

            throw new NotImplementedException();
        }
        #endregion
    }
}

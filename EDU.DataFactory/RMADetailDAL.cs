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
   
    public class RMADetailDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public RMADetailDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<RMADetail> GetList(string documentNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTRMADETAIL, MapBuilder<RMADetail>.BuildAllProperties(),documentNo).ToList();
        }

        public List<RMADetail> GetListByDocumentNo(string documentNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTRMADETAILBYDOCUMENTNO, 
                MapBuilder<RMADetail>.BuildAllProperties(), 
                documentNo).ToList();
        }



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

            var rmadetail = (RMADetail)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVERMADETAIL);

                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, rmadetail.DocumentNo);
                db.AddInParameter(savecommand, "SerialNo", System.Data.DbType.String, rmadetail.SerialNo);
                db.AddInParameter(savecommand, "WarrantyExpiryDate", System.Data.DbType.DateTime, rmadetail.WarrantyExpiryDate);
                db.AddInParameter(savecommand, "IsValid", System.Data.DbType.Boolean, rmadetail.IsValid);
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
            var invoicedetail = (RMADetail)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETERMADETAIL);

                db.AddInParameter(deleteCommand, "InvoiceNo", System.Data.DbType.String, invoicedetail.DocumentNo);
                db.AddInParameter(deleteCommand, "SerialNo", System.Data.DbType.String, invoicedetail.SerialNo);
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

            var item = (RMADetail)lookupItem;

            var  detailItem = db.ExecuteSprocAccessor(DBRoutine.SELECTRMADETAIL,
                                                    MapBuilder<RMADetail>.BuildAllProperties(),
                                                    item.DocumentNo,
                                                    item.SerialNo).FirstOrDefault();
            return detailItem;
        }

 
        #endregion

    }
}

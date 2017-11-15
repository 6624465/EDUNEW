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
    
    public class RMAOutwardDetailDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public RMAOutwardDetailDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<RMAOutwardDetail> GetList(short branchID, string documentNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTRMAOUTWARDDETAIL, MapBuilder<RMAOutwardDetail>.BuildAllProperties(), branchID, documentNo).ToList();
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

            var  detail = (RMAOutwardDetail)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVERMAOUTWARDDETAIL);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, detail.BranchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, detail.DocumentNo);
                db.AddInParameter(savecommand, "OldSerialNo", System.Data.DbType.String, detail.OldSerialNo);
                db.AddInParameter(savecommand, "ProductCode", System.Data.DbType.String, detail.ProductCode);
                db.AddInParameter(savecommand, "NewSerialNo", System.Data.DbType.String, detail.NewSerialNo);                
                db.AddInParameter(savecommand, "IsCreditNote", System.Data.DbType.Boolean, detail.IsCreditNote);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, detail.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, detail.ModifiedBy);




                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw;
            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var invoicedetail = (RMAOutwardDetail)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETERMAOUTWARDDETAIL);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, invoicedetail.BranchID);
                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, invoicedetail.DocumentNo);
 
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

            var item = (RMAOutwardDetail)lookupItem;

            var vendorinvoicedetailItem = db.ExecuteSprocAccessor(DBRoutine.SELECTRMAOUTWARDDETAIL,
                                                    MapBuilder<RMAOutwardDetail>.BuildAllProperties(),
                                                    item.BranchID,
                                                    item.DocumentNo,
                                                    item.OldSerialNo).FirstOrDefault();
            return vendorinvoicedetailItem;
        }

        public List<RMAOutwardDetail> RMAOutWardDetailListByRMAInWardDocumentNo(string documentNo,Int16 BranchId)
        {

            

            var detailList = db.ExecuteSprocAccessor(DBRoutine.RMAOUTWARDDETAILLISTBYRMAINWARDDOCUMENTNO,
                                                    MapBuilder<RMAOutwardDetail>.BuildAllProperties(),
                                                    documentNo,BranchId).ToList();
            return detailList;
        }




        #endregion

    }
}

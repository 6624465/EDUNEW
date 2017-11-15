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
     
    public class RMAOutwardHeaderDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public RMAOutwardHeaderDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<RMAOutwardHeader> GetList(short branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTINVOICEHEADER,
                                           MapBuilder<RMAOutwardHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.RmaDetails)
                                           .Build(), branchID).ToList();
        }

        


        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }


        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var rmaoutheader = (RMAOutwardHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVERMAOUTWARDHEADER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, rmaoutheader.BranchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, rmaoutheader.DocumentNo ?? "");
                db.AddInParameter(savecommand, "DocumentDate", System.Data.DbType.DateTime, rmaoutheader.DocumentDate);
                db.AddInParameter(savecommand, "ReferenceNo", System.Data.DbType.String, rmaoutheader.ReferenceNo);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, rmaoutheader.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, rmaoutheader.ModifiedBy);
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);


                if (result > 0)
                {
                    rmaoutheader.DocumentNo = savecommand.Parameters["@NewDocumentNo"].Value.ToString();
                    rmaoutheader.RmaDetails.ForEach(dt =>
                    {
                        dt.DocumentNo = rmaoutheader.DocumentNo;
                        dt.CreatedBy = rmaoutheader.CreatedBy;
                        dt.ModifiedBy = rmaoutheader.ModifiedBy;
                    });
                    RMAOutwardDetailDAL detailDAL = new RMAOutwardDetailDAL();

                    Func<RMAOutwardDetail, bool> WhereFunc = delegate (RMAOutwardDetail detail)
                    {
                        var NewSerialNo = detail.NewSerialNo ?? "";
                        var IsCreditNote = detail.IsCreditNote;
                        return NewSerialNo.Trim().Length > 0 || IsCreditNote;
                    };
                    result = detailDAL.SaveList(rmaoutheader.RmaDetails.Where(WhereFunc).ToList(),transaction) == true ? 1 : 0;


                    result = new RMAHeaderDAL().CloseRMAHeader(rmaoutheader.BranchID, rmaoutheader.ReferenceNo, rmaoutheader.DocumentNo,transaction) == true ? 1 : 0;

                }

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
            var invoiceheader = (RMAOutwardHeader)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEINVOICEHEADER);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, invoiceheader.BranchID);
                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, invoiceheader.DocumentNo);

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

            var item = (RMAOutwardHeader)lookupItem;

            var  headerItem = db.ExecuteSprocAccessor(DBRoutine.SELECTINVOICEHEADER,
                                                    MapBuilder<RMAOutwardHeader>
                                                    .MapAllProperties()
                                                    .DoNotMap(hd => hd.RmaDetails)
                                                    .Build(),
                                                    item.BranchID,
                                                   item.DocumentNo).FirstOrDefault();

            if (headerItem != null)
            {
                headerItem.RmaDetails = new RMAOutwardDetailDAL().GetList(headerItem.BranchID,headerItem.DocumentNo);
            }

            return headerItem;
        }


         


        #endregion


    }
}

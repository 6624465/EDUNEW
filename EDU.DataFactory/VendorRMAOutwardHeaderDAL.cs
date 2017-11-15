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
   public class VendorRMAOutwardHeaderDAL:IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public VendorRMAOutwardHeaderDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<VendorRMAOutwardHeader> GetList(short branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTINVOICEHEADER,
                                           MapBuilder<VendorRMAOutwardHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.VendorRmaDetails)
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

            var vendorRMAOutwardHeader = (VendorRMAOutwardHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMAOUTWARDHEADER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, vendorRMAOutwardHeader.BranchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, vendorRMAOutwardHeader.DocumentNo ?? "");
                db.AddInParameter(savecommand, "DocumentDate", System.Data.DbType.DateTime, vendorRMAOutwardHeader.DocumentDate);
                db.AddInParameter(savecommand, "ReferenceNo", System.Data.DbType.String, vendorRMAOutwardHeader.ReferenceNo);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, vendorRMAOutwardHeader.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, vendorRMAOutwardHeader.ModifiedBy);
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);


                if (result > 0)
                {
                    vendorRMAOutwardHeader.DocumentNo = savecommand.Parameters["@NewDocumentNo"].Value.ToString();
                    vendorRMAOutwardHeader.VendorRmaDetails.ForEach(dt =>
                    {
                        dt.DocumentNo = vendorRMAOutwardHeader.DocumentNo;
                        dt.CreatedBy =  vendorRMAOutwardHeader.CreatedBy;
                        dt.ModifiedBy = vendorRMAOutwardHeader.ModifiedBy;
                    });



                    VendorRMAOutwardDetailDAL detailDAL = new VendorRMAOutwardDetailDAL();
                    Func<VendorRMAOutwardDetail, bool> WhereFunc = delegate (VendorRMAOutwardDetail detail)
                    {
                        var NewSerialNo = detail.NewSerialNo ?? "";
                        var IsCreditNote = detail.IsCreditNote;
                        return NewSerialNo.Trim().Length > 0 || IsCreditNote;
                    };

                    result = detailDAL.SaveList(vendorRMAOutwardHeader.VendorRmaDetails.Where(WhereFunc).ToList(), transaction) == true ? 1 : 0;


                    result = new VendorRMAHeaderDAL().CloseVendorRMAHeader(vendorRMAOutwardHeader.BranchID, vendorRMAOutwardHeader.ReferenceNo, vendorRMAOutwardHeader.DocumentNo, transaction) == true ? 1 : 0;

                }

                if (currentTransaction == null)
                    transaction.Commit();


            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();
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

            var headerItem = db.ExecuteSprocAccessor(DBRoutine.SELECTINVOICEHEADER,
                                                    MapBuilder<RMAOutwardHeader>
                                                    .MapAllProperties()
                                                    .DoNotMap(hd => hd.RmaDetails)
                                                    .Build(),
                                                    item.BranchID,
                                                   item.DocumentNo).FirstOrDefault();

            if (headerItem != null)
            {
                headerItem.RmaDetails = new RMAOutwardDetailDAL().GetList(headerItem.BranchID, headerItem.DocumentNo);
            }

            return headerItem;
        }
        #endregion
    }
}

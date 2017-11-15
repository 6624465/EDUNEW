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
    public class InvoiceDetailDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceDetailDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<InvoiceDetail> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTINVOICEDETAIL, MapBuilder<InvoiceDetail>.BuildAllProperties()).ToList();
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

            var invoicedetail = (InvoiceDetail)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEINVOICEDETAIL);

                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, invoicedetail.DocumentNo);
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, invoicedetail.InvoiceNo);
                db.AddInParameter(savecommand, "ModelNo", System.Data.DbType.String, invoicedetail.ModelNo);
                db.AddInParameter(savecommand, "SerialNo", System.Data.DbType.String, invoicedetail.SerialNo);
                db.AddInParameter(savecommand, "Make", System.Data.DbType.String, invoicedetail.Make);
                db.AddInParameter(savecommand, "ExpiryDate", System.Data.DbType.DateTime, invoicedetail.ExpiryDate);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, invoicedetail.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, invoicedetail.ModifiedBy);




                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception)
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
            var invoicedetail = (InvoiceDetail)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEINVOICEDETAIL);

                db.AddInParameter(deleteCommand, "InvoiceNo", System.Data.DbType.String, invoicedetail.InvoiceNo);
                db.AddInParameter(deleteCommand, "ModelNo", System.Data.DbType.String, invoicedetail.ModelNo);
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

            var item = (InvoiceDetail)lookupItem;

            var vendorinvoicedetailItem = db.ExecuteSprocAccessor(DBRoutine.SELECTINVOICEDETAIL,
                                                    MapBuilder<InvoiceDetail>.BuildAllProperties(),
                                                    item.InvoiceNo,
                                                    item.ModelNo,
                                                    item.SerialNo).FirstOrDefault();
            return vendorinvoicedetailItem;
        }

        public RMAInwardDTO GetVendorInvoiceDetailBySerialNo(string serialNo, Int16 branchID, Int16 invoiceType)
        {



            var rmaInwardDTO = db.ExecuteSprocAccessor(DBRoutine.INVOICEDETAILSBYSERIALNO,
                                                    MapBuilder<RMAInwardDTO>.BuildAllProperties(),
                                                    branchID,
                                                    serialNo,
                                                    invoiceType).FirstOrDefault();
            return rmaInwardDTO;
        }
        public VendorRMAInwardDTO GetVendorDetailBySerialNo(string serialNo, Int16 branchID, Int16 invoiceType)
        {



            var rmaInwardDTO = db.ExecuteSprocAccessor(DBRoutine.VENDORDETAILSBYSERIALNO,
                                                    MapBuilder<VendorRMAInwardDTO>.BuildAllProperties(),
                                                    branchID,
                                                    serialNo,
                                                    invoiceType).FirstOrDefault();
            return rmaInwardDTO;
        }
        public MaterialsEnquiryDetails GetDetailBySerialNo(string serialNo, Int16 branchID)
        {
            try
            {
                var rmaInwardDTO = db.ExecuteSprocAccessor(DBRoutine.SERIALNOSTATUSDETAILS,
                                                    MapBuilder<MaterialsEnquiryDetails>.BuildAllProperties(),
                                                    branchID,
                                                    serialNo
                                                    ).FirstOrDefault();
                return rmaInwardDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<InvoiceDetail> GetInvoiceItemDetailList(InvoiceDetail item)
        {
            var detailList = db.ExecuteSprocAccessor(DBRoutine.INVOICEDETAILBYDOCUMENTINVOICENO,
                                                    MapBuilder<InvoiceDetail>.BuildAllProperties(),
                                                    item.DocumentNo,
                                                    item.InvoiceNo).ToList();
            return detailList;
        }

        #endregion

    }
}


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
    public class InvoiceHeaderDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceHeaderDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<InvoiceHeader> GetList(short branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTINVOICEHEADER,
                                           MapBuilder<InvoiceHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.InvoiceDetailItems)
                                           .DoNotMap(hd=> hd.ProductDescription)
                                           .Build(), branchID).ToList();
        }

        public List<InvoiceHeaderData> GetVendorInvoiceDataTableList(DataTableObject Obj, Int16 invoiceType)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTINVOICEHEADERDATATABLELIST2,
                MapBuilder<InvoiceHeaderData>.BuildAllProperties(),
                Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType, invoiceType, Obj.BranchID).ToList();
        }


        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }


        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var invoiceheader = (InvoiceHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEINVOICEHEADER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, invoiceheader.BranchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, invoiceheader.DocumentNo ?? "");
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, invoiceheader.InvoiceNo);
                db.AddInParameter(savecommand, "CusInvoiceNo", System.Data.DbType.String, invoiceheader.CusInvoiceNo);
                db.AddInParameter(savecommand, "InvoiceType", System.Data.DbType.Int16, invoiceheader.InvoiceType);
                db.AddInParameter(savecommand, "InvoiceDate", System.Data.DbType.DateTime, invoiceheader.InvoiceDate);
                db.AddInParameter(savecommand, "CustomerCode", System.Data.DbType.String, invoiceheader.CustomerCode);
                db.AddInParameter(savecommand, "ProductCode", System.Data.DbType.String, invoiceheader.ProductCode);
                db.AddInParameter(savecommand, "WarrantyPeriod", System.Data.DbType.Int16, invoiceheader.WarrantyPeriod);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, invoiceheader.Status);
                db.AddInParameter(savecommand, "FileName", System.Data.DbType.String, invoiceheader.FileName);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, invoiceheader.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, invoiceheader.ModifiedBy);
                db.AddInParameter(savecommand, "Quantity", System.Data.DbType.Int16, invoiceheader.Quantity);
                db.AddInParameter(savecommand, "UnitPrice", System.Data.DbType.Decimal, invoiceheader.UnitPrice);
                db.AddInParameter(savecommand, "CurrencyCode", System.Data.DbType.String, invoiceheader.CurrencyCode);
                db.AddInParameter(savecommand, "ProductCategory", System.Data.DbType.Int16, invoiceheader.ProductCategory);
                db.AddInParameter(savecommand, "ModelNo", System.Data.DbType.String, invoiceheader.ModelNo);
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);


                if (result > 0)
                {
                    invoiceheader.DocumentNo = savecommand.Parameters["@NewDocumentNo"].Value.ToString();
                    invoiceheader.InvoiceDetailItems.ForEach(dt =>
                    {
                        dt.DocumentNo = invoiceheader.DocumentNo;
                        dt.CreatedBy = invoiceheader.CreatedBy;
                        dt.ModifiedBy = invoiceheader.ModifiedBy;
                    });


                    InvoiceDetailDAL invoicedetailDAL = new InvoiceDetailDAL();
                    
                    result = invoicedetailDAL.SaveList(invoiceheader.InvoiceDetailItems, transaction) == true ? 1 : 0;

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
        public List<string> CheckSerailNumber(Int16 branchID, List<string> serialNumbers)
        {
            List<string> lstMissingSerailNos = new List<string>();
            var result = false;

            foreach (var serialNo in serialNumbers)
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.CHECKINWARDSERIALNO);
                db.AddInParameter(deleteCommand, "@BranchID", System.Data.DbType.Int16, branchID);
                db.AddInParameter(deleteCommand, "@SerialNo", System.Data.DbType.String, serialNo);
                if ((int)db.ExecuteScalar(deleteCommand)==0)
                {
                    lstMissingSerailNos.Add(serialNo);
                }
                 
            }
            
                                         
            return lstMissingSerailNos;
        }

        public bool DeleteSerialNo(string serialNo)
        {
            var result =false;
            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETESERIALNO);
                db.AddInParameter(deleteCommand, "@SerialNo", System.Data.DbType.String,serialNo);
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
        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var invoiceheader = (InvoiceHeader)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEINVOICEHEADER);

                db.AddInParameter(deleteCommand, "InvoiceNo", System.Data.DbType.String, invoiceheader.InvoiceNo);
                db.AddInParameter(deleteCommand, "InvoiceType", System.Data.DbType.Int16, invoiceheader.InvoiceType);
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

            var item = (InvoiceHeader)lookupItem;

            var vendorinvoiceheaderItem = db.ExecuteSprocAccessor(DBRoutine.SELECTINVOICEHEADER,
                                                    MapBuilder<InvoiceHeader>
                                                    .MapAllProperties()
                                                    .DoNotMap(hd => hd.InvoiceDetailItems)
                                                   // .DoNotMap(hd=>hd.CusInvoiceNo)
                                                    .Build(),
                                                    item.BranchID,
                                                    item.DocumentNo,
                                                   item.InvoiceNo,
                                                   item.InvoiceType).FirstOrDefault();

            if(vendorinvoiceheaderItem != null)
            {
                vendorinvoiceheaderItem.InvoiceDetailItems = new InvoiceDetailDAL().GetInvoiceItemDetailList(new InvoiceDetail { DocumentNo = vendorinvoiceheaderItem.DocumentNo, InvoiceNo = vendorinvoiceheaderItem.InvoiceNo });
            }

            return vendorinvoiceheaderItem;
        }



        public System.Data.IDataReader SerialNoInquiry(string modelNo,  string serialNo)
        {


            System.Data.SqlClient.SqlDataReader reader;

            var searchcommand = db.GetStoredProcCommand(DBRoutine.SERIALNOINQUIRY);
            db.AddInParameter(searchcommand, "ModelNo", System.Data.DbType.String, modelNo.Length == 0 ? null : modelNo);
            db.AddInParameter(searchcommand, "SerialNo", System.Data.DbType.String, serialNo.Length == 0 ? null : serialNo);
            


            try
            {
                reader = ((RefCountingDataReader)db.ExecuteReader(searchcommand)).InnerReader as System.Data.SqlClient.SqlDataReader;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return reader;


        }


        #endregion

         
    }
}


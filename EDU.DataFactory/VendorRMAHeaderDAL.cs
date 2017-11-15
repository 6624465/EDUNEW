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
   public class VendorRMAHeaderDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
             #region IDataFactory Members
        public VendorRMAHeaderDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }
        public List<VendorRMAHeader> GetListByBranchID(Int16 branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.VENDORRMAHEADERLISTBYBRANCHID,
                                           MapBuilder<VendorRMAHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.VendorRmaDetails)
                                           .Build(), branchID).ToList();
        }
        public bool Save<T>(T item, Int16 branchID) where T : IContract
        {
            var result = 0;

            var vendorrmaheader = (VendorRMAHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMAHEADER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, branchID);
                db.AddInParameter(savecommand, "DoNo", System.Data.DbType.String, vendorrmaheader.DoNo ?? "");
                db.AddInParameter(savecommand, "CountryCode", System.Data.DbType.String, vendorrmaheader.CountryCode);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, vendorrmaheader.EmailID);
                db.AddInParameter(savecommand, "ContactNo", System.Data.DbType.String, vendorrmaheader.ContactNo);
                db.AddInParameter(savecommand, "IncidentDate", System.Data.DbType.DateTime, vendorrmaheader.IncidentDate);
                db.AddInParameter(savecommand, "CustomerAddress", System.Data.DbType.String, vendorrmaheader.CustomerAddress);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, vendorrmaheader.Status);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, vendorrmaheader.CreatedBy ?? "ADMIN");
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, vendorrmaheader.ModifiedBy ?? "ADMIN");
                db.AddInParameter(savecommand, "CompanyName", System.Data.DbType.String, vendorrmaheader.CompanyName);
                db.AddOutParameter(savecommand, "NewDoNo", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);


                if (result > 0)
                {
                    vendorrmaheader.DoNo = savecommand.Parameters["@NewDoNo"].Value.ToString();
                    vendorrmaheader.VendorRmaDetails.ForEach(dt =>
                    {
                        dt.DocumentNo = vendorrmaheader.DoNo;
                        dt.CreatedBy = vendorrmaheader.CreatedBy;
                        dt.ModifiedBy = vendorrmaheader.ModifiedBy;
                    });


                    VendorRMADetailDAL detailsVendorRMA = new VendorRMADetailDAL();

                    result = detailsVendorRMA.SaveList(vendorrmaheader.VendorRmaDetails, transaction) == true ? 1 : 0;
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
        public bool CloseVendorRMAHeader(short branchID, string referenceNo,string documentNo, DbTransaction parentTransaction)
        {
            var result = false;


            //if (currentTransaction == null)
            //{
            //    connection = db.CreateConnection();
            //    connection.Open();
            //}

            currentTransaction = parentTransaction;


            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.CLOSEVENDORRMAHEADER);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, branchID);
                db.AddInParameter(deleteCommand, "ReferenceNo", System.Data.DbType.String, referenceNo);
                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, documentNo);

                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();
                throw ex;
            }

            return result;
        }
        #endregion

    }
}

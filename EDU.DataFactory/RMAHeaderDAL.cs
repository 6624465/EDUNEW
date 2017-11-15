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
    public class RMAHeaderDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public RMAHeaderDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<RMAHeader> GetList(string countryCode)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTRMAHEADER,
                                           MapBuilder<RMAHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.rmaDetails)
                                           .Build(), countryCode).ToList();
        }

        public List<RMAHeader> GetListByBranchID(Int16 BranchID)
        {
            try
            {
                return db.ExecuteSprocAccessor(DBRoutine.RMAHEADERLISTBYBRANCHID,
                                          MapBuilder<RMAHeader>.MapAllProperties()
                                          .DoNotMap(hd => hd.rmaDetails)
                                          .Build(), BranchID).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VendorRMASave<T>(T item, Int16 branchID) where T : IContract
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

                //db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, branchID);
                db.AddInParameter(savecommand, "DoNo", System.Data.DbType.String, vendorrmaheader.DoNo ?? "");
                db.AddInParameter(savecommand, "CountryCode", System.Data.DbType.String, vendorrmaheader.CountryCode);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, vendorrmaheader.EmailID);
                db.AddInParameter(savecommand, "ContactNo", System.Data.DbType.String, vendorrmaheader.ContactNo);
                db.AddInParameter(savecommand, "IncidentDate", System.Data.DbType.DateTime, vendorrmaheader.IncidentDate);
                db.AddInParameter(savecommand, "CustomerAddress", System.Data.DbType.String, vendorrmaheader.CustomerAddress);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, vendorrmaheader.Status);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, vendorrmaheader.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, vendorrmaheader.ModifiedBy);
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


                    VendorRMADetailDAL detailsDAL = new VendorRMADetailDAL();

                    result = detailsDAL.SaveList(vendorrmaheader.VendorRmaDetails, transaction) == true ? 1 : 0;
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
        public bool Save<T>(T item, Int16 branchID) where T : IContract
        {
            var result = 0;

            var rmaheader = (RMAHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVERMAHEADER);

                //db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, branchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, rmaheader.DocumentNo ?? "");
                db.AddInParameter(savecommand, "CountryCode", System.Data.DbType.String, rmaheader.CountryCode);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, rmaheader.EmailID);
                db.AddInParameter(savecommand, "ContactNo", System.Data.DbType.String, rmaheader.ContactNo);
                db.AddInParameter(savecommand, "IncidentDate", System.Data.DbType.DateTime, rmaheader.IncidentDate);
                db.AddInParameter(savecommand, "CustomerAddress", System.Data.DbType.String, rmaheader.CustomerAddress);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, rmaheader.Status);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, rmaheader.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, rmaheader.ModifiedBy);
                db.AddInParameter(savecommand, "CompanyName", System.Data.DbType.String, rmaheader.CompanyName);
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);


                if (result > 0)
                {
                    rmaheader.DocumentNo = savecommand.Parameters["@NewDocumentNo"].Value.ToString();
                    rmaheader.rmaDetails.ForEach(dt =>
                    {
                        dt.DocumentNo = rmaheader.DocumentNo;
                        dt.CreatedBy = rmaheader.CreatedBy;
                        dt.ModifiedBy = rmaheader.ModifiedBy;
                    });


                    RMADetailDAL detailsDAL = new RMADetailDAL();

                    result = detailsDAL.SaveList(rmaheader.rmaDetails, transaction) == true ? 1 : 0;

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
            var rmaheader = (RMAHeader)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETERMAHEADER);

                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, rmaheader.DocumentNo);
                db.AddInParameter(deleteCommand, "CountryCode", System.Data.DbType.String, rmaheader.CountryCode);
                db.AddInParameter(deleteCommand, "EmailID", System.Data.DbType.String, rmaheader.EmailID);

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

            var item = (RMAHeader)lookupItem;

            var rmaheaderItem = db.ExecuteSprocAccessor(DBRoutine.SELECTRMAHEADER,
                                                    MapBuilder<RMAHeader>
                                                    .MapAllProperties()
                                                    .DoNotMap(hd => hd.rmaDetails)
                                                    .Build(),
                                                    item.DocumentNo,
                                                   item.CountryCode,
                                                   item.EmailID).FirstOrDefault();

            if (rmaheaderItem != null)
            {
                rmaheaderItem.rmaDetails = new RMADetailDAL().GetList(rmaheaderItem.DocumentNo);
            }

            return rmaheaderItem;
        }

        public IContract GetItemByDocumentNo<T>(IContract lookupItem) where T : IContract
        {

            var item = (RMAHeader)lookupItem;

            var rmaheaderItem = db.ExecuteSprocAccessor(DBRoutine.SELECTRMAHEADERBYDOCUMENTNO,
                                                    MapBuilder<RMAHeader>
                                                    .MapAllProperties()
                                                    .DoNotMap(hd => hd.rmaDetails)
                                                    .Build(),
                                                    item.DocumentNo).FirstOrDefault();

            if (rmaheaderItem != null)
            {
                rmaheaderItem.rmaDetails = new RMADetailDAL().GetListByDocumentNo(rmaheaderItem.DocumentNo);
            }

            return rmaheaderItem;
        }



        public bool CloseRMAHeader(short branchID, string referenceNo, string documentNo, DbTransaction parentTransaction)
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
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.CLOSERMAHEADER);

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

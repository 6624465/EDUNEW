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
    public class LookupDAL : IDataFactory
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public LookupDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<Lookup> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTLOOKUP, MapBuilder<Lookup>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var lookup = (Lookup)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVELOOKUP);

                db.AddInParameter(savecommand, "LookupID", System.Data.DbType.Int16, lookup.LookupID);
                db.AddInParameter(savecommand, "LookupCode", System.Data.DbType.String, lookup.LookupCode);
                db.AddInParameter(savecommand, "LookupDescription", System.Data.DbType.String, lookup.LookupDescription);
                db.AddInParameter(savecommand, "LookupCategory", System.Data.DbType.String, lookup.LookupCategory);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, lookup.Status);
                db.AddInParameter(savecommand, "ISOCode", System.Data.DbType.String, lookup.ISOCode);
                db.AddInParameter(savecommand, "MappingCode", System.Data.DbType.String, lookup.MappingCode);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, lookup.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, lookup.ModifiedBy);



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
        public bool Add(Lookup LookupCode) 
        {
            var result = false;
            var lookup = (Lookup)(object)LookupCode;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var AddCommand = db.GetStoredProcCommand(DBRoutine.ADDLOOKUP);
                db.AddInParameter(AddCommand, "LookupID", System.Data.DbType.Int16, lookup.LookupID);
                db.AddInParameter(AddCommand, "LookupCode", System.Data.DbType.String, lookup.LookupCode);

                result = Convert.ToBoolean(db.ExecuteNonQuery(AddCommand, transaction));

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
            var lookup = (Lookup)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETELOOKUP);

                db.AddInParameter(deleteCommand, "LookupID", System.Data.DbType.Int16, lookup.LookupID);

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
            var item = (Lookup)lookupItem;

            var currentItem = db.ExecuteSprocAccessor(DBRoutine.SELECTLOOKUP,
                                                    MapBuilder<Lookup>.BuildAllProperties(),
                                                    item.LookupID).FirstOrDefault();
            return currentItem;
        }

        #endregion






    }
}


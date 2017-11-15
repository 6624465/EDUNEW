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
using EZY.EDU.Contract;

namespace EZY.EDU.DataFactory
{
    public class UserRightsDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserRightsDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<UserRights> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTUSERRIGHTS, MapBuilder<UserRights>.BuildAllProperties()).ToList();
        }

        public List<UserRights> GetListByUser(string userID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTUSERRIGHTSBYUSER, MapBuilder<UserRights>.BuildAllProperties(), userID).ToList();
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

            var userrights = (UserRights)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction); 

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEUSERRIGHTS);

                db.AddInParameter(savecommand, "UserID", System.Data.DbType.String, userrights.UserID);
                db.AddInParameter(savecommand, "SecurableItem", System.Data.DbType.String, userrights.SecurableItem);
                db.AddInParameter(savecommand, "Permission", System.Data.DbType.String, userrights.Permission);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, userrights.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, userrights.ModifiedBy);



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
            var userrights = (UserRights)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEUSERRIGHTS);



                db.AddInParameter(deleteCommand, "UserID", System.Data.DbType.String, userrights.UserID);
                db.AddInParameter(deleteCommand, "SecurableItem", System.Data.DbType.String, userrights.SecurableItem);
                db.AddInParameter(deleteCommand, "Permission", System.Data.DbType.String, userrights.Permission);


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
            var userrights = ((UserRights)lookupItem);

            var userrightsItem = db.ExecuteSprocAccessor(DBRoutine.SELECTUSERRIGHTS,
                                                    MapBuilder<UserRights>.BuildAllProperties(),
                                                    userrights.UserID,
                                                    userrights.SecurableItem,
                                                    userrights.Permission).FirstOrDefault();
            return userrightsItem;
        }

        #endregion






    }
}


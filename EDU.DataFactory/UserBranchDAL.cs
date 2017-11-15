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
    public class UserBranchDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        public UserBranchDAL()
        {
            db = DatabaseFactory.CreateDatabase("EDU");
        }

        public List<UserBranch> GetList(string UserID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.USERBRANCHLIST,
                                            MapBuilder<UserBranch>
                                            .MapAllProperties()
                                            .Build(), UserID).ToList();
        }

        public List<UserBranchSelectedList> GetUserBranchSelectedList(string UserID, string CompanyCode)
        {
            return db.ExecuteSprocAccessor(DBRoutine.USERBRANCHSELECTEDLIST,
                                            MapBuilder<UserBranchSelectedList>
                                            .MapAllProperties()
                                            .Build(), UserID, CompanyCode).ToList();
        }

        public bool Delete(string userID, string companyCode)
        {
            var result = false;            

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEUSERBRANCH);

                db.AddInParameter(deleteCommand, "UserID", System.Data.DbType.String, userID.Trim());
                db.AddInParameter(deleteCommand, "CompanyCode", System.Data.DbType.String, companyCode);
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

            var userBranch = (UserBranch)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.INSERTUSERBRANCH);

                db.AddInParameter(savecommand, "UserID", System.Data.DbType.String, userBranch.UserID);
                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, userBranch.BranchID);
                db.AddInParameter(savecommand, "CompanyCode", System.Data.DbType.String, userBranch.CompanyCode);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, userBranch.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, userBranch.ModifiedBy);

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
    }
}

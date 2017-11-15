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
    public class UsersDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public UsersDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<Users> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTUSERS,
                                            MapBuilder<Users>
                                            .MapAllProperties()
                                            .DoNotMap(x => x.userBranchList)
                                            .Build()
                                            ).ToList();
        }

        public bool Save<T>(T item, string CompanyID, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item, CompanyID);

        }




        public bool Save<T>(T item, string CompanyID) where T : IContract
        {
            var result = 0;

            var users = (Users)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEUSERS);

                db.AddInParameter(savecommand, "UserID", System.Data.DbType.String, users.UserID);
                db.AddInParameter(savecommand, "UserName", System.Data.DbType.String, users.UserName);
                db.AddInParameter(savecommand, "Password", System.Data.DbType.String, users.Password);
                db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, true);
                db.AddInParameter(savecommand, "Email", System.Data.DbType.String, users.Email == null ? "" : users.Email);
                db.AddInParameter(savecommand, "MobileNumber", System.Data.DbType.String, users.MobileNumber == null ? "" : users.MobileNumber);
                db.AddInParameter(savecommand, "LogInStatus", System.Data.DbType.Boolean, true);
                db.AddInParameter(savecommand, "RoleCode", System.Data.DbType.String, users.RoleCode);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, users.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, users.ModifiedBy);


                result = db.ExecuteNonQuery(savecommand, transaction);

                if(result > 0)
                {
                    result = new UserBranchDAL().Delete(users.UserID, CompanyID) ? 1 : 0;

                    var userBranchList = new List<EDU.Contract.UserBranch>();
                    for (var i = 0; i < users.userBranchList.Count; i++)
                    {
                        if (users.userBranchList[i].IsSelected)
                        {
                            var obj = new EDU.Contract.UserBranch
                            {
                                UserID = users.userBranchList[i].UserID == null ? users.UserID : users.userBranchList[i].UserID,
                                BranchID = users.userBranchList[i].BranchID,
                                CompanyCode = CompanyID,
                                CreatedBy = users.CreatedBy,
                                ModifiedBy = users.ModifiedBy
                            };

                            userBranchList.Add(obj);
                        }

                    }

                    result = new UserBranchDAL().SaveList(userBranchList, transaction) ? 1 : 0;
                }



                transaction.Commit();

            }
            catch (Exception ex)
            {

                transaction.Rollback();

                throw;
            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var users = (Users)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEUSERS);


                db.AddInParameter(deleteCommand, "UserID", System.Data.DbType.String, users.UserID);


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
            var item = ((Users)lookupItem);

            var usersItem = db.ExecuteSprocAccessor(DBRoutine.SELECTUSERS,
                                                    MapBuilder<Users>
                                                    .MapAllProperties()
                                                    .DoNotMap(x => x.userBranchList)
                                                    .Build(),
                                                    item.UserID).FirstOrDefault();



            return usersItem;
        }
        #endregion
    }
}


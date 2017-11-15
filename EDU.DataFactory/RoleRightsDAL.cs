using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.DataFactory
{
    public class RoleRightsDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public RoleRightsDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<RoleRights> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTROLERIGHTS, MapBuilder<RoleRights>.BuildAllProperties()).ToList();
        }



        public List<RoleRights> GetList(string roleCode)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTROLERIGHTSBYROLE, MapBuilder<RoleRights>.BuildAllProperties(), roleCode).ToList();
        }

        public List<Securables> GetSecurableItemsList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTSECURABLES, MapBuilder<Securables>.BuildAllProperties()).ToList();
        }

        public List<Securables> GetSecurableItemsListByRoleCode(string roleCode)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTSECURABLESBYROLE, 
                MapBuilder<Securables>.BuildAllProperties(), roleCode).ToList();
        }

        public bool SaveList<T>(List<T> items) where T : IContract
        {


            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();

            }
            var result = true;
            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);
            try
            {
                currentTransaction = transaction;



                var roleCode = items.Select(x => x as RoleRights).ToList().FirstOrDefault().RoleCode;

                result = DeleteAllRightsOfRole(roleCode, currentTransaction);

                if (items.Count == 0)
                    result = true;

                foreach (var item in items)
                {
                    result = Save(item);
                    if (result == false) break;
                }

                if (result)
                    transaction.Commit();


            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
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

            var rolerights = (RoleRights)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEROLERIGHTS);

                db.AddInParameter(savecommand, "RoleCode", System.Data.DbType.String, rolerights.RoleCode);
                db.AddInParameter(savecommand, "SecurableItem", System.Data.DbType.String, rolerights.SecurableItem);



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

        public bool DeleteAllRightsOfRole(string roleCode, DbTransaction parentTransaction)
        {
            currentTransaction = parentTransaction;
            return DeleteAllRightsOfRole(roleCode);
        }

        public bool DeleteAllRightsOfRole(string roleCode)
        {
            var result = false;


            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);



            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEALLRIGHTSOFROLE);


                db.AddInParameter(deleteCommand, "RoleCode", System.Data.DbType.String, roleCode);


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


        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var rolerights = (RoleRights)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEROLERIGHTS);


                db.AddInParameter(deleteCommand, "RoleCode", System.Data.DbType.String, rolerights.RoleCode);
                db.AddInParameter(deleteCommand, "SecurableItem", System.Data.DbType.String, rolerights.SecurableItem);

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
            var obj = ((RoleRights)lookupItem);

            var rolerightsItem = db.ExecuteSprocAccessor(DBRoutine.SELECTROLERIGHTS,
                                                    MapBuilder<RoleRights>.BuildAllProperties(),
                                                    obj.RoleCode, obj.SecurableItem).FirstOrDefault();
            return rolerightsItem;
        }

        #endregion

    }
}

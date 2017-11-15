using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.EDU.Contract;

namespace EZY.EDU.DataFactory
{
    public class RolesDAL
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public RolesDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<Roles> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTROLES, MapBuilder<Roles>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var roles = (Roles)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEROLES);

                db.AddInParameter(savecommand, "RoleCode", System.Data.DbType.String, roles.RoleCode);
                db.AddInParameter(savecommand, "RoleDescription", System.Data.DbType.String, roles.RoleDescription);
                db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, roles.IsActive);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, roles.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, roles.ModifiedBy);


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

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var roles = (Roles)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEROLES);



                db.AddInParameter(deleteCommand, "RoleCode", System.Data.DbType.String, roles.RoleCode);

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
            var obj = ((Roles)lookupItem);

            var rolesItem = db.ExecuteSprocAccessor(DBRoutine.SELECTROLES,
                                                    MapBuilder<Roles>.BuildAllProperties(),
                                                    obj.RoleCode).FirstOrDefault();
            return rolesItem;
        }

        #endregion

    }
}

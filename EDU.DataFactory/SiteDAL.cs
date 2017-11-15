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
    public class SiteDAL : IDataFactory
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public SiteDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<Site> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTSITE, MapBuilder<Site>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var site = (Site)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVESITE);

                db.AddInParameter(savecommand, "SitePrefix", System.Data.DbType.String, site.SitePrefix);
                db.AddInParameter(savecommand, "SiteCode", System.Data.DbType.String, site.SiteCode);
                db.AddInParameter(savecommand, "SiteName", System.Data.DbType.String, site.SiteName);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, site.Status);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, site.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, site.ModifiedBy);




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
            var site = (Site)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETESITE);

                db.AddInParameter(deleteCommand, "SiteCode", System.Data.DbType.String, site.SiteCode);

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

            var item = (Site)lookupItem;

            var siteItem = db.ExecuteSprocAccessor(DBRoutine.SELECTSITE,
                                                    MapBuilder<Site>.BuildAllProperties(),
                                                    item.SiteCode).FirstOrDefault();
            return siteItem;
        }

        #endregion






    }
}


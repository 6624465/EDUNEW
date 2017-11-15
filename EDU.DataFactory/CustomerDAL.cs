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
    public class CustomerDAL : IDataFactory
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerDAL()
        {

            db = DatabaseFactory.CreateDatabase("EDU");

        }

        #region IDataFactory Members

        public List<Customer> GetList(short branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTCUSTOMER, 
                                                MapBuilder<Customer>
                                                .MapAllProperties()
                                                .DoNotMap(c=> c.CustomerTypeDescription)
                                                .Build(),branchID).ToList();
        }

        //public List<Customer> GetCustomerTableDataList(DataTableObject Obj)
        //{
        //    return db.ExecuteSprocAccessor(DBRoutine.SELECTCUSTOMERTABLEDATA,
        //        MapBuilder<Customer>.BuildAllProperties(),
        //        Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType,Obj.BranchID).ToList();
        //}


        public List<Customer> GetCustomerTableDataList(DataTableObject Obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.CUSTOMERPAGEVIEW,
                MapBuilder<Customer>.BuildAllProperties(),
                 Obj.BranchID,Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType).ToList();
        }

        

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var customer = (Customer)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVECUSTOMER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, customer.BranchID);
                db.AddInParameter(savecommand, "CustomerCode", System.Data.DbType.String, customer.CustomerCode);
                db.AddInParameter(savecommand, "CustomerName", System.Data.DbType.String, customer.CustomerName);
                db.AddInParameter(savecommand, "CustomerType", System.Data.DbType.Int16, customer.CustomerType);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, customer.Status);
                db.AddInParameter(savecommand, "Address1", System.Data.DbType.String, customer.Address1);
                db.AddInParameter(savecommand, "Address2", System.Data.DbType.String, customer.Address2);
                db.AddInParameter(savecommand, "State", System.Data.DbType.String, customer.State);
                db.AddInParameter(savecommand, "CountryCode", System.Data.DbType.String, customer.CountryCode);
                db.AddInParameter(savecommand, "PostCode", System.Data.DbType.String, customer.PostCode);
                db.AddInParameter(savecommand, "PhoneNumber", System.Data.DbType.String, customer.PhoneNumber);
                db.AddInParameter(savecommand, "FaxNumber", System.Data.DbType.String, customer.FaxNumber);
                db.AddInParameter(savecommand, "ContactPerson", System.Data.DbType.String, customer.ContactPerson);
                db.AddInParameter(savecommand, "MobilePhoneNumber", System.Data.DbType.String, customer.MobilePhoneNumber);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, customer.EmailID);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, customer.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, customer.ModifiedBy);




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
            var customer = (Customer)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETECUSTOMER);

                db.AddInParameter(deleteCommand, "CustomerCode", System.Data.DbType.String, customer.CustomerCode);
                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, customer.BranchID);

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

            var item = (Customer)lookupItem;

            var customerItem = db.ExecuteSprocAccessor(DBRoutine.SELECTCUSTOMER,
                                                MapBuilder<Customer>
                                                .MapAllProperties()
                                                .DoNotMap(c => c.CustomerTypeDescription)
                                                .Build(),
                                                item.BranchID, item.CustomerCode).FirstOrDefault();



            return customerItem;
        }

        #endregion






    }
}


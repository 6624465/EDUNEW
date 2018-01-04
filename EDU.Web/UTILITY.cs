using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EDU.Web
{
    public static class UTILITY
    {
        //public static string REPORTSUBFOLDER = WebConfigurationManager.AppSettings["ReportPath"].ToString();
        public static string SSN_USER_OBJECT = "SSN_USER";
        public static string SSN_USER_SECURABLES = "SSN_SECURABLES";

        public static short CONFIG_CUSTOMERTYPE_MANUFACTURER = 101;
        public static short CONFIG_CUSTOMERTYPE_VENDOR = 102;
        public static short CONFIG_CUSTOMERTYPE_CUSTOMER = 103;

        public static short CONFIG_INVOICETYPE_VENDOR = 120;
        public static short CONFIG_INVOICETYPE_CUSTOMER = 121;

        public static short CONFIG_REPLACEMENTTYPE_REPLACEMENT = 125;
        public static short CONFIG_REPLACEMENTTYPE_CREDITNOTE = 126;
        public static short CONFIG_REPLACEMENTTYPE_OPEN = 127;

        public static string CONFIG_LOOKUPCATEGORY_CUSTOMERTYPE = "CustomerType";
        public static string CONFIG_LOOKUPCATEGORY_INVOICETYPE = "InvoiceType";
        public static string CONFIG_LOOKUPCATEGORY_REPLACEMENTTYPE = "ReplacementType";
        public static string CONFIG_LOOKUPCATEGORY_CATEGORY = "CategoryGroup";
        //public static string CONFIG_LOOKUPCATEGORY_PRODUCTCATEGORY = "ProductCategory";
        public static string CONFIG_LOOKUPCATEGORY_CURRENCY = "Currency";


        public static string SECURABLE_ADMINISTRATION = "Administration";
        public static string SECURABLE_COMPANYPROFILE = "CompanyProfile";
        public static string SECURABLE_COMPANYPROFILEEDIT = "CompanyProfileEdit";
        public static string SECURABLE_COMPANYPROFILESAVE = "CompanyProfileSave";
        public static string SECURABLE_CONFIG = "Config";
        public static string SECURABLE_CUSTOMERADD = "CustomerAdd";
        public static string SECURABLE_CUSTOMERDELETE = "CustomerDelete";
        public static string SECURABLE_CUSTOMEREDIT = "CustomerEdit";
        public static string SECURABLE_CUSTOMERINVOICEADD = "CustomerInvoiceAdd";
        public static string SECURABLE_CUSTOMERINVOICEDELETE = "CustomerInvoiceDelete";
        public static string SECURABLE_CUSTOMERINVOICEEDIT = "CustomerInvoiceEdit";
        public static string SECURABLE_CUSTOMERINVOICELIST = "CustomerInvoiceList";
        public static string SECURABLE_CUSTOMERINVOICESAVE = "CustomerInvoiceSave";
        public static string SECURABLE_CUSTOMERS = "Customers";
        public static string SECURABLE_CUSTOMERSAVE = "CustomerSave";
        public static string SECURABLE_MANAGEMENTREPORTS = "ManagementReports";
        public static string SECURABLE_MASTER = "Master";
        public static string SECURABLE_OPERATIONALREPORTS = "OperationalReports";
        public static string SECURABLE_PRODUCTADD = "ProductAdd";
        public static string SECURABLE_PRODUCTDELETE = "ProductDelete";
        public static string SECURABLE_PRODUCTEDIT = "ProductEdit";
        public static string SECURABLE_PRODUCTS = "Products";
        public static string SECURABLE_PRODUCTSAVE = "ProductSave";
        public static string SECURABLE_REPORTS = "Reports";
        public static string SECURABLE_ROLERIGHTS = "RoleRights";
        public static string SECURABLE_SERIALNOINVOICE = "SerialNoInvoice";
        public static string SECURABLE_TRANSACTIONS = "Transactions";
        public static string SECURABLE_USERSLIST = "UsersList";
        public static string SECURABLE_VENDORINVOICEADD = "VendorInvoiceAdd";
        public static string SECURABLE_VENDORINVOICEDELETE = "VendorInvoiceDelete";
        public static string SECURABLE_VENDORINVOICEEDIT = "VendorInvoiceEdit";
        public static string SECURABLE_VENDORINVOICELIST = "VendorInvoiceList";
        public static string SECURABLE_VENDORINVOICESAVE = "VendorInvoiceSave";

        //public static DateTime SingaporeTime(this DateTime value)
        //{
        //    string nzTimeZoneKey = "SE Asia Standard Time";
        //    TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
        //    return TimeZoneInfo.ConvertTimeFromUtc(value, nzTimeZone);
        //}

        public static DateTime SINGAPORETIME
        {
            get
            {
                string nzTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, nzTimeZone);
            }

        }

        public static bool HasRight(string securableItem)
        {
            var securables = (List<Securables>)System.Web.HttpContext.Current.Session[UTILITY.SSN_USER_SECURABLES];

            var obj = securables.Where(x => x.SecurableItem == securableItem)
                        .FirstOrDefault();

            if (obj != null)
                return true;
            else
                return false;

        }

        public static bool HasRight(string securableItem, string actionType)
        {
            var securables = (List<Securables>)System.Web.HttpContext.Current.Session[UTILITY.SSN_USER_SECURABLES];

            var obj = securables.Where(x => x.SecurableItem == securableItem && x.ActionType == actionType)
                        .FirstOrDefault();

            if (obj != null)
                return true;
            else
                return false;

        }
    }
}
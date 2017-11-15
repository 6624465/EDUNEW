using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.DataFactory
{
    public class DBRoutine
    {
        /// <summary>
		/// [Config].[Lookup]
		/// </summary>
        public const string  ADDLOOKUP = "[Config].[usp_LookupAdd]";
        public const string  DELETELOOKUP = "[Config].[usp_LookupDelete]";
		public const string  LISTLOOKUP = "[Config].[usp_LookupList]";
		public const string  SAVELOOKUP = "[Config].[usp_LookupSave]";
		public const string  SELECTLOOKUP = "[Config].[usp_LookupSelect]";


		/// <summary>
		/// [Master].[Customer]
		/// </summary>

		public const string  DELETECUSTOMER = "[Master].[usp_CustomerDelete]";
		public const string  LISTCUSTOMER = "[Master].[usp_CustomerList]";
		public const string  SAVECUSTOMER = "[Master].[usp_CustomerSave]";
		public const string  SELECTCUSTOMER = "[Master].[usp_CustomerSelect]";
        public const string SELECTCUSTOMERTABLEDATA = "[Master].[CustomerTableData]";
        public const string CUSTOMERPAGEVIEW = "[Master].[usp_CustomerPageView]";
        


        /// <summary>
        /// [Master].[Product]
        /// </summary>

        public const string  DELETEPRODUCT = "[Master].[usp_ProductDelete]";
		public const string  LISTPRODUCT = "[Master].[usp_ProductList]";
		public const string  SAVEPRODUCT = "[Master].[usp_ProductSave]";
		public const string  SELECTPRODUCT = "[Master].[usp_ProductSelect]";
        public const string SELECTPRODUCTTABLEDATA = "[Master].[ProductTableData]";

        /// <summary>
        /// [Edu].[Product]
        /// </summary>

        public const string DELETEEDUPRODUCT = "[Edu].[usp_ProductDelete]";
        public const string LISTEDUPRODUCT = "[Edu].[usp_ProductList]";
        public const string SAVEEDUPRODUCT = "[Edu].[usp_ProductSave]";
        public const string SELECTEDUPRODUCT = "[Edu].[usp_ProductSelect]";
        public const string SELECTEDUPRODUCTTABLEDATA = "[Edu].[ProductTableData]";
        public const string PRODUCTCOUNT = "[Edu].[usp_ProductCount]";

        /// <summary>
        /// [Edu].[Course]
        /// </summary>

        public const string DELETEEDUCOURSE = "[Edu].[usp_CourseDelete]";
        public const string LISTEDUCOURSE = "[Edu].[usp_CourseList]";
        public const string SAVEEDUCOURSE = "[Edu].[usp_CourseSave]";
        public const string SELECTEDUCOURSE = "[Edu].[usp_CourseSelect]";
        public const string SELECTEDUCOURSETABLEDATA = "[Edu].[CourseTableData]";
        public const string COURSESBYPRODUCT = "[Edu].[usp_CoursesByProduct]";
        public const string COURSECOUNT = "[Edu].[usp_CourseCount]";

        /// <summary>
        /// [Edu].[CourseDetail]
        /// </summary>

        public const string DELETEEDUCOURSEDETAIL = "[Edu].[usp_CourseDetailDelete]";
        public const string LISTEDUCOURSEDETAIL = "[Edu].[usp_CourseDetailList]";
        public const string SAVEEDUCOURSEDETAIL = "[Edu].[usp_CourseDetailSave]";
        public const string SELECTEDUCOURSEDETAIL = "[Edu].[usp_CourseDetailSelect]";
        public const string SELECTEDUCOURSEDETAILTABLEDATA = "[Edu].[CourseDetailTableData]";

        /// <summary>
        /// [Edu].[CourseSalesMaster]
        /// </summary>

        public const string DELETEEDUCOURSESALESMASTER = "[Edu].[usp_CourseSalesMasterDelete]";
        public const string LISTEDUCOURSESALESMASTER = "[Edu].[usp_CourseSalesMasterList]";
        public const string SAVEEDUCOURSESALESMASTER = "[Edu].[usp_CourseSalesMasterSave]";
        public const string SELECTEDUCOURSESALESMASTER = "[Edu].[usp_CourseSalesMasterSelect]";
        public const string SELECTEDUCOURSESALESMASTERTABLEDATA = "[Edu].[CourseSalesMasterTableData]";

        /// <summary>
        /// [Master].[Site]
        /// </summary>

        public const string  DELETESITE = "[Master].[usp_SiteDelete]";
		public const string  LISTSITE = "[Master].[usp_SiteList]";
		public const string  SAVESITE = "[Master].[usp_SiteSave]";
		public const string  SELECTSITE = "[Master].[usp_SiteSelect]";



        		/// <summary>
		/// [Operation].[VendorInvoiceDetail]
		/// </summary>

        public const string DELETEINVOICEDETAIL = "[Operation].[usp_InvoiceDetailDelete]";
        public const string LISTINVOICEDETAIL = "[Operation].[usp_InvoiceDetailList]";
        public const string SAVEINVOICEDETAIL = "[Operation].[usp_InvoiceDetailSave]";
        public const string SELECTINVOICEDETAIL = "[Operation].[usp_InvoiceDetailSelect]";
        public const string INVOICEDETAILBYDOCUMENTINVOICENO = "[Operation].[InvoiceDetailByDocumentInvoiceNo]";
        public const string INVOICEDETAILSBYSERIALNO = "[Operation].[usp_InvoiceDetailsBySerialNo]";
        public const string VENDORDETAILSBYSERIALNO = "[Operation].[usp_VendorInvoiceDetailsBySerialNo]";

        public const string SERIALNOSTATUSDETAILS = "[Operation].[usp_CheckIsValidDetailsBySerialNo]";

        /// <summary>
        /// [Operation].[VendorInvoiceHeader]
        /// </summary>
        public const string DELETEINVOICEHEADER = "[Operation].[usp_InvoiceHeaderDelete]";
        public const string LISTINVOICEHEADER = "[Operation].[usp_InvoiceHeaderList]";
        public const string LISTINVOICEHEADER2 = "[Operation].[usp_InvoiceHeaderList2]";
        public const string LISTINVOICEHEADERDATATABLELIST = "[Operation].[usp_InvoiceHeaderDataTableList]";
        public const string LISTINVOICEHEADERDATATABLELIST2 = "[Operation].[usp_InvoiceHeaderDataTableList2]";
        public const string SAVEINVOICEHEADER = "[Operation].[usp_InvoiceHeaderSave]";
        public const string UPDATEINVOICEHEADER = "[Operation].[usp_InvoiceHeaderUpdates]";
        public const string SELECTINVOICEHEADER = "[Operation].[usp_InvoiceHeaderSelect]";

        public const string SERIALNOINQUIRY = "[Operation].[usp_SerialNoInquiry]";
        public const string DELETESERIALNO = "[Operation].[usp_SerialNoDelete]";
        public const string CHECKINWARDSERIALNO = "[Operation].[usp_CheckInwardSerialNo]";

        /// <summary>
        /// [Security].[Users]
        /// </summary>

        public const string SELECTUSERS = "[Security].[usp_UsersSelect]";
        public const string LISTUSERS = "[Security].[usp_UsersList]";
        public const string SAVEUSERS = "[Security].[usp_UsersSave]";
        public const string DELETEUSERS = "[Security].[usp_UsersDelete]";
        public const string USERBRANCHLIST = "[Master].[UserBranchList]";
        public const string USERBRANCHSELECTEDLIST = "[Master].[UserBranchSelectedList]";

        public const string VALIDATEUSERLOGIN = "[Security].[usp_ValidateUserLogin]";


        public const string  DELETEUSERRIGHTS = "[Security].[usp_UserRightsDelete]";
		public const string  LISTUSERRIGHTS = "[Security].[usp_UserRightsList]";
        public const string LISTUSERRIGHTSBYUSER = "[Security].[usp_UserRightsListByUser]";
        
		public const string  SAVEUSERRIGHTS = "[Security].[usp_UserRightsSave]";
		public const string  SELECTUSERRIGHTS = "[Security].[usp_UserRightsSelect]";

        /// <summary>
        /// [Master].[Address]
        /// </summary>

        public const string SELECTADDRESS = "[Master].[usp_AddressSelect]";
        public const string LISTADDRESS = "[Master].[usp_AddressList]";
        public const string SAVEADDRESS = "[Master].[usp_AddressSave]";
        public const string DELETEADDRESS = "[Master].[usp_AddressDelete]";
        public const string CONTACTLISTBYCUSTOMER = "[Master].[usp_ContactListByCustomer]";



        /// <summary>
        /// [Master].[Branch]
        /// </summary>

        public const string SELECTBRANCH = "[Master].[usp_BranchSelect]";
        public const string LISTBRANCH = "[Master].[usp_BranchList]";
        public const string LISTBRANCHBYCOMPANYCODE = "[Master].[usp_BranchListByCompanyCode]";
        public const string SAVEBRANCH = "[Master].[usp_BranchSave]";
        public const string DELETEBRANCH = "[Master].[usp_BranchDelete]";
        public const string DELETEUSERBRANCH = "[Master].[usp_UserBranchDelete]";
        public const string INSERTUSERBRANCH = "[Master].[usp_UserBranchInsert]";
        public const string LISTBRANCHBYCOMPANY = "[Master].[usp_BranchListByCompany]";


        /// <summary>
        /// [Master].[Company]
        /// </summary>

        public const string SELECTCOMPANY = "[Master].[usp_CompanySelect]";
        public const string LISTCOMPANY = "[Master].[usp_CompanyList]";
        public const string SAVECOMPANY = "[Master].[usp_CompanySave]";
        public const string DELETECOMPANY = "[Master].[usp_CompanyDelete]";


        /// <summary>
        /// [Master].[Country]
        /// </summary>
        /// 

        /// <summary>
        /// [Master].[Country]
        /// </summary>

        public const string SELECTCOUNTRY = "[Master].[usp_CountrySelect]";
        public const string LISTCOUNTRY = "[Master].[usp_CountryList]";
        public const string SAVECOUNTRY = "[Master].[usp_CountrySave]";
        public const string DELETECOUNTRY = "[Master].[usp_CountryDelete]";
        

        /// <summary>
        /// [Security].[RoleRights]
        /// </summary>

        public const string SELECTROLERIGHTS = "[Security].[usp_RoleRightsSelect]";
        public const string LISTROLERIGHTS = "[Security].[usp_RoleRightsList]";
        public const string SAVEROLERIGHTS = "[Security].[usp_RoleRightsSave]";
        public const string DELETEROLERIGHTS = "[Security].[usp_RoleRightsDelete]";
        public const string LISTROLERIGHTSBYROLE = "[Security].[usp_RoleRightsListByRole]";
        public const string LISTSECURABLES = "[Security].[usp_SecurablesList]";
        public const string DELETEALLRIGHTSOFROLE = "[Security].[usp_DeleteRightsOfRole]";
        public const string LISTSECURABLESBYROLE = "[Security].[usp_SecurablesListByRole]";

        /// <summary>
        /// [Security].[Roles]
        /// </summary>

        public const string SELECTROLES = "[Security].[usp_RolesSelect]";
        public const string LISTROLES = "[Security].[usp_RolesList]";
        public const string SAVEROLES = "[Security].[usp_RolesSave]";
        public const string DELETEROLES = "[Security].[usp_RolesDelete]";

        ///// <summary>
        ///// [Operation].[RMAOutwardHeader]
        ///// </summary>

        //public const string SELECTRMAOUTWARDHEADER = "[Operation].[usp_RMAOutwardHeaderSelect]";
        //public const string LISTRMAOUTWARDHEADER = "[Operation].[usp_RMAOutwardHeaderList]";
        //public const string SAVERMAOUTWARDHEADER = "[Operation].[usp_RMAOutwardHeaderSave]";
        //public const string DELETERMAOUTWARDHEADER = "[Operation].[usp_RMAOutwardHeaderDelete]";

        ///// <summary>
        ///// [Operation].[RMAOutwardDetail]
        ///// </summary>

        //public const string SELECTRMAOUTWARDDETAIL = "[Operation].[usp_RMAOutwardDetailSelect]";
        //public const string LISTRMAOUTWARDDETAIL = "[Operation].[usp_RMAOutwardDetailList]";
        //public const string SAVERMAOUTWARDDETAIL = "[Operation].[usp_RMAOutwardDetailSave]";
        //public const string DELETERMAOUTWARDDETAIL = "[Operation].[usp_RMAOutwardDetailDelete]";
        //public const string RMAOUTWARDDETAILLISTBYRMAINWARDDOCUMENTNO = "[Operation].[usp_RMAOutWardDetailListByRMAInWardDocumentNo]";

        ///// <summary>
        ///// [Operation].[VendorRMAOutwardHeader]
        ///// </summary>
       
        //public const string SAVEVENDORRMAOUTWARDHEADER = "[Operation].[usp_VendorRMAOutwardHeaderSave]";
        //public const string SELECTVENDORRMAOUTWARDHEADER = "[Operation].[usp_VendorRMAOutwardHeaderSelect]";
        //public const string LISTVENDORRMAOUTWARDHEADER = " [Operation].[usp_VendorRMAOutwardHeaderList]";
        //public const string DELETEVENDORRMAOUTWARDHEADER = "[Operation].[usp_VendorRMAOutwardHeaderDelete]";


        ///// <summary>
        ///// [Operation].[VendorRMAOutwardDetail]
        ///// </summary>
        //public const string SELECTVENDORRMAOUTWARDDETAIL = "[Operation].[usp_VendorRmaOutwardDetailSelect]";
        //public const string LISTVENDORRMAOUTWARDDETAIL = "[Operation].[usp_VendorRmaOutwardDetailList]";
        //public const string SAVEVENDORRMAOUTWARDDETAIL = "[Operation].[usp_VendorRMAOutwardDetailSave]";
        //public const string DELETEVENDORRMAOUTWARDDETAIL = "[Operation].[usp_VendorRMAOutwardDetailDelete]";
        //public const string VENDORRMAOUTWARDDETAILLISTBYRMAINWARDDOCUMENTNO = "[Operation].[usp_VendorRMADetailListByDocumentNo]";

        ///// <summary>
        ///// [Operation].[RMADetail]
        ///// </summary>

        //public const string SELECTRMADETAIL = "[Operation].[usp_RMADetailSelect]";
        //public const string LISTRMADETAIL = "[Operation].[usp_RMADetailList]";
        //public const string SAVERMADETAIL = "[Operation].[usp_RMADetailSave]";
        //public const string DELETERMADETAIL = "[Operation].[usp_RMADetailDelete]";
        //public const string LISTRMADETAILBYDOCUMENTNO = "[Operation].[usp_RMADetailListByDocumentNo]";

        ///// <summary>
        ///// [Operation].[RMAHeader]
        ///// </summary>

        //public const string SELECTRMAHEADER = "[Operation].[usp_RMAHeaderSelect]";
        //public const string LISTRMAHEADER = "[Operation].[usp_RMAHeaderList]";
        //public const string SAVERMAHEADER = "[Operation].[usp_RMAHeaderSave]";
        //public const string DELETERMAHEADER = "[Operation].[usp_RMAHeaderDelete]";
        //public const string RMAHEADERLISTBYBRANCHID = "[Operation].[usp_RMAHeaderListByBranchID]";
        //public const string SELECTRMAHEADERBYDOCUMENTNO = "[Operation].[usp_RMAHeaderSelectByDocumentNo]";
        //public const string CLOSERMAHEADER = "[Operation].[usp_RMAHeaderClose]";


        ///// <summary>
        ///// [Operation].[VendorRMAHeader]
        ///// </summary>


        //public const string SAVEVENDORRMAHEADER = "[Operation].[usp_VendorRMAHeaderSave]";
        //public const string VENDORRMAHEADERLISTBYBRANCHID = "[Operation].[usp_VendorRMAHeaderListByBranchID]";
        //public const string CLOSEVENDORRMAHEADER = "[Operation].[usp_VendorRMAHeaderClose]";

        ///// <summary>
        ///// [Operation].[VendorRMADetail]
        ///// </summary>
        //public const string SAVEVENDORRMADETAIL = "[Operation].[usp_VendorRMADetailSave]";


        /////[Operation].[DashBoard]

        //public const string ORDERCOUNTDASHBOARD = "[Operation].[usp_DashboardForMaterialInward1]";

        //public const string MATERIALINWARDDASHBOARDCOUNT = "[Operation].[usp_MaterialInwardReport]";
        //public const string MATERIALOUTWARDDASHBOARDCOUNT = "[Operation].[usp_MaterialOutwardReport]";
        //public const string RMAINWARDDASHBOARDCOUNT = "[Operation].[usp_RMAInwardReport]";
        //public const string RMAOUTWARDDASHBOARDCOUNT = "[Operation].[usp_RMAOutwardReport]";

        //public const string MATERIALINANDOUTDASHBOARDCOUNT = "[Operation].[usp_MaterialInwardAndOutwardReport]";
        //public const string RMAINANDOUTDASHBOARDCOUNT = "[Operation].[usp_RMAInwardAndOutwardReport]";

        //public const string MATERIALINSUMOFQTYANDPRICE = "[Operation].[usp_MaterialInwardMonthReportForQtyandPrice]";
        //public const string MATERIALOUTSUMOFQTYANDPRICE = "[Operation].[usp_MaterialOutwardMonthReportForQtyandPrice]";
        //public const string RMAINSUMOFQTYANDPRICE = "[Operation].[usp_RMAInwardMonthReportForQtyandPrice]";
        //public const string RMAOUTSUMOFQTYANDPRICE = "[Operation].[usp_RMAOutwardMonthReportForQtyandPrice]";

        //public const string MATERIALINSUMOFPRODUCTANDPRICE = "[Operation].[usp_MaterialInwardMonthReportForProductandPrice]";
        //public const string MATERIALOUTSUMOFPRODUCTANDPRICE = "[Operation].[usp_MaterialOutwardMonthReportForProductandPrice]";
        //public const string RMAINSUMOFPRODUCTANDPRICE = "[Operation].[usp_RMAInwardMonthReportForProductandPrice]";
        //public const string RMAOUTSUMOFPRODUCTANDPRICE = "[Operation].[usp_RMAOutwardMonthReportForProductandPrice]";
        //public const string TOTALMATERIALINWARDLISTPRODUCTWISE = "[Operation].[usp_MaterialInwardReportForTotalCountofProductsWise]";
        //public const string TOTALMATERIALINWARDMONTHLISTPRODUCTWISE = "[Operation].[usp_MaterialInwardMonthReportForTotalCountofProductsWise]";
        //public const string TOTALMATERIALINWARDPRODUCTWISECOUNT = "[Operation].[usp_MaterialInwardReportForTotalCountofProducts]";
        //public const string TOTALMATERIALINWARDMONTHLIST = "[Operation].[usp_MaterialInwardMonthReportForTotalCountofProducts]";

        /// <summary>
        /// [Master].[ProductCategory]
        /// </summary>

        public const string SELECTPRODUCTCATEGORY = "[Master].[usp_ProductCategorySelect]";
        public const string LISTPRODUCTCATEGORY = "[Master].[usp_ProductCategoryList]";
        public const string SAVEPRODUCTCATEGORY = "[Master].[usp_ProductCategorySave]";
        public const string DELETEPRODUCTCATEGORY = "[Master].[usp_ProductCategoryDelete]";

    }
}

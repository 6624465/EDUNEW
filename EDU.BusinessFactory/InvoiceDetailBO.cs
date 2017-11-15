using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class InvoiceDetailBO
    {
        private InvoiceDetailDAL vendorinvoicedetailDAL;
        public InvoiceDetailBO()
        {

            vendorinvoicedetailDAL = new InvoiceDetailDAL();
        }

        public List<InvoiceDetail> GetList()
        {
            return vendorinvoicedetailDAL.GetList();
        }


        public bool SaveVendorInvoiceDetail(InvoiceDetail newItem)
        {

            return vendorinvoicedetailDAL.Save(newItem);

        }

        public bool DeleteVendorInvoiceDetail(InvoiceDetail item)
        {
            return vendorinvoicedetailDAL.Delete(item);
        }

        public InvoiceDetail GetVendorInvoiceDetail(InvoiceDetail item)
        {
            return (InvoiceDetail)vendorinvoicedetailDAL.GetItem<InvoiceDetail>(item);
        }

        public RMAInwardDTO GetVendorInvoiceDetailBySerialNo(string serialNo, Int16 branchID, Int16 invoiceType)
        {
            return vendorinvoicedetailDAL.GetVendorInvoiceDetailBySerialNo(serialNo, branchID, invoiceType);
        }
        public VendorRMAInwardDTO GetVendorDetailBySerialNo(string serialNo, Int16 branchID, Int16 invoiceType)
        {
            return vendorinvoicedetailDAL.GetVendorDetailBySerialNo(serialNo, branchID, invoiceType);
        }
        public MaterialsEnquiryDetails GetDetailBySerialNo(string serialNo, Int16 branchID)
        {
            return vendorinvoicedetailDAL.GetDetailBySerialNo(serialNo, branchID);
        }
    }
}

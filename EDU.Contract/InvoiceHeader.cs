using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Contract
{
    public class InvoiceHeader : IContract
    {
        // Constructor 
        public InvoiceHeader() { }

        // Public Members 
        public short BranchID { get; set; }
        public string DocumentNo { get; set; }
        public string InvoiceNo { get; set; }
        public string CusInvoiceNo { get; set; }
        public Int16 InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string CustomerCode { get; set; }

        public string ProductCode { get; set; }

        public Int16 WarrantyPeriod { get; set; }

        public bool Status { get; set; }

        public string FileName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<InvoiceDetail> InvoiceDetailItems { get; set; }

        public Int16 Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string CurrencyCode { get; set; }

        public Int16 ProductCategory { get; set; }

        public string ModelNo { get; set; }

        public string CustomerName { get; set; }

        public string ProductCategoryDescription { get; set; }

        public string InvoiceTypeDescription { get; set; }


        public string ProductDescription { get; set; }

        public DateTime WarrantyExpiryDate { get; set; }

    }

    public class InvoiceHeaderData : IContract
    {         
        public InvoiceHeaderData() { }         

        public string DocumentNo { get; set; }
        public string InvoiceNo { get; set; }
        public string CusInvoiceNo { get; set; }

        public Int16 InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public Int16 Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime WarrantyExpiryDate { get; set; }
    }
}




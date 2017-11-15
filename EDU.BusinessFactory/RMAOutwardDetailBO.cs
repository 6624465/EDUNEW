using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    
    public class RMAOutwardDetailBO
    {
        private RMAOutwardDetailDAL rmaoutwarddetailDAL;
        public RMAOutwardDetailBO()
        {

            rmaoutwarddetailDAL = new RMAOutwardDetailDAL();
        }

        public List<RMAOutwardDetail> GetList(short branchID, string documentNo)
        {
            return rmaoutwarddetailDAL.GetList(  branchID,   documentNo);
        }


        public bool SaveVendorInvoiceDetail(RMAOutwardDetail newItem)
        {

            return rmaoutwarddetailDAL.Save(newItem);

        }

        public bool DeleteVendorInvoiceDetail(RMAOutwardDetail item)
        {
            return rmaoutwarddetailDAL.Delete(item);
        }

        public RMAOutwardDetail GetVendorInvoiceDetail(RMAOutwardDetail item)
        {
            return (RMAOutwardDetail)rmaoutwarddetailDAL.GetItem<RMAOutwardDetail>(item);
        }

        public List<RMAOutwardDetail> RMAOutWardDetailListByRMAInWardDocumentNo(string documentNo,Int16 BranchId)
        {
            return (List<RMAOutwardDetail>)rmaoutwarddetailDAL.RMAOutWardDetailListByRMAInWardDocumentNo(documentNo,BranchId);
        }
    }
}

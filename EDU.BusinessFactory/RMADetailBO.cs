using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
     
    public class RMADetailBO
    {
        private RMADetailDAL rmadetailDAL;
        public RMADetailBO()
        {

            rmadetailDAL = new RMADetailDAL();
        }

        public List<RMADetail> GetList(string documentNo)
        {
            return rmadetailDAL.GetList(documentNo);
        }
        public List<RMADetail> GetListByDocumentNo(string documentNo)
        {
            return rmadetailDAL.GetListByDocumentNo(documentNo);
        }
        public bool SaveVendorInvoiceDetail(RMADetail newItem)
        {

            return rmadetailDAL.Save(newItem);

        }

        public bool DeleteVendorInvoiceDetail(RMADetail item)
        {
            return rmadetailDAL.Delete(item);
        }

        public RMADetail GetVendorInvoiceDetail(RMADetail item)
        {
            return (RMADetail)rmadetailDAL.GetItem<RMADetail>(item);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;


namespace EZY.EDU.BusinessFactory
{
  public class VendorRMAOutwardHeaderBO
    {
        private VendorRMAOutwardHeaderDAL vendorRMAOutwardHeaderDAL;

        public VendorRMAOutwardHeaderBO()
        {
            vendorRMAOutwardHeaderDAL = new VendorRMAOutwardHeaderDAL();
        }
        public bool SaveVendorRMAOutwardHeader(VendorRMAOutwardHeader newItem)
        {
            return vendorRMAOutwardHeaderDAL.Save(newItem);
        }
        public bool DeleteRMAOutwardHeader(VendorRMAOutwardHeader item)
        {
            return vendorRMAOutwardHeaderDAL.Delete(item);
        }

        public VendorRMAOutwardHeader GetRMAOutwardHeader(VendorRMAOutwardHeader item)
        {
            return (VendorRMAOutwardHeader)vendorRMAOutwardHeaderDAL.GetItem<VendorRMAOutwardHeader>(item);
        }
        public List<VendorRMAOutwardHeader> GetList(short branchID)
        {
            return vendorRMAOutwardHeaderDAL.GetList(branchID);
        }
    }
}

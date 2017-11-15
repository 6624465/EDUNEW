using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public  class VendorRMAHeaderBO
    {
        private VendorRMAHeaderDAL vendorrmaheaderDAL;

        public VendorRMAHeaderBO()
        {

            vendorrmaheaderDAL = new VendorRMAHeaderDAL();
        }
        public bool SaveVendorRMAHeader(VendorRMAHeader newItem, Int16 branchID)
        {

            return vendorrmaheaderDAL.Save(newItem, branchID);

        }
        public List<VendorRMAHeader> GetListByBranchID(Int16 branchID)
        {
            return vendorrmaheaderDAL.GetListByBranchID(branchID);
        }
    }
}

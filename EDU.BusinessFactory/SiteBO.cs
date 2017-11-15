using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class SiteBO
    {
        private SiteDAL siteDAL;
        public SiteBO()
        {

            siteDAL = new SiteDAL();
        }

        public List<Site> GetList()
        {
            return siteDAL.GetList();
        }


        public bool SaveSite(Site newItem)
        {

            return siteDAL.Save(newItem);

        }

        public bool DeleteSite(Site item)
        {
            return siteDAL.Delete(item);
        }

        public Site GetSite(Site item)
        {
            return (Site)siteDAL.GetItem<Site>(item);
        }

    }
}

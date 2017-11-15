using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class EduProductBO
    {
        private EduProductDAL eduProductDAL;
        public EduProductBO()
        {
            eduProductDAL = new EduProductDAL();
        }

        public List<EduProduct> GetList()
        {
            return eduProductDAL.GetList();
        }

        public EduProduct GetEduProduct(EduProduct item)
        {
            return (EduProduct)eduProductDAL.GetItem<EduProduct>(item);
        }

        public bool SaveEduProduct(EduProduct newItem)
        {
            return eduProductDAL.Save(newItem);
        }
        public bool DeleteEduProduct(EduProduct item)
        {
            return eduProductDAL.Delete<EduProduct>(item);
        }

        public bool IsEduProductExists(EduProduct item)
        {
            return eduProductDAL.IsEduProductExists<EduProduct>(item);
        }
    }
}

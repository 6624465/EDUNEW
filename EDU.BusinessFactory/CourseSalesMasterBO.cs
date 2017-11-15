using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.EDU.DataFactory;
using EZY.EDU.Contract;

namespace EZY.EDU.BusinessFactory
{
    public class CourseSalesMasterBO
    {
        private CourseSalesMasterDAL courseSalesMasterDAL;
        public CourseSalesMasterBO()
        {
            courseSalesMasterDAL = new CourseSalesMasterDAL();
        }

        public List<CourseSalesMasterViewModel> GetList()
        {
            return courseSalesMasterDAL.GetList();
        }


        public bool SaveCourseSalesMaster(CourseSalesMaster newItem)
        {

            return courseSalesMasterDAL.Save(newItem);

        }

        public bool DeleteCourseSalesMaster(CourseSalesMaster item)
        {
            return courseSalesMasterDAL.Delete(item);
        }

        public CourseSalesMaster GetCourseSalesMaster(CourseSalesMaster item)
        {
            return (CourseSalesMaster)courseSalesMasterDAL.GetItem<CourseSalesMaster>(item);
        }
    }
}

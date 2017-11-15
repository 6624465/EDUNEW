using EZY.EDU.Contract;
using EZY.EDU.DataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EZY.EDU.BusinessFactory
{
    public class RolesBO
    {
        private RolesDAL rolesDAL;
        public RolesBO()
        {

            rolesDAL = new RolesDAL();
        }

        public List<Roles> GetList()
        {
            return rolesDAL.GetList();
        }


        public bool SaveRoles(Roles newItem)
        {

            return rolesDAL.Save(newItem);

        }

        public bool DeleteRoles(Roles item)
        {
            return rolesDAL.Delete(item);
        }

        public Roles GetRoles(Roles item)
        {
            return (Roles)rolesDAL.GetItem<Roles>(item);
        }

    }
}

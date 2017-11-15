using EZY.EDU.Contract;
using EZY.EDU.DataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.BusinessFactory
{
    public class RoleRightsBO
    {
        private RoleRightsDAL rolerightsDAL;
        public RoleRightsBO()
        {

            rolerightsDAL = new RoleRightsDAL();
        }

        public List<RoleRights> GetList()
        {
            return rolerightsDAL.GetList();
        }


        public List<RoleRights> GetList(string roleCode)
        {
            return rolerightsDAL.GetList(roleCode);
        }


        public List<Securables> GetSecurableItemsList()
        {
            return rolerightsDAL.GetSecurableItemsList();
        }

        public List<Securables> GetSecurableItemsListByRoleCode(string roleCode)
        {
            return rolerightsDAL.GetSecurableItemsListByRoleCode(roleCode);
        }

        public bool SaveRoleRights(RoleRights newItem)
        {

            return rolerightsDAL.Save(newItem);

        }

        public bool SaveRoleRights(List<RoleRights> roleRights)
        {

            return rolerightsDAL.SaveList(roleRights);

        }

        public bool DeleteRoleRights(RoleRights item)
        {
            return rolerightsDAL.Delete(item);
        }

        public bool DeleteAllRightsOfRole(string roleCode)
        {
            return rolerightsDAL.DeleteAllRightsOfRole(roleCode);
        }




        public RoleRights GetRoleRights(RoleRights item)
        {
            return (RoleRights)rolerightsDAL.GetItem<RoleRights>(item);
        }

    }
}

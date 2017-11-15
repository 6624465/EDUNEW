using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class UserRightsBO
    {
        private UserRightsDAL userrightsDAL;
        public UserRightsBO()
        {

            userrightsDAL = new UserRightsDAL();
        }

        public List<UserRights> GetList()
        {
            return userrightsDAL.GetList();
        }


        public bool SaveUserRights(UserRights newItem)
        {

            return userrightsDAL.Save(newItem);

        }

        public bool DeleteUserRights(UserRights item)
        {
            return userrightsDAL.Delete(item);
        }

        public UserRights GetUserRights(UserRights item)
        {
            return (UserRights)userrightsDAL.GetItem<UserRights>(item);
        }

    }
}

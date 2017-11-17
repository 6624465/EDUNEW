using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.EDU.Contract;
using EZY.EDU.BusinessFactory;

namespace EDU.Web.Controllers
{
    [WebSsnFilter]
    public class UsersController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var usersList = new UsersBO().GetList();
            return View("Index", usersList);
        }

        
        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult EditUser(string userID)
        {
            Users user = new Users();

            if (userID == "NEW")
            {
                userID = "";                
            }

            if (userID != null && userID.Length > 0)
                user = new UsersBO().GetUsers(new Users { UserID = userID });

            ViewBag.BranchList = new BranchBO()
                .GetListByCompanyCode(COMPANY_ID);

            user.userBranchList = new UserBranchBO().GetUserBranchSelectedList(user.UserID, COMPANY_ID);

            //ViewBag.userBranchSelectedList = Temp;

            //for (var i = 0; i < Temp.Count; i++)
            //{
            //    var userBranch = new UserBranch();

            //    userBranch.BranchID = Temp[i].BranchID;
            //    userBranch.UserID = Temp[i].UserID;

            //    user.userBranchList.Add(userBranch);
            //}

            user.RoleCodeList = new RolesBO().GetList().Select(x => new SelectListItem
            {
                Value = x.RoleCode,
                Text = x.RoleDescription
            }).ToList();

            return View("EditUser", user);
        }
        
        [HttpPost]
        public ActionResult SaveUser(Users user)
        {
            try
            {
                user.LogInStatus = true;
                user.CreatedBy = USER_ID;
                user.ModifiedBy = USER_ID;                

                var result = new UsersBO().SaveUsers(user, COMPANY_ID);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            var lstUsers = new UsersBO().GetList();
            return View("Index", lstUsers);
        }

        [HttpPost]
        public ActionResult DeleteUser(string UserID)
        {
            var result = new UsersBO().DeleteUsers(new Users { UserID = UserID });
            return RedirectToAction("Index");
        }
    }
}
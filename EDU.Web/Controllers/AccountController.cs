using EDU.Web.ViewModels.Account;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EDU.Web.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index1()
        {
            LoginViewModel model = new LoginViewModel();

            //var compist = new CompanyBO().GetList();
            //for(var i = 0;i < compist.Count;i++)
            //{
            //    compist[i].CompanyCode = UrlEncryptionHelper.Encrypt(compist[i].CompanyCode);
            //}
            //model.CompaniesList = new SelectList(compist, "CompanyCode", "CompanyName");
            var companyCode = "EZY";
            var branchList = new BranchBO().GetList().Where(x => x.CompanyCode == companyCode).ToList();
            model.BranchList = new SelectList(branchList, "BranchID", "BranchName").ToList();

            var compist = new CompanyBO().GetList();
            for (var i = 0; i < compist.Count; i++)
            {
                compist[i].CompanyCode = UrlEncryptionHelper.Encrypt(compist[i].CompanyCode);
            }
            model.CompanyCode = "EZY";
            model.CompaniesList = new SelectList(compist, "CompanyCode", "CompanyName");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var currentUser = new UsersBO().GetList().Where(
                ur => ur.UserID == model.UserID &&
                ur.Password == model.Password).FirstOrDefault();

            var userBranch = new UserBranchBO().GetList(model.UserID)
                                    .FirstOrDefault();

            var BranchDetails = new BranchBO().GetList().Where(x => x.BranchID == userBranch.BranchID).ToList();
            string BranchName = BranchDetails.Select(x => x.BranchName).FirstOrDefault();

            if (currentUser != null && userBranch != null)// 
            {
                FormsAuthentication.SetAuthCookie(currentUser.UserID, false);

                var _CompanyId = model.CompanyCode;
                var SsnObj = new SessionObject
                {
                    UserID = currentUser.UserID,
                    UserName = currentUser.UserName,
                    Email = currentUser.Email,
                    RoleCode = currentUser.RoleCode,
                    BranchId = userBranch.BranchID,
                    BranchName = BranchName
                };

                USER_OBJECT = SsnObj;
                USER_SECURABLES = new RoleRightsBO().GetSecurableItemsListByRoleCode(SsnObj.RoleCode);

                Session["UserID"] = currentUser.UserID;
                Session["UserName"] = currentUser.UserName;
                Session["BranchId"] = userBranch.BranchID;
                Session["RoleCode"] = currentUser.RoleCode.ToUpper();

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //ModelState.AddModelError("", "The user name or password provided is incorrect.");

                ViewBag.ErrMsg = "The user name or password provided is incorrect.";
                LoginViewModel modelObj = new LoginViewModel();

                var compist = new CompanyBO().GetList();
                for (var i = 0; i < compist.Count; i++)
                {
                    compist[i].CompanyCode = UrlEncryptionHelper.Encrypt(compist[i].CompanyCode);
                }
                modelObj.CompaniesList = new SelectList(compist, "CompanyCode", "CompanyName");
                var companyCode = "EZY";
                var branchList = new BranchBO().GetList().Where(x => x.CompanyCode == companyCode).ToList();
                modelObj.BranchList = new SelectList(branchList, "BranchID", "BranchName").ToList();

                return View("Index", modelObj);
            }

        }
    }
}
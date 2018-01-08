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
        [HttpGet]
        public ActionResult Index()
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            model.CompanyCode = "EZY";
            var BranchDetails = new BranchBO().GetList().Where(x => x.BranchID == model.BranchID).ToList();
            string BranchName = BranchDetails.Select(x => x.BranchName).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var lstUsers = new UsersBO().GetList();

            var currentUser = lstUsers.Where(
                ur => ur.UserID.ToLower() == model.UserID.ToLower() &&
                ur.Password.ToLower() == model.Password.ToLower()).FirstOrDefault();

            var userBranch = new UserBranchBO().GetList(model.UserID)
                                    .Where(x => x.BranchID == model.BranchID)
                                    .FirstOrDefault();

            if (currentUser != null && userBranch != null)
            {
                FormsAuthentication.SetAuthCookie(currentUser.UserID, false);

                var _CompanyId = model.CompanyCode;
                var SsnObj = new SessionObject
                {
                    UserID = currentUser.UserID,
                    UserName = currentUser.UserName,
                    Email = currentUser.Email,
                    RoleCode = currentUser.RoleCode,
                    BranchId = model.BranchID,
                    BranchName = BranchName,
                    CompanyId = _CompanyId,
                    CompanyName = new CompanyBO().GetList().Where(x => x.CompanyCode == _CompanyId).FirstOrDefault().CompanyName
                };

                USER_OBJECT = SsnObj;
                USER_SECURABLES = new RoleRightsBO().GetSecurableItemsListByRoleCode(SsnObj.RoleCode);

                if (currentUser.UserID.ToLower() == "cxo@ezy-corp.com")
                    Session["UserID"] = "ADMIN";
                else
                    Session["UserID"] = currentUser.UserID;

                Session["UserName"] = currentUser.UserName;
                Session["BranchId"] = model.BranchID;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [WebSsnFilter]
    public class LayoutController : BaseController
    {
        [ChildActionOnly]
        public PartialViewResult Links()
        {
            var layoutVm = new LayoutVm {
                Country = BRANCH_NAME,
                RoleCode = ROLE_CODE
            };
            return PartialView(layoutVm);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction("Index", "Account");
        }
    }

    public class LayoutVm
    {
        public string Country { get; set; }
        public string RoleCode { get; set; }
    }
}
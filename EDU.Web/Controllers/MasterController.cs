using EZY.EDU.BusinessFactory;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class MasterController : BaseController
    {
        #region EduProduct
        [HttpGet]
        public ViewResult EduProductList()
        {
            var list = new EduProductBO().GetList();
            return View("EduProductList", list.AsEnumerable());
        }

        [HttpGet]
        public PartialViewResult EduProduct(int? Id)
        {
            if (!Id.HasValue)
            {
                ViewBag.Title = "New Product";
                return PartialView(new EduProduct { Id = -1, IsActive = true });
            }
            else
            {
                ViewBag.Title = "Update Product";
                return PartialView(new EduProductBO().GetEduProduct(new EduProduct { Id = Id.Value }));

            }
        }

        [HttpPost]
        public RedirectToRouteResult SaveEduProduct(EduProduct eduProduct)
        {
            eduProduct.ProductDescription = eduProduct.ProductName;

            eduProduct.CreatedBy = USER_ID;
            eduProduct.CreatedOn = UTILITY.SINGAPORETIME;
            eduProduct.ModifiedBy = USER_ID;
            eduProduct.ModifiedOn = UTILITY.SINGAPORETIME;
            if (!IsEduProductExists(eduProduct.ProductName))
            {
                var result = new EduProductBO().SaveEduProduct(eduProduct);
            }

            return RedirectToAction("EduProductList");
        }

        [HttpPost]
        public ActionResult DeleteEduProduct(int? Id)
        {
            var result = new EduProductBO().DeleteEduProduct(new EduProduct { Id = Id.Value });

            var list = new EduProductBO().GetList();
            return View("EduProductList", list.AsEnumerable());
        }


        [HttpGet]
        public bool IsEduProductExists(string productName)
        {
            return new EduProductBO().IsEduProductExists(new EduProduct { ProductName = productName });
        }

        #endregion
    }
}
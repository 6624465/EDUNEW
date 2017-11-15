using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["BranchId"] == null)
            {
                try
                {
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new RedirectResult("~/Account/Index");
                    }
                    else
                    {
                        /*
                        filterContext.Result = new JsonResult
                        {
                            Data = new
                            {
                                // put whatever data you want which will be sent
                                // to the client
                                message = "sorry, but you were logged out"
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                        */

                        filterContext.HttpContext.Response.StatusCode = 401;
                        filterContext.HttpContext.Response.End();

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                //var UserID = (string)System.Web.HttpContext.Current.Session["UserID"];
                //if (UserID != "Admin")
                //{
                //    var Url = filterContext.HttpContext.Request.Path;
                //    var securables = (SessionObject)System.Web.HttpContext.Current.Session["SsnSecurables"];

                //    if (securables.Where(x => x.Link == Url).Count() == 0)
                //    {
                //        if (Url != "/Home/Index" && Url != "/Reports/InvoiceReport" && Url != "/Reports/ProductStockHistory" && Url != "/Operation/SearchPO" && Url != "/Operation/ImageUploads" && Url != "/Operation/UploadFiles"
                //            && Url != "/Operation/Goodsreceiveform1" && Url != "/Operation/Goodsreceiveform2" && Url != "/Operation/Goodsreceiveform3" && Url != "/Operation/inspection" && Url != "/Reports/ViewGF1" && Url != "/Reports/ViewGF2" && Url != "/Reports/ViewCheckSheet" && Url != "/Reports/ViewInspection"
                //            && Url != "/Operation/AddProductToGrid" && Url != "/Reports/OrderInvoiceReport" && Url != "/Reports/QuotationReport" && Url != "/Reports/PurchaseOrderReport" && Url != "/MasterData/Branch")
                //            filterContext.Result = new RedirectResult("~/Home/Index");
                //    }
            }
        }
    }
}
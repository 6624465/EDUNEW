using EDU.Web.Models;
using EDU.Web.ViewModels.CustomerPayments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    public class CustomerPaymentController : Controller
    {
        EducationEntities dbContext = new EducationEntities();
        // GET: CustomerPayment
        public ActionResult Payment(int customerId = -1)
        {
            CustomerPayment customerPayment = dbContext.CustomerPayments.
                    Where(x => x.CustomerId == customerId && x.IsActive == true).FirstOrDefault();

            if (customerPayment == null)
            {
                CustomerPaymentVM customerPaymentVM = new CustomerPaymentVM();
                customerPaymentVM.CustomerId = customerId;

                return View(customerPaymentVM);
            }
            else
            {

                CustomerPaymentVM customerPaymentVM = new CustomerPaymentVM();

                customerPaymentVM.BalanceAmount = customerPayment.BalanceAmount;
                customerPaymentVM.CourseName = customerPayment.CourseName;
                customerPaymentVM.CustomerName = customerPayment.CustomerName;
                customerPaymentVM.DueDate = customerPayment.DueDate;
                customerPaymentVM.InvoiceValue = customerPayment.InvoiceValue;
                customerPaymentVM.OrderId = customerPayment.OrderId;
                customerPaymentVM.OtherRecievables = customerPayment.OtherRecievables;
                customerPaymentVM.PaidAmount = customerPayment.PaidAmount;
                customerPaymentVM.RecieptDate = customerPayment.RecieptDate;
                return View(customerPaymentVM);
            }



        }
        [HttpPost]
        public ActionResult Payment(CustomerPaymentVM customerPaymentVM)
        {
            if (customerPaymentVM.CustomerId == -1)
            {
                CustomerPayment customerPayment = new CustomerPayment();
                customerPayment.BalanceAmount = customerPaymentVM.BalanceAmount;
                customerPayment.CourseName = customerPaymentVM.CourseName;
                customerPayment.CustomerName = customerPaymentVM.CustomerName;
                customerPayment.DueDate = customerPaymentVM.DueDate;
                customerPayment.InvoiceValue = customerPaymentVM.InvoiceValue;
                customerPayment.OrderId = customerPaymentVM.OrderId;
                customerPayment.OtherRecievables = customerPaymentVM.OtherRecievables;
                customerPayment.PaidAmount = customerPaymentVM.PaidAmount;
                customerPayment.RecieptDate = customerPaymentVM.RecieptDate;
                customerPayment.IsActive = true;

                if (customerPaymentVM.ReferenceDocument.ContentLength > 0)
                {
                    customerPayment.ReferenceDocument = customerPaymentVM.ReferenceDocument.FileName;
                    string fileName = customerPaymentVM.ReferenceDocument.FileName;
                    string path = Path.Combine(Server.MapPath("~/FileUploads"), fileName);
                    customerPaymentVM.ReferenceDocument.SaveAs(path);
                }


                dbContext.CustomerPayments.Add(customerPayment);
                dbContext.SaveChanges();
            }
            else
            {
                CustomerPayment customerPayment = dbContext.CustomerPayments.
                    Where(x => x.CustomerId == customerPaymentVM.CustomerId).FirstOrDefault();

                customerPayment.BalanceAmount = customerPaymentVM.BalanceAmount;
                customerPayment.CourseName = customerPaymentVM.CourseName;
                customerPayment.CustomerName = customerPaymentVM.CustomerName;
                customerPayment.DueDate = customerPaymentVM.DueDate;
                customerPayment.InvoiceValue = customerPaymentVM.InvoiceValue;
                customerPayment.OrderId = customerPaymentVM.OrderId;
                customerPayment.OtherRecievables = customerPaymentVM.OtherRecievables;
                customerPayment.PaidAmount = customerPaymentVM.PaidAmount;
                customerPayment.RecieptDate = customerPaymentVM.RecieptDate;
                customerPayment.ReferenceDocument = customerPaymentVM.ReferenceDocument.FileName;

                dbContext.SaveChanges();
            }
            return RedirectToAction("CustomerPaymentList");
        }

        public ActionResult CustomerPaymentList()
        {
            List<CustomerPayment> customerPayment = dbContext.CustomerPayments.Where(x => x.IsActive == true).ToList();
            foreach (CustomerPayment cp in customerPayment)
            {
                cp.ReferenceDocument = "~/FileUploads/" + cp.ReferenceDocument;
            }
            return View(customerPayment);
        }

        [HttpPost]
        public JsonResult DeleteCustomerPayment(int? customerId)
        {
            CustomerPayment customerPayment = dbContext.CustomerPayments.
                      Where(x => x.CustomerId == customerId).FirstOrDefault();

            if (customerPayment != null)
            {
                customerPayment.IsActive = false;
                dbContext.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}
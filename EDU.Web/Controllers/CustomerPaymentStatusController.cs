using EDU.Web.Models;
using EDU.Web.ViewModels.CustomerPaymentStatusModel;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class CustomerPaymentStatusController : BaseController
    {
        // GET: CustomerPaymentStatus
        EducationEntities dbContext = new EducationEntities();
        public ActionResult CustomerPaymentStatusList(string trainingConfirmationID)
        {
            CustomerPaymentStatusVM result = new CustomerPaymentStatusVM();
            result = getListData(trainingConfirmationID);

            return View(result);

        }
        private CustomerPaymentStatusVM getListData(string trainingConfirmationID)
        {
            CustomerPaymentStatusVM result = new CustomerPaymentStatusVM();
            try
            {
                List<TrainingConfirmation> tc = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();
                List<Registration> regList = GetList(trainingConfirmationID);
                TrainingConfirmation tcdtl = tc.Where(x => x.TrainingConfirmationID == trainingConfirmationID).FirstOrDefault();


                TrainingConfirmDtl tcd = new TrainingConfirmDtl();

                if (tcdtl != null)
                {
                    string productName = new EduProductBO().GetList().Where(x => x.Id == tcdtl.Product).FirstOrDefault() != null ? new EduProductBO().GetList().Where(x => x.Id == tcdtl.Product).FirstOrDefault().ProductName : "";
                    string courseName = new CourseBO().GetList().Where(x => x.Id == tcdtl.Course).FirstOrDefault() != null ? new CourseBO().GetList().Where(x => x.Id == tcdtl.Course).FirstOrDefault().CourseName : "";
                    tcd = new TrainingConfirmDtl()
                    {
                        Id = tcdtl.Id,
                        TrainingConfirmationID = tcdtl.TrainingConfirmationID,
                        Product = tcdtl.Product,
                        Course = tcdtl.Course,
                        ProductName = productName,
                        CourseName = courseName,
                        TrianerName = dbContext.TrainerInformations.Where(t => t.TrianerId == tcdtl.TrianerId).FirstOrDefault() == null ? "" : dbContext.TrainerInformations.Where(t => t.TrianerId == tcdtl.TrianerId).FirstOrDefault().TrainerName
                    };
                }

                List<CustomerPaymentVM> customerPaymentVM = dbContext.CustomerPayments
                    .Where(x => x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID)
                    .Select(x => new CustomerPaymentVM
                    {
                        CustomerPaymentId = x.CustomerPaymentId,
                        RegistrationId = x.RegistrationId,
                        TrainingConfirmationID = x.TrainingConfirmationID,
                        InvoiceAmount = x.InvoiceAmount,
                        PaidAmount = x.PaidAmount,
                        BalanceAmount = x.BalanceAmount,
                        OtherReceivablesAmount = x.OtherReceivablesAmount,
                        TotalAmount = x.TotalAmount,
                        DueDate = x.DueDate,
                        ReceiptDate = x.ReceiptDate,
                        ReferenceDoc = x.ReferenceDoc,
                        IsActive = true,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedOn = x.ModifiedOn,
                        CustomerName = dbContext.Registrations.Where(r => r.RegistrationId == x.RegistrationId).FirstOrDefault().StudentName
                    })
                    .ToList();


                foreach (var item in regList)
                {
                    if (dbContext.CustomerPayments.Where(x => x.IsActive == true && x.RegistrationId == item.RegistrationId).Count() == 0)
                    {
                        customerPaymentVM.Add(new CustomerPaymentVM()
                        {
                            CustomerPaymentId = -1,
                            RegistrationId = item.RegistrationId,
                            TrainingConfirmationID = item.TrainingConfirmationID,
                            InvoiceAmount = item.TotalAmount,
                            PaidAmount = (item.Payment1 == null ? 0 : item.Payment1) + (item.Payment2 == null ? 0 : item.Payment2) + (item.Payment3 == null ? 0 : item.Payment3),
                            BalanceAmount = item.BalanceAmount,
                            OtherReceivablesAmount = 0,
                            TotalAmount = 0,
                            DueDate = null,
                            ReceiptDate = null,
                            ReferenceDoc = null,
                            IsActive = true,
                            CreatedBy = null,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = null,
                            ModifiedOn = null,
                            CustomerName = item.StudentName
                        });
                    }
                }

                result.customerPayment = customerPaymentVM;

                result.trainingconf = tc;
                result.trainingconfDetail = tcd;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        [HttpPost]
        public PartialViewResult CustomerPaymentStatusDetail(CustomerPaymentVM customerPayment)
        {
            if (customerPayment.CustomerPaymentId == -1)
            {
                ViewBag.Title = "New Customer Payment Status";
                return PartialView(new CustomerPaymentVM { CustomerPaymentId = -1, IsActive = true });
            }
            else
            {
                ViewBag.Title = "Update Customer Payment Status";
                return PartialView(customerPayment);

            }
        }
        private List<Registration> GetList(string trainingConfirmationID)
        {
            List<Registration> List = dbContext.Registrations
                .Where(x => x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID && x.BalanceAmount > 0)
                .ToList();
            return List;
        }
        [HttpPost]
        public ActionResult SaveCustomerPaymentStatus(CustomerPaymentVM customerpaymentvm)
        {
            try
            {
                CustomerPayment customerpayment = new CustomerPayment();
                if (customerpaymentvm.CustomerPaymentId == -1)
                {
                    customerpayment.CustomerPaymentId = customerpaymentvm.CustomerPaymentId;
                    customerpayment.RegistrationId = customerpaymentvm.RegistrationId;
                    customerpayment.TrainingConfirmationID = customerpaymentvm.TrainingConfirmationID;
                    customerpayment.InvoiceAmount = customerpaymentvm.InvoiceAmount;
                    customerpayment.PaidAmount = customerpaymentvm.PaidAmount;
                    customerpayment.BalanceAmount = customerpaymentvm.BalanceAmount;
                    customerpayment.OtherReceivablesAmount = customerpaymentvm.OtherReceivablesAmount;
                    customerpayment.TotalAmount = customerpaymentvm.TotalAmount;
                    customerpayment.DueDate = customerpaymentvm.DueDate;

                    customerpayment.ReceiptDate = customerpaymentvm.ReceiptDate;
                    customerpayment.IsActive = true;
                    customerpayment.CreatedBy = USER_ID;
                    customerpayment.CreatedOn = UTILITY.SINGAPORETIME;
                    if (customerpaymentvm.FileName != null && customerpaymentvm.FileName.ContentLength > 0)
                    {
                        customerpayment.ReferenceDoc = customerpaymentvm.FileName.FileName;
                    }
                    dbContext.CustomerPayments.Add(customerpayment);

                    dbContext.SaveChanges();

                    if (customerpaymentvm.FileName != null && customerpaymentvm.FileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/" + "CPS_" + customerpayment.CustomerPaymentId + "/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        customerpaymentvm.FileName.SaveAs(path + customerpaymentvm.FileName.FileName);
                    }
                }
                else {
                    customerpayment = dbContext.CustomerPayments.Where(x => x.CustomerPaymentId == customerpaymentvm.CustomerPaymentId).FirstOrDefault();

                    customerpayment.RegistrationId = customerpaymentvm.RegistrationId;
                    customerpayment.TrainingConfirmationID = customerpaymentvm.TrainingConfirmationID;
                    customerpayment.InvoiceAmount = customerpaymentvm.InvoiceAmount;
                    customerpayment.PaidAmount = customerpaymentvm.PaidAmount;
                    customerpayment.BalanceAmount = customerpaymentvm.BalanceAmount;
                    customerpayment.OtherReceivablesAmount = customerpaymentvm.OtherReceivablesAmount;
                    customerpayment.TotalAmount = customerpaymentvm.TotalAmount;
                    customerpayment.DueDate = customerpaymentvm.DueDate;

                    customerpayment.ReceiptDate = customerpaymentvm.ReceiptDate;
                    customerpayment.IsActive = true;
                    customerpayment.ModifiedBy = USER_ID;
                    customerpayment.ModifiedOn = UTILITY.SINGAPORETIME;
                    if (customerpaymentvm.FileName != null && customerpaymentvm.FileName.ContentLength > 0)
                    {
                        customerpayment.ReferenceDoc = customerpaymentvm.FileName.FileName;
                    }
                    dbContext.SaveChanges();

                    if (customerpaymentvm.FileName != null && customerpaymentvm.FileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/" + "CPS_" + customerpayment.CustomerPaymentId + "/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        customerpaymentvm.FileName.SaveAs(path + customerpaymentvm.FileName.FileName);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("CustomerPaymentStatusList", new { trainingConfirmationID = customerpaymentvm.TrainingConfirmationID });
        }

        [HttpPost]
        public JsonResult DeleteCustomerPaymentStatus(int CustomerPaymentId)
        {
            CustomerPayment deletecustomer = dbContext.CustomerPayments.Where(x => x.CustomerPaymentId == CustomerPaymentId).FirstOrDefault();
            if(deletecustomer != null)
            {
                deletecustomer.IsActive = false;
                dbContext.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}
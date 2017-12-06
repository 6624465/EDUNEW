using EDU.Web.Models;
using EDU.Web.ViewModels.CustomerPaymentStatusModel;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
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
                List<Registration> List = GetList(trainingConfirmationID);
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

                result.customerPayment = List.Select(x => new CustomerPaymentVM
                {
                    CustomerPaymentId = -1,
                    RegistrationId = x.RegistrationId,
                    InvoiceAmount = x.TotalAmount,
                    PaidAmount = (x.Payment1==null?0:x.Payment1) + (x.Payment2 == null ? 0 : x.Payment2) + (x.Payment3 == null ? 0 : x.Payment3),
                    BalanceAmount = x.BalanceAmount,
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
                    CustomerName = x.StudentName
                }).ToList();

                result.trainingconf = tc;
                result.trainingconfDetail = tcd;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        private List<Registration> GetList(string trainingConfirmationID)
        {
            List<Registration> List = dbContext.Registrations
                .Where(x => x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID && x.BalanceAmount > 0)
                .ToList();
            return List;
        }
    }
}
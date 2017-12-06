using EDU.Web.Models;
using EDU.Web.ViewModels.Registration;
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
            RegistrationVM result = new RegistrationVM();
            result = getListData(trainingConfirmationID);

            return View(result);
           
        }
        private RegistrationVM getListData(string trainingConfirmationID)
        {
            RegistrationVM result = new RegistrationVM();
            try
            {
                List<TrainingConfirmation> tc = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();
                List<Registration> List = GetList(trainingConfirmationID);
                TrainingConfirmation tcdtl = tc.Where(x => x.TrainingConfirmationID == trainingConfirmationID).FirstOrDefault();


                TrainingConfirmDtl tcd = new TrainingConfirmDtl();
                result.trainingconf = tc;
               
                //if (tcdtl != null)
                //{
                //    string productName = new EduProductBO().GetList().Where(x => x.Id == tcdtl.Product).FirstOrDefault() != null ? new EduProductBO().GetList().Where(x => x.Id == tcdtl.Product).FirstOrDefault().ProductName : "";
                //    string courseName = new CourseBO().GetList().Where(x => x.Id == tcdtl.Course).FirstOrDefault() != null ? new CourseBO().GetList().Where(x => x.Id == tcdtl.Course).FirstOrDefault().CourseName : "";
                //    tcd = new TrainingConfirmDtl()
                //    {
                //        Id = tcdtl.Id,
                //        TrainingConfirmationID = tcdtl.TrainingConfirmationID,
                //        Product = tcdtl.Product,
                //        Course = tcdtl.Course,
                //        TotalNoOfDays = tcdtl.TotalNoOfDays,
                //        NoOfStudents = tcdtl.NoOfStudents,
                //        Private = tcdtl.Private,
                //        Public = tcdtl.Public,
                //        StartDate = tcdtl.StartDate,
                //        EndDate = tcdtl.EndDate,
                //        TrianerId = tcdtl.TrianerId,
                //        IsActive = tcdtl.IsActive,
                //        CreatedBy = tcdtl.CreatedBy,
                //        CreatedOn = tcdtl.CreatedOn,
                //        ModifiedBy = tcdtl.ModifiedBy,
                //        ModifiedOn = tcdtl.ModifiedOn,
                //        ProductName = productName,
                //        CourseName = courseName,
                //        TrianerName = dbContext.TrainerInformations.Where(t => t.TrianerId == tcdtl.TrianerId).FirstOrDefault().TrainerName
                //    };
                //}

                //    result.registration = List;
                //    result.trainingconf = tc;
                //    result.trainingconfDetail = tcd;

                //    long amounttotal = 0;
                //    long whttotal = 0;
                //    long vattotal = 0;
                //    long sumamounttotal = 0;
                //    long payment1total = 0;
                //    long payment2total = 0;
                //    long payment3total = 0;
                //    long baltotal = 0;
                //    long othertotal = 0;

                //    foreach (var item in List)
                //    {
                //        amounttotal += Convert.ToInt64(item.Amount == null ? 0 : item.Amount.Value);
                //        whttotal += Convert.ToInt64(item.WHTAmount == null ? 0 : item.WHTAmount.Value);
                //        vattotal += Convert.ToInt64(item.VATAmount == null ? 0 : item.VATAmount.Value);
                //        sumamounttotal += Convert.ToInt64(item.TotalAmount == null ? 0 : item.TotalAmount.Value);
                //        payment1total += Convert.ToInt64(item.Payment1 == null ? 0 : item.Payment1.Value);
                //        payment2total += Convert.ToInt64(item.Payment2 == null ? 0 : item.Payment2.Value);
                //        payment3total += Convert.ToInt64(item.Payment3 == null ? 0 : item.Payment3.Value);
                //        othertotal += Convert.ToInt64(item.OtherDeductionsAmount == null ? 0 : item.OtherDeductionsAmount.Value);
                //        baltotal += Convert.ToInt64(item.BalanceAmount == null ? 0 : item.BalanceAmount.Value);
                //    }

                //    List<string> summary = new List<string>();
                //    summary.Add(amounttotal.ToString());
                //    summary.Add(whttotal.ToString());
                //    summary.Add(vattotal.ToString());
                //    summary.Add(sumamounttotal.ToString());
                //    summary.Add(payment1total.ToString());
                //    summary.Add(payment2total.ToString());
                //    summary.Add(payment3total.ToString());
                //    summary.Add(othertotal.ToString());
                //    summary.Add(baltotal.ToString());
                //    ViewData["Summary"] = summary;
                //}
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        private List<Registration> GetList(string trainingConfirmationID)
        {
            List<Registration> registrationList = dbContext.Registrations.Where(x => x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID).ToList();
            return registrationList;
        }
    }
}
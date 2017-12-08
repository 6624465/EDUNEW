using EDU.Web.Models;
using EDU.Web.ViewModels.Registration;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class RegistrationController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();

        [HttpGet]
        public ActionResult RegistrationList(string trainingConfirmationID)
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
                        TotalNoOfDays = tcdtl.TotalNoOfDays,
                        NoOfStudents = tcdtl.NoOfStudents,
                        Private = tcdtl.Private,
                        Public = tcdtl.Public,
                        StartDate = tcdtl.StartDate,
                        EndDate = tcdtl.EndDate,
                        TrianerId = tcdtl.TrianerId,
                        IsActive = tcdtl.IsActive,
                        CreatedBy = tcdtl.CreatedBy,
                        CreatedOn = tcdtl.CreatedOn,
                        ModifiedBy = tcdtl.ModifiedBy,
                        ModifiedOn = tcdtl.ModifiedOn,
                        ProductName = productName,
                        CourseName = courseName,
                        TrianerName = dbContext.TrainerInformations.Where(t => t.TrianerId == tcdtl.TrianerId).FirstOrDefault().TrainerName
                    };
                }

                result.registration = List;
                result.trainingconf = tc;
                result.trainingconfDetail = tcd;

                long amounttotal = 0;
                long whttotal = 0;
                long vattotal = 0;
                long sumamounttotal = 0;
                long payment1total = 0;
                long payment2total = 0;
                long payment3total = 0;
                long baltotal = 0;
                long othertotal = 0;

                foreach (var item in List)
                {
                    amounttotal += Convert.ToInt64(item.Amount == null ? 0 : item.Amount.Value);
                    whttotal += Convert.ToInt64(item.WHTAmount == null ? 0 : item.WHTAmount.Value);
                    vattotal += Convert.ToInt64(item.VATAmount == null ? 0 : item.VATAmount.Value);
                    sumamounttotal += Convert.ToInt64(item.TotalAmount == null ? 0 : item.TotalAmount.Value);
                    payment1total += Convert.ToInt64(item.Payment1 == null ? 0 : item.Payment1.Value);
                    payment2total += Convert.ToInt64(item.Payment2 == null ? 0 : item.Payment2.Value);
                    payment3total += Convert.ToInt64(item.Payment3 == null ? 0 : item.Payment3.Value);
                    othertotal += Convert.ToInt64(item.OtherDeductionsAmount == null ? 0 : item.OtherDeductionsAmount.Value);
                    baltotal += Convert.ToInt64(item.BalanceAmount == null ? 0 : item.BalanceAmount.Value);
                }

                List<decimal?> summary = new List<decimal?>();
                summary.Add(amounttotal);
                summary.Add(whttotal);
                summary.Add(vattotal);
                summary.Add(sumamounttotal);
                summary.Add(payment1total);
                summary.Add(payment2total);
                summary.Add(payment3total);
                summary.Add(othertotal);
                summary.Add(baltotal);
                ViewData["Summary"] = summary;
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



        public ActionResult Registration(int Id, string trainingConfirmationID)
        {
            Registration registration = dbContext.Registrations.Where(x => x.RegistrationId == Id && x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID).FirstOrDefault();
            if (registration == null)
            {
                registration = new Registration();
                registration.RegistrationId = Id;
                registration.TrainingConfirmationID = trainingConfirmationID;
            }
            return View(registration);
        }

        [HttpPost]
        public ActionResult Registration(Registration registration)
        {
            if (registration.RegistrationId == -1)
            {
                registration.IsActive = true;
                registration.CreatedBy = USER_ID;
                registration.CreatedOn = UTILITY.SINGAPORETIME;

                dbContext.Registrations.Add(registration);
                dbContext.SaveChanges();
            }
            else
            {
                Registration regObj = dbContext.Registrations.Where(x => x.RegistrationId == registration.RegistrationId && x.TrainingConfirmationID == registration.TrainingConfirmationID).FirstOrDefault();
                if (regObj != null)
                {
                    regObj.StudentName = registration.StudentName;
                    regObj.Email = registration.Email;
                    regObj.Contact = registration.Contact;
                    regObj.CompanyName = registration.CompanyName;
                    regObj.Amount = registration.Amount;
                    regObj.WHTPercent = registration.WHTPercent;
                    regObj.VATPercent = registration.VATPercent;
                    regObj.WHTAmount = registration.WHTAmount;
                    regObj.VATAmount = registration.VATAmount;
                    regObj.OtherDeductionsAmount = registration.OtherDeductionsAmount;
                    regObj.TotalAmount = registration.TotalAmount;
                    regObj.Payment1 = registration.Payment1;
                    regObj.Payment2 = registration.Payment2;
                    regObj.Payment3 = registration.Payment3;
                    regObj.Payment1Date = registration.Payment1Date;
                    regObj.Payment2Date = registration.Payment2Date;
                    regObj.Payment3Date = registration.Payment3Date;
                    regObj.Payment1Type = registration.Payment1Type;
                    regObj.Payment2Type = registration.Payment2Type;
                    regObj.Payment3Type = registration.Payment3Type;

                    if (registration.Payment1Type == 2 || registration.Payment1Type == 3)
                        regObj.ChequeNo1 = registration.ChequeNo1;
                    else
                        regObj.ChequeNo1 = "";
                    if (registration.Payment2Type == 2 || registration.Payment2Type == 3)
                        regObj.ChequeNo2 = registration.ChequeNo2;
                    else
                        regObj.ChequeNo2 = "";

                    if (registration.Payment3Type == 2 || registration.Payment3Type == 3)
                        regObj.ChequeNo3 = registration.ChequeNo3;
                    else
                        regObj.ChequeNo3 = "";

                    regObj.BalanceAmount = registration.BalanceAmount;
                    regObj.TrainingConfirmationID = registration.TrainingConfirmationID;

                    regObj.IsActive = true;

                    regObj.ModifiedBy = USER_ID;
                    regObj.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(regObj).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            return RedirectToAction("RegistrationList", new { trainingConfirmationID = registration.TrainingConfirmationID });
        }

        [HttpPost]
        public JsonResult DeleteRegistration(int Id)
        {
            Registration regObj = dbContext.Registrations.Where(x => x.RegistrationId == Id).FirstOrDefault();
            if (regObj != null)
            {
                regObj.IsActive = false;
                dbContext.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
using EDU.Web.Models;
using EDU.Web.ViewModels.Registration;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class RegistrationController : Controller
    {
        EducationEntities dbContext = new EducationEntities();

        [HttpGet]
        public ActionResult RegistrationList(string trainingConfirmationID)
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
                    string productName = new EduProductBO().GetList().Where(x => x.Id == tcdtl.Product).FirstOrDefault().ProductName;
                    string courseName = new CourseBO().GetList().Where(x => x.Id == tcdtl.Course).FirstOrDefault().CourseName;
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(result);
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
                dbContext.Registrations.Add(registration);
                dbContext.SaveChanges();
            }
            else
            {
                Registration regObj = dbContext.Registrations.Where(x => x.RegistrationId == registration.RegistrationId).FirstOrDefault();
                if (regObj != null)
                {
                    //regObj.OracleDataBase = registration.OracleDataBase;
                    //regObj.Payment1 = registration.Payment1;
                    //regObj.Payment2 = registration.Payment2;
                    //regObj.Product = registration.Product;
                    //regObj.StartDate = registration.StartDate;
                    //regObj.TrainerName = registration.TrainerName;
                    //regObj.Email = registration.Email;
                    //regObj.Amount = registration.Amount;
                    //regObj.Balance = registration.Balance;
                    //regObj.CompanyName = registration.CompanyName;
                    //regObj.Contact = registration.Contact;
                    //regObj.CourseName = registration.CourseName;
                    //regObj.VAT = registration.VAT;
                    //regObj.WHT = registration.WHT;
                    //regObj. = registration.CompanyName;
                }
                dbContext.SaveChanges();
            }
            return RedirectToAction("RegistrationList");
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
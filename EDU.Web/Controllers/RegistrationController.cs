using EDU.Web.Models;
using EDU.Web.ViewModels.Registration;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
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
        public ActionResult GetRegistrationList()
        {
            //List<Registration> registrationList = dbContext.Registrations.Where(x=>x.IsActive==true).ToList();

            var results = dbContext.Registrations
                            .GroupJoin(dbContext.TrainingConfirmations,
                                            a => new { a.TrainingConfirmationID },
                                            b => new { b.TrainingConfirmationID },
                                        (a, b) => new { A = a, B = b.DefaultIfEmpty() })
                            .Where(x => x.A.IsActive == true)
                            .Select(x => new RegistrationVM()
                            {
                                RegistrationId = x.A.RegistrationId,
                                StudentName = x.A.StudentName,
                                Email = x.A.Email,
                                Contact = x.A.Contact,
                                CompanyName = x.A.CompanyName,
                                Amount = x.A.Amount,
                                WHTPercent = x.A.WHTPercent,
                                VATPercent = x.A.VATPercent,
                                WHTAmount = x.A.WHTAmount,
                                VATAmount = x.A.VATAmount,
                                OtherDeductionsAmount = x.A.OtherDeductionsAmount,
                                TotalAmount = x.A.TotalAmount,
                                Payment1 = x.A.Payment1,
                                Payment2 = x.A.Payment2,
                                Payment3 = x.A.Payment3,
                                Payment1Date = x.A.Payment1Date,
                                Payment2Date = x.A.Payment2Date,
                                Payment3Date = x.A.Payment3Date,
                                Payment1Type = x.A.Payment1Type,
                                Payment2Type = x.A.Payment2Type,
                                Payment3Type = x.A.Payment3Type,
                                CheckNo = x.A.CheckNo,
                                BalanceAmount = x.A.BalanceAmount,
                                TrainingConfirmationID = x.A.TrainingConfirmationID,
                                CreatedBy = x.A.CreatedBy,
                                CreatedOn = x.A.CreatedOn,
                                ModifiedBy = x.A.ModifiedBy,
                                ModifiedOn = x.A.ModifiedOn,
                                IsActive = x.A.IsActive,
                                ProductName = new EduProductBO().GetList().Where(p=>p.Id== x.B.FirstOrDefault().Product).FirstOrDefault().ProductName,
                                CourseName = new CourseBO().GetList().Where(c => c.Id == x.B.FirstOrDefault().Course).FirstOrDefault().CountryName,
                                StartDate = x.B.FirstOrDefault().StartDate.Value,
                                EndDate = x.B.FirstOrDefault().EndDate.Value,
                                TrainerName = dbContext.TrainerInformations.Where(t=>t.TrianerId==x.B.FirstOrDefault().TrianerId).FirstOrDefault().TrainerName
                            }).AsQueryable();

            return View(results);
        }

        public ActionResult Registration(int Id=-1)
        {
            Registration registration = dbContext.Registrations.Where(x => x.RegistrationId == Id && x.IsActive == true).FirstOrDefault();
            if (registration == null)
            {
                registration = new Registration();
                registration.RegistrationId = Id;
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
            return RedirectToAction("GetRegistrationList");
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
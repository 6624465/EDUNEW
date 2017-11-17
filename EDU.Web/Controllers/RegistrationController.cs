using EDU.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    public class RegistrationController : Controller
    {
        EducationEntities dbContext = new EducationEntities();

        [HttpGet]
        public ActionResult GetRegistrationList()
        {
            List<Registration> registrationList = dbContext.Registrations.Where(x=>x.IsActive==true).ToList();
            return View(registrationList);
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
                    regObj.OracleDataBase = registration.OracleDataBase;
                    regObj.Payment1 = registration.Payment1;
                    regObj.Payment2 = registration.Payment2;
                    regObj.Product = registration.Product;
                    regObj.StartDate = registration.StartDate;
                    regObj.TrainerName = registration.TrainerName;
                    regObj.Email = registration.Email;
                    regObj.Amount = registration.Amount;
                    regObj.Balance = registration.Balance;
                    regObj.CompanyName = registration.CompanyName;
                    regObj.Contact = registration.Contact;
                    regObj.CourseName = registration.CourseName;
                    regObj.VAT = registration.VAT;
                    regObj.WHT = registration.WHT;
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
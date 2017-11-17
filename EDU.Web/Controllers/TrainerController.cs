using EDU.Web.Models;
using EDU.Web.ViewModels.Master;
using EDU.Web.ViewModels.Trainer;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    public class TrainerController : Controller
    {
        EducationEntities dbContext = new EducationEntities();

        // GET: Trainer
        public ActionResult Index(int Id = -1)
        {
            TrainerInformation TrainerInfo = dbContext.TrainerInformations.
                Where(x => x.TrianerId == Id && x.IsActive == true).FirstOrDefault();

            if (TrainerInfo == null)
            {
                var trainerVM = new TrainerVM()
                {
                    countryList = new CountryBO().GetList().AsEnumerable()
                };
                trainerVM.TrainerInformation = new TrainerInformationVM()
                {
                    TrianerId = Id
                };
                return View(trainerVM);
            }
            else
            {
                var trainerVM = new TrainerVM()
                {

                    countryList = new CountryBO().GetList().AsEnumerable()
                };
                trainerVM.TrainerInformation = new TrainerInformationVM()
                {


                    TrainerName = TrainerInfo.TrainerName,
                    Address = TrainerInfo.Address,
                    Contact = TrainerInfo.Contact,
                    Country = TrainerInfo.Country,
                    TrianerId = TrainerInfo.TrianerId,
                    // Profile = "~/FileUploads/" + TrainerInfo.Profile,
                    Remarks = TrainerInfo.Remarks,
                    Technology = TrainerInfo.Technology,
                    TrainerRate = TrainerInfo.TrainerRate,
                    VendorName = TrainerInfo.VendorName,
                };

                return View(trainerVM);
            }
        }

        public ActionResult SaveTrainer(TrainerVM TrainerInfo)
        {
            if (TrainerInfo.TrainerInformation.TrianerId == -1)
            {
                TrainerInformation ti = new TrainerInformation();

                ti.TrainerName = TrainerInfo.TrainerInformation.TrainerName;
                ti.Address = TrainerInfo.TrainerInformation.Address;
                ti.Contact = TrainerInfo.TrainerInformation.Contact;
                ti.Country = TrainerInfo.TrainerInformation.Country;
                if (TrainerInfo.TrainerInformation.Profile.ContentLength > 0)
                {
                    ti.Profile = TrainerInfo.TrainerInformation.Profile.FileName;
                    string fileName = Path.GetFileName(TrainerInfo.TrainerInformation.Profile.FileName);
                    string path = Path.Combine(Server.MapPath("~/FileUploads"), ti.Profile);
                    TrainerInfo.TrainerInformation.Profile.SaveAs(path);

                }
                ti.Remarks = TrainerInfo.TrainerInformation.Remarks;
                ti.Technology = TrainerInfo.TrainerInformation.Technology;
                ti.TrainerRate = TrainerInfo.TrainerInformation.TrainerRate;
                ti.VendorName = TrainerInfo.TrainerInformation.VendorName;
                ti.IsActive = true;
                dbContext.TrainerInformations.Add(ti);
                dbContext.SaveChanges();
            }

            else
            {
                TrainerInformation trainerInfoDetail = dbContext.TrainerInformations.
                    Where(x => x.TrianerId == TrainerInfo.TrainerInformation.TrianerId).FirstOrDefault();
                if (trainerInfoDetail != null)
                {
                    trainerInfoDetail.TrainerName = TrainerInfo.TrainerInformation.TrainerName;
                    trainerInfoDetail.Address = TrainerInfo.TrainerInformation.Address;
                    trainerInfoDetail.Contact = TrainerInfo.TrainerInformation.Contact;
                    trainerInfoDetail.Country = TrainerInfo.TrainerInformation.Country;
                    trainerInfoDetail.Profile = TrainerInfo.TrainerInformation.Profile.FileName;
                    trainerInfoDetail.Remarks = TrainerInfo.TrainerInformation.Remarks;
                    trainerInfoDetail.Technology = TrainerInfo.TrainerInformation.Technology;
                    trainerInfoDetail.TrainerRate = TrainerInfo.TrainerInformation.TrainerRate;
                    trainerInfoDetail.VendorName = TrainerInfo.TrainerInformation.VendorName;
                }
                dbContext.SaveChanges();
            }
            return RedirectToAction("TrainersList");
        }

        [HttpGet]
        public ActionResult TrainersList()
        {
            List<TrainerInformation> trainerList = dbContext.TrainerInformations.Where(x => x.IsActive == true).ToList();
            foreach (TrainerInformation ti in trainerList)
            {
                ti.Profile = "~/FileUploads/"+ti.Profile;
                var countryList = new CountryBO().GetList().AsEnumerable();
                ti.Country = countryList.Where(x => x.CountryCode == ti.Country).FirstOrDefault().CountryName;
            }
            return View(trainerList);
        }


        [HttpPost]
        public JsonResult DeleteTrainer(int Id)
        {
            TrainerInformation trainerInfoDetail = dbContext.TrainerInformations.
                   Where(x => x.TrianerId == Id).FirstOrDefault();
            if (trainerInfoDetail != null)
            {
                trainerInfoDetail.IsActive = false;
                dbContext.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
using EDU.Web.Models;
using EDU.Web.ViewModels.Master;
//using EDU.Web.ViewModels.Trainer;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    public class TrainerController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();

        // GET: Trainer
        public ActionResult Trainer(int Id = -1)
        {
            TrainerInformation TrainerInfo = dbContext.TrainerInformations.
                Where(x => x.TrianerId == Id && x.IsActive == true).FirstOrDefault();

            ViewData["CountryData"] = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();
            if (TrainerInfo == null)
            {
                TrainerInfo = new TrainerInformation();
                TrainerInfo.TrianerId = -1;

                return View(TrainerInfo);
            }
            else
            {
                return View(TrainerInfo);
            }
        }

        [HttpPost]
        public ActionResult SaveTrainer(TrainerInformation TrainerInfo)
        {
            try
            {

                if (TrainerInfo.TrianerId == -1)
                {
                    //TrainerInfo.CreatedBy = USER_ID;
                    //TrainerInfo.CreatedOn = UTILITY.SINGAPORETIME;
                    TrainerInfo.IsActive = true;
                    TrainerInfo.CountryName = "";

                    dbContext.TrainerInformations.Add(TrainerInfo);
                }

                else
                {
                    TrainerInformation trainerInfoDetail = dbContext.TrainerInformations.
                        Where(x => x.TrianerId == TrainerInfo.TrianerId).FirstOrDefault();

                    trainerInfoDetail.Technology = TrainerInfo.Technology;
                    trainerInfoDetail.Country = TrainerInfo.Country;
                    trainerInfoDetail.CountryName = TrainerInfo.CountryName ?? "";
                    trainerInfoDetail.Profile = TrainerInfo.Profile;
                    trainerInfoDetail.TrainerRate = TrainerInfo.TrainerRate;
                    trainerInfoDetail.VendorName = TrainerInfo.VendorName;
                    trainerInfoDetail.Address = TrainerInfo.Address;
                    trainerInfoDetail.Contact = TrainerInfo.Contact;
                    trainerInfoDetail.Remarks = TrainerInfo.Remarks;
                    trainerInfoDetail.TrainerName = TrainerInfo.TrainerName;

                    trainerInfoDetail.IsActive = true;

                    //trainerInfoDetail.ModifiedBy = USER_ID;
                    //trainerInfoDetail.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(trainerInfoDetail).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("TrainersList");
        }

        [HttpGet]
        public ActionResult TrainersList()
        {
            List<TrainerInformation> trainerList = dbContext.TrainerInformations.Where(x => x.IsActive == true).ToList();
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
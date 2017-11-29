using EDU.Web.Models;
using EDU.Web.ViewModels.Master;
using EDU.Web.ViewModels.Trainer;
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
    [SessionFilter]
    public class TrainerController : BaseController
    {
        EducationEntities dbContext = new EducationEntities();


        #region TrainerInformation
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
                    TrainerInfo.CreatedBy = USER_ID;
                    TrainerInfo.CreatedOn = UTILITY.SINGAPORETIME;
                    TrainerInfo.IsActive = true;

                    if (TrainerInfo.FileName != null && TrainerInfo.FileName.ContentLength > 0)
                    {
                        TrainerInfo.Profile = TrainerInfo.FileName.FileName;
                    }
                    else
                        TrainerInfo.Profile = null;

                    dbContext.TrainerInformations.Add(TrainerInfo);
                    dbContext.SaveChanges();

                    if (TrainerInfo.FileName != null && TrainerInfo.FileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/" + TrainerInfo.TrianerId + "/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        TrainerInfo.FileName.SaveAs(path + TrainerInfo.FileName.FileName);
                    }
                }
                else
                {
                    TrainerInformation trainerInfoDetail = dbContext.TrainerInformations.
                        Where(x => x.TrianerId == TrainerInfo.TrianerId).FirstOrDefault();

                    trainerInfoDetail.Technology = TrainerInfo.Technology;
                    trainerInfoDetail.Country = TrainerInfo.Country;
                    trainerInfoDetail.CountryName = TrainerInfo.CountryName ?? "";
                    if (TrainerInfo.FileName != null && TrainerInfo.FileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/" + TrainerInfo.TrianerId + "/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        TrainerInfo.FileName.SaveAs(path + TrainerInfo.FileName.FileName);
                        TrainerInfo.Profile = TrainerInfo.FileName.FileName;
                    }
                    else
                        TrainerInfo.Profile = null;
                    trainerInfoDetail.Profile = TrainerInfo.Profile;
                    trainerInfoDetail.TrainerRate = TrainerInfo.TrainerRate;
                    trainerInfoDetail.VendorName = TrainerInfo.VendorName;
                    trainerInfoDetail.Address = TrainerInfo.Address;
                    trainerInfoDetail.Contact = TrainerInfo.Contact;
                    trainerInfoDetail.Remarks = TrainerInfo.Remarks;
                    trainerInfoDetail.TrainerName = TrainerInfo.TrainerName;

                    trainerInfoDetail.IsActive = true;

                    trainerInfoDetail.ModifiedBy = USER_ID;
                    trainerInfoDetail.ModifiedOn = UTILITY.SINGAPORETIME;

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
        #endregion


        #region TrainerConfirmation
        // GET: Trainer
        public PartialViewResult TrainerConfirmation(int Id = -1)
        {
            TrainingConfirmation TrainingConfInfo = dbContext.TrainingConfirmations.
                Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();

            ViewData["ProductData"] = new EduProductBO().GetList().Where(x => x.IsActive == true).ToList();
            ViewData["CourseData"] = new CourseBO().GetList().Where(x => x.IsActive == true).ToList();
            ViewData["TrainerData"] = dbContext.TrainerInformations.Where(x => x.IsActive == true).ToList();

            if (TrainingConfInfo == null)
            {
                TrainingConfInfo = new TrainingConfirmation();
                TrainingConfInfo.Id = -1;
                TrainingConfInfo.Public = true;
                return PartialView(TrainingConfInfo);
            }
            else
            {
                return PartialView(TrainingConfInfo);
            }
        }

        [HttpPost]
        public ActionResult SaveTrainingConfirmation(TrainingConfirmation TrainerConfInfo)
        {
            try
            {

                if (TrainerConfInfo.Id == -1)
                {
                    TrainerConfInfo.CreatedBy = USER_ID;
                    TrainerConfInfo.CreatedOn = UTILITY.SINGAPORETIME;
                    TrainerConfInfo.IsActive = true;

                    dbContext.TrainingConfirmations.Add(TrainerConfInfo);
                }

                else
                {
                    TrainingConfirmation traininfConfDetail = dbContext.TrainingConfirmations.
                        Where(x => x.TrainingConfirmationID == TrainerConfInfo.TrainingConfirmationID).FirstOrDefault();

                    traininfConfDetail.Product = TrainerConfInfo.Product;
                    traininfConfDetail.Course = TrainerConfInfo.Course;
                    traininfConfDetail.TotalNoOfDays = TrainerConfInfo.TotalNoOfDays;
                    traininfConfDetail.NoOfStudents = TrainerConfInfo.NoOfStudents;
                    traininfConfDetail.Private = TrainerConfInfo.Private;
                    traininfConfDetail.Public = TrainerConfInfo.Public;
                    traininfConfDetail.StartDate = TrainerConfInfo.StartDate;
                    traininfConfDetail.EndDate = TrainerConfInfo.EndDate;
                    traininfConfDetail.TrianerId = TrainerConfInfo.TrianerId;

                    traininfConfDetail.IsActive = true;

                    traininfConfDetail.ModifiedBy = USER_ID;
                    traininfConfDetail.ModifiedOn = UTILITY.SINGAPORETIME;

                    dbContext.Entry(traininfConfDetail).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("TrainingConfirmationList");
        }

        [HttpGet]
        public ActionResult TrainingConfirmationList()
        {
            List<TrainingConfirmation> trainingConfList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();

            List<TrainingConfirmationVM> list = new List<TrainingConfirmationVM>();
            foreach (var item in trainingConfList)
            {
                TrainingConfirmationVM trainingConfirmationVM = new TrainingConfirmationVM();
                trainingConfirmationVM.Id = item.Id;
                trainingConfirmationVM.TrainingConfirmationID = item.TrainingConfirmationID;
                trainingConfirmationVM.Product = item.Product;
                trainingConfirmationVM.Course = item.Course;
                trainingConfirmationVM.ProductName = new EduProductBO().GetList().Where(x => x.Id == item.Product).FirstOrDefault() != null ? new EduProductBO().GetList().Where(x => x.Id == item.Product).FirstOrDefault().ProductName : "";
                trainingConfirmationVM.CourseName = new CourseBO().GetList().Where(x => x.Id == item.Course).FirstOrDefault() != null ? new CourseBO().GetList().Where(x => x.Id == item.Course).FirstOrDefault().CourseName : "";
                trainingConfirmationVM.TotalNoOfDays = item.TotalNoOfDays;
                trainingConfirmationVM.NoOfStudents = item.NoOfStudents;
                trainingConfirmationVM.Private = item.Private;
                trainingConfirmationVM.Public = item.Public;
                trainingConfirmationVM.StartDate = item.StartDate;
                trainingConfirmationVM.EndDate = item.EndDate;
                trainingConfirmationVM.TrianerId = item.TrianerId;
                trainingConfirmationVM.TrianerName = dbContext.TrainerInformations.Where(x => x.TrianerId == item.TrianerId).FirstOrDefault() != null ? dbContext.TrainerInformations.Where(x => x.TrianerId == item.TrianerId).FirstOrDefault().TrainerName : "";

                list.Add(trainingConfirmationVM);
            }


            return View(list);
        }


        [HttpPost]
        public ActionResult DeletetrainingConf(int Id)
        {
            TrainingConfirmation trainingConfDetail = dbContext.TrainingConfirmations.
                   Where(x => x.Id == Id).FirstOrDefault();
            if (trainingConfDetail != null)
            {
                trainingConfDetail.IsActive = false;
                dbContext.SaveChanges();
            }
            //List<TrainingConfirmationVM> list = new List<TrainingConfirmationVM>();
            return RedirectToAction("TrainingConfirmationList");
        }
        #endregion
    }
}
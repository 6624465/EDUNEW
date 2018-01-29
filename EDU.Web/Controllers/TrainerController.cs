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
                TrainerVM trainerVM = new TrainerVM();
                trainerVM.TrianerId = -1;

                return View(trainerVM);
            }
            else
            {
                TrainerVM trainerVM = dbContext.TrainerInformations.
                Where(x => x.TrianerId == Id && x.IsActive == true)
                        .Select(x => new TrainerVM
                        {
                            TrianerId = TrainerInfo.TrianerId,
                            Technology = TrainerInfo.Technology,
                            Country = TrainerInfo.Country,
                            CountryName = TrainerInfo.CountryName,
                            TrainerRate = TrainerInfo.TrainerRate,
                            VendorName = TrainerInfo.VendorName,
                            Address = TrainerInfo.Address,
                            Contact = TrainerInfo.Contact,
                            Remarks = TrainerInfo.Remarks,
                            TrainerName = TrainerInfo.TrainerName,
                            CreatedBy = TrainerInfo.CreatedBy,
                            CreatedOn = TrainerInfo.CreatedOn,
                            ModifiedBy = TrainerInfo.ModifiedBy,
                            ModifiedOn = TrainerInfo.ModifiedOn,
                            IsActive = true,
                            Profile = TrainerInfo.Profile
                        }).FirstOrDefault();
                return View(trainerVM);
            }
        }

        [HttpPost]
        public ActionResult SaveTrainer(TrainerVM TrainerInfo)
        {
            try
            {
                if (TrainerInfo.TrianerId == -1)
                {
                    TrainerInformation trainer = new TrainerInformation();

                    trainer.TrianerId = TrainerInfo.TrianerId;
                    trainer.Technology = TrainerInfo.Technology;
                    trainer.Country = TrainerInfo.Country;
                    trainer.CountryName = TrainerInfo.CountryName;
                    trainer.TrainerRate = TrainerInfo.TrainerRate;
                    trainer.VendorName = TrainerInfo.VendorName;
                    trainer.Address = TrainerInfo.Address;
                    trainer.Contact = TrainerInfo.Contact;
                    trainer.Remarks = TrainerInfo.Remarks;
                    trainer.TrainerName = TrainerInfo.TrainerName;

                    trainer.CreatedBy = USER_ID;
                    trainer.CreatedOn = UTILITY.SINGAPORETIME;
                    trainer.IsActive = true;

                    if (TrainerInfo.FileName != null && TrainerInfo.FileName.ContentLength > 0)
                    {
                        trainer.Profile = TrainerInfo.FileName.FileName;
                    }
                    else
                        trainer.Profile = null;

                    dbContext.TrainerInformations.Add(trainer);
                    dbContext.SaveChanges();

                    if (TrainerInfo.FileName != null && TrainerInfo.FileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/" + trainer.TrianerId + "/");
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
                        trainerInfoDetail.Profile = TrainerInfo.FileName.FileName;
                    }
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

        public PartialViewResult TrainerConfirmation(short? month, int year, int Id = -1)
        {
            TrainingConfirmation TrainingConfInfo = dbContext.TrainingConfirmations.
                Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();

            ViewData["ProductData"] = new EduProductBO().GetList().Where(x => x.IsActive == true).ToList();
            var CourseData = new CourseBO().GetList().Where(x => x.IsActive == true).ToList();

            if (Session["AccessRights"].ToString() == "false")
            {
                CourseData = CourseData.Where(x => x.Country == Convert.ToUInt16(Session["BranchId"])).ToList();
            }

            ViewData["CourseData"] = CourseData;
            ViewData["TrainerData"] = dbContext.TrainerInformations.Where(x => x.IsActive == true).OrderBy(y=>y.TrainerName).ToList();
            ViewData["CountryData"] = new BranchBO().GetList().Where(x => x.IsActive == true).ToList();

            if (TrainingConfInfo == null)
            {
                TrainingConfInfo = new TrainingConfirmation();
                TrainingConfInfo.Id = -1;
                TrainingConfInfo.Public = true;
                TrainingConfInfo.Year = year;
                TrainingConfInfo.Month = month;

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
                    TrainerConfInfo.Year = (TrainerConfInfo.StartDate != null ? TrainerConfInfo.StartDate.Value.Year : TrainerConfInfo.Year);
                    TrainerConfInfo.Month = (TrainerConfInfo.StartDate != null ? Convert.ToInt16(TrainerConfInfo.StartDate.Value.Month) : (TrainerConfInfo.Month == 0 ? Convert.ToInt16(DateTime.UtcNow.Month) : TrainerConfInfo.Month));

                    dbContext.TrainingConfirmations.Add(TrainerConfInfo);
                }
                else
                {
                    TrainingConfirmation traininfConfDetail = dbContext.TrainingConfirmations.
                        Where(x => x.TrainingConfirmationID == TrainerConfInfo.TrainingConfirmationID).FirstOrDefault();

                    traininfConfDetail.Country = TrainerConfInfo.Country;
                    traininfConfDetail.Product = TrainerConfInfo.Product;
                    traininfConfDetail.Course = TrainerConfInfo.Course;
                    traininfConfDetail.TotalNoOfDays = TrainerConfInfo.TotalNoOfDays;
                    traininfConfDetail.NoOfStudents = TrainerConfInfo.NoOfStudents;
                    traininfConfDetail.Private = TrainerConfInfo.Private;
                    traininfConfDetail.Public = TrainerConfInfo.Public;
                    traininfConfDetail.LVC = TrainerConfInfo.LVC;
                    traininfConfDetail.StartDate = TrainerConfInfo.StartDate;
                    traininfConfDetail.EndDate = TrainerConfInfo.EndDate;
                    traininfConfDetail.Year = (TrainerConfInfo.StartDate != null ? TrainerConfInfo.StartDate.Value.Year : TrainerConfInfo.Year);
                    traininfConfDetail.Month = (TrainerConfInfo.StartDate != null ? Convert.ToInt16(TrainerConfInfo.StartDate.Value.Month) : (TrainerConfInfo.Month == 0 ? Convert.ToInt16(DateTime.UtcNow.Month) : TrainerConfInfo.Month));
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
            return RedirectToAction("TrainingConfirmationList", new { month = TrainerConfInfo.Month, year = TrainerConfInfo.Year });
        }

        [HttpGet]
        public ActionResult TrainingConfirmationList(short? month, int year)
        {
            List<TrainingConfirmation> trainingConfList = new List<TrainingConfirmation>();
            if (month == 0)
                trainingConfList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true && x.Year == year).ToList();
            else
                trainingConfList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true && x.Year == year && x.Month == month).ToList();

            if (Session["AccessRights"].ToString() == "false")
            {
                trainingConfList = trainingConfList.Where(x => x.Country == Convert.ToUInt16(Session["BranchId"])).ToList();
            }

            List<TrainingConfirmationVM> list = new List<TrainingConfirmationVM>();
            foreach (var item in trainingConfList)
            {
                TrainingConfirmationVM trainingConfirmationVM = new TrainingConfirmationVM();
                trainingConfirmationVM.Id = item.Id;
                trainingConfirmationVM.TrainingConfirmationID = item.TrainingConfirmationID;
                trainingConfirmationVM.Product = item.Product;
                trainingConfirmationVM.Course = item.Course;
                trainingConfirmationVM.ProductName = new EduProductBO().GetList().Where(x => x.Id == item.Product).FirstOrDefault() != null ? new EduProductBO().GetList().Where(x => x.Id == item.Product).FirstOrDefault().ProductName : "";
                trainingConfirmationVM.CourseName = new CourseBO().GetList().Where(x => x.Id == item.Course && x.Product == item.Product && x.Country == item.Country).FirstOrDefault() != null ? new CourseBO().GetList().Where(x => x.Id == item.Course && x.Product == item.Product && x.Country == item.Country).FirstOrDefault().CourseName : "";
                trainingConfirmationVM.TotalNoOfDays = item.TotalNoOfDays;
                trainingConfirmationVM.NoOfStudents = item.NoOfStudents;
                trainingConfirmationVM.Private = item.Private;
                trainingConfirmationVM.Public = item.Public;
                trainingConfirmationVM.LVC = item.LVC;
                trainingConfirmationVM.StartDate = item.StartDate;
                trainingConfirmationVM.EndDate = item.EndDate;
                trainingConfirmationVM.Year = item.Year;
                trainingConfirmationVM.Month = item.Month;
                trainingConfirmationVM.TrianerId = item.TrianerId;
                trainingConfirmationVM.TrianerName = dbContext.TrainerInformations.Where(x => x.TrianerId == item.TrianerId).FirstOrDefault() != null ? dbContext.TrainerInformations.Where(x => x.TrianerId == item.TrianerId).FirstOrDefault().TrainerName : "";
                trainingConfirmationVM.Country = item.Country;
                trainingConfirmationVM.CountryName = new BranchBO().GetList().Where(x => x.BranchID == item.Country).FirstOrDefault() != null ? new BranchBO().GetList().Where(x => x.BranchID == item.Country).FirstOrDefault().BranchName : "";

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
            return RedirectToAction("TrainingConfirmationList", new { month = trainingConfDetail.Month, year = trainingConfDetail.Year });
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.EDU.Contract;
using EZY.EDU.BusinessFactory;

using EDU.Web.ViewModels.Master;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class MasterController : BaseController
    {

        [HttpGet]
        public JsonResult GetCoursesByProductCoutry(int Id, short country)
        {
            var courseList = new CourseBO().GetCoursesByProductCoutry(Id, country);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }



        #region EduProduct
        [HttpGet]
        public ViewResult EduProductList()
        {
            var list = new EduProductBO().GetList();
            return View("EduProductList", list.AsEnumerable());
        }

        [HttpGet]
        public PartialViewResult EduProduct(int? Id)
        {
            if (!Id.HasValue)
            {
                ViewBag.Title = "New Product";
                return PartialView(new EduProduct { Id = -1, IsActive = true });
            }
            else
            {
                ViewBag.Title = "Update Product";
                return PartialView(new EduProductBO().GetEduProduct(new EduProduct { Id = Id.Value }));

            }
        }

        [HttpPost]
        public RedirectToRouteResult SaveEduProduct(EduProduct eduProduct)
        {
            eduProduct.ProductDescription = eduProduct.ProductName;

            eduProduct.CreatedBy = USER_ID;
            eduProduct.CreatedOn = UTILITY.SINGAPORETIME;
            eduProduct.ModifiedBy = USER_ID;
            eduProduct.ModifiedOn = UTILITY.SINGAPORETIME;
            if (!IsEduProductExists(eduProduct.ProductName))
            {
                var result = new EduProductBO().SaveEduProduct(eduProduct);
            }

            return RedirectToAction("EduProductList");
        }

        [HttpPost]
        public ActionResult DeleteEduProduct(int? Id)
        {
            var result = new EduProductBO().DeleteEduProduct(new EduProduct { Id = Id.Value });

            var list = new EduProductBO().GetList();
            return View("EduProductList", list.AsEnumerable());
        }


        [HttpGet]
        public bool IsEduProductExists(string productName)
        {
            return new EduProductBO().IsEduProductExists(new EduProduct { ProductName = productName });
        }

        #endregion

        #region Course
        [HttpGet]
        public ViewResult CourseList()
        {
            var list = new CourseBO().GetList().AsEnumerable();
            return View("CourseList", list);
        }

        [HttpGet]
        public PartialViewResult Course(int? Id)
        {
            ViewData["ProductData"] = new EduProductBO().GetList();
            ViewData["CountryData"] = new BranchBO().GetList();
            if (!Id.HasValue)
            {
                ViewBag.Title = "New Course";
                return PartialView(new Course { Id = -1, IsActive = true });
            }
            else
            {
                ViewBag.Title = "Update Course";
                return PartialView(new CourseBO().GetCourse(new Course { Id = Id.Value }));
            }
        }

        [HttpGet]
        public JsonResult GetCourse(int Id)
        {
            return Json(new CourseBO().GetCourse(new Course { Id = Id }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult SaveCourse(Course course)
        {
            course.CourseDescription = course.CourseName;
            course.CreatedBy = USER_ID;
            course.CreatedOn = UTILITY.SINGAPORETIME;
            course.ModifiedBy = USER_ID;
            course.ModifiedOn = UTILITY.SINGAPORETIME;
            course.AvailableSeats = 0;
            var result = new CourseBO().SaveCouse(course);

            return RedirectToAction("CourseList");
        }


        [HttpPost]
        public ActionResult DeleteEduCourse(int? Id)
        {
            var result = new CourseBO().DeleteEduCourse(new Course { Id = Id.Value });

            var list = new CourseBO().GetList();
            return View("CourseList", list);
        }

        [HttpGet]
        public bool IsEduCourseExists(string courseName, short country, int product)
        {
            return new CourseBO().IsEduCourseExists(new Course { CourseName = courseName, Country = country, Product = product, });
        }
        #endregion

        #region Course Sales Master
        [HttpGet]
        public ViewResult CourseSalesMasterList()
        {
            var list = new CourseSalesMasterBO().GetList();
            var totalleadsonhand = 0;
            long totalrevenue = 0;

            foreach (var item in list)
            {
                totalleadsonhand += item.LeadsOnHead;
                totalrevenue += Convert.ToInt64(item.Revenue == null ? 0 : item.Revenue.Value);
            }

            ViewData["totalleadsonhand"] = totalleadsonhand;
            ViewData["totalrevenue"] = totalrevenue;

            var prodList = new EduProductBO().GetList().OrderBy(x => x.Id).ToList();
            ViewData["ProductList"] = prodList;
            return View("CourseSalesMasterList", list.AsEnumerable());
        }

        [HttpGet]
        public ViewResult CourseSalesMaster(int Id)
        {
            CourseSalesMaster courseSalesMaster = null;
            var courseSalesMasterVm = new CourseSalesMasterVm
            {
                eduProductList = new EduProductBO().GetList().AsEnumerable(),
                branchList = new BranchBO().GetList().AsEnumerable(),
                monthList = GetMonthData(),
                courseSalesMaster = courseSalesMaster
            };

            if (Id == -1)
            {
                courseSalesMaster = new CourseSalesMaster
                {
                    Id = Id
                    //StartDate = UTILITY.SINGAPORETIME,
                    //EndDate = UTILITY.SINGAPORETIME,
                    //RegClosingDate = UTILITY.SINGAPORETIME.AddDays(-14)
                };
            }
            else
            {
                courseSalesMaster = new CourseSalesMasterBO()
                                            .GetCourseSalesMaster(new CourseSalesMaster { Id = Id });

                courseSalesMasterVm.courseList = new CourseBO()
                                                    .GetCoursesByProductCoutry(courseSalesMaster.Product, courseSalesMaster.Country)
                                                    .AsEnumerable();
            }

            courseSalesMasterVm.courseSalesMaster = courseSalesMaster;
            return View("CourseSalesMaster", courseSalesMasterVm);
        }

        [HttpPost]
        public RedirectToRouteResult CoureSalesMaster(CourseSalesMaster courseSalesMaster)
        {
            courseSalesMaster.IsActive = true;
            courseSalesMaster.CreatedBy = USER_ID;
            courseSalesMaster.CreatedOn = UTILITY.SINGAPORETIME;
            courseSalesMaster.ModifiedBy = USER_ID;
            courseSalesMaster.ModifiedOn = UTILITY.SINGAPORETIME;
            courseSalesMaster.ConfirmOrDropDate = UTILITY.SINGAPORETIME;

            courseSalesMaster.Id = new CourseSalesMasterBO().SaveCourseSalesMaster(courseSalesMaster);
            return RedirectToAction("CourseSalesMasterList");
        }


        [HttpPost]
        public ActionResult DeleteCourseSalesMaster(int? Id)
        {
            var result = new CourseSalesMasterBO().DeleteCourseSalesMaster(new CourseSalesMaster { Id = Id.Value });

            var list = new CourseSalesMasterBO().GetList();
            var totalleadsonhand = 0;
            long totalrevenue = 0;

            foreach (var item in list)
            {
                totalleadsonhand += item.LeadsOnHead;
                totalrevenue += Convert.ToInt64(item.Revenue == null ? 0 : item.Revenue.Value);
            }

            ViewData["totalleadsonhand"] = totalleadsonhand;
            ViewData["totalrevenue"] = totalrevenue;

            return View("CourseSalesMasterList", list.AsEnumerable());
        }
        #endregion
    }
}
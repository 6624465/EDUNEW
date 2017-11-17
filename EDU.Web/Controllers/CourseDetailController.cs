using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZY.EDU.BusinessFactory;
using EZY.EDU.Contract;
using EDU.Web.ViewModels.CourseDetail;
using System.Globalization;

namespace EDU.Web.Controllers
{
    [WebSsnFilter]
    public class CourseDetailController : BaseController
    {
        [HttpGet]
        public ViewResult List()
        {            
            return View("List", new CourseDetailBO().GetList());
        }

        

        [HttpGet]
        public ViewResult CourseDetail(int id)
        {
            CourseDetail courseDetail = null;
            List<Course> courseData = new List<Course>();
            if (id == -1)
                courseDetail = new CourseDetail
                {
                    Id = id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Course = -1
                };
            else
            {
                courseDetail = new CourseDetailBO()
                    .GetCourseDetail(new CourseDetail { Id = id });

                courseData = new CourseBO().GetCoursesByProduct(courseDetail.Product);
            }

            var courseDetailVm = new CourseDetailVm {
                courseDetail = courseDetail,
                countryList = new CountryBO().GetList(),
                MonthData = GetMonthData(),
                EduProductData = new EduProductBO().GetList(),
                CourseData = courseData
            };

            return View("CourseDetail", courseDetailVm);
        }        

        [HttpPost]
        public RedirectToRouteResult CourseDetail(CourseDetail courseDetail)
        {
            var result = new CourseDetailBO().SaveCouseDetail(courseDetail);
            return RedirectToAction("List");
        }

        [HttpGet]
        public JsonResult GetCoursesByProduct(int Id)
        {
            var courseList = new CourseBO().GetCoursesByProduct(Id);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult SaveCourseDetail(CourseDetail courseDetail)
        {
            courseDetail.CreatedBy = USER_ID;
            courseDetail.CreatedOn = UTILITY.SINGAPORETIME;
            courseDetail.ModifiedBy = USER_ID;
            courseDetail.ModifiedOn = UTILITY.SINGAPORETIME;

            var result = new CourseDetailBO().SaveCouseDetail(courseDetail);
            return RedirectToAction("List");
        }
    }
}
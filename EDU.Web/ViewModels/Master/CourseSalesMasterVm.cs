using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EZY.EDU.Contract;
using EDU.Web.ViewModels.CourseDetail;

namespace EDU.Web.ViewModels.Master
{
    public class CourseSalesMasterVm
    {
        public CourseSalesMaster courseSalesMaster { get; set; }

        public IEnumerable<Branch> branchList { get; set; }

        public IEnumerable<Course> courseList { get; set; }

        public IEnumerable<EduProduct> eduProductList { get; set; }

        public IEnumerable<MonthVm> monthList { get; set; }
    }
}
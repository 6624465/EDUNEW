using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contract = EZY.EDU.Contract;

namespace EDU.Web.ViewModels.CourseDetail
{
    public class CourseDetailVm
    {
        public Contract.CourseDetail courseDetail { get; set; }

        public List<Contract.Country> countryList { get; set; }

        public IEnumerable<MonthVm> MonthData { get; set; }

        public IEnumerable<Contract.EduProduct> EduProductData { get; set; }

        public List<Contract.Course> CourseData { get; set; }
    }

    public class MonthVm
    {
        public string Name { get; set; }

        public Int16 Value { get; set; }
    }
}
using EDU.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.CourseModel
{
    public class CourseVM
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public short NoOfDays { get; set; }
        public string Country { get; set; }
        public short AvailableSeats { get; set; }
        public decimal PublicPrice { get; set; }
        public decimal PrivatePrice { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }



        public string CountryName { get; set; }
        public string ProductName { get; set; }
    }
}
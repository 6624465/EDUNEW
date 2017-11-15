using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class Course : IContract
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public Int16 NoOfDays { get; set; }
        public string Country { get; set; }
        public Int16 AvailableSeats { get; set; }
        public decimal PublicPrice { get; set; }
        public decimal PrivatePrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

    public class CourseVm : IContract
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public Int16 NoOfDays { get; set; }
        public string CountryName { get; set; }
        public Int16 AvailableSeats { get; set; }
        public decimal PublicPrice { get; set; }
        public decimal PrivatePrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.Registration
{
    public class RegistrationVM
    {
        public Nullable<int> Year { get; set; }
        public Nullable<short> Month { get; set; }
        public List<Web.Models.Registration> registration { get; set; }
        public List<Web.Models.Registration> registrationPrivate { get; set; }
        public List<Web.Models.TrainingConfirmation> trainingconf { get; set; }
        public TrainingConfirmDtl trainingconfDetail { get; set; }

    }

    public class TrainingConfirmDtl
    {
        public int Id { get; set; }
        public string TrainingConfirmationID { get; set; }
        public int Product { get; set; }
        public int Course { get; set; }
        public short TotalNoOfDays { get; set; }
        public short NoOfStudents { get; set; }
        public bool Private { get; set; }
        public bool Public { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int TrianerId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public string ProductName { get; set; }
        public string CourseName { get; set; }
        public string TrianerName { get; set; }
        public bool LVC { get; set; }
    }
}
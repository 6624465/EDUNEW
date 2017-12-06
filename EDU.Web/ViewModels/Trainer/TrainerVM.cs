using EDU.Web.Models;
using EZY.EDU.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.Trainer
{
    public class TrainerVM
    {
        public int TrianerId { get; set; }
        public string Technology { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
        public string Profile { get; set; }
        public Nullable<decimal> TrainerRate { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public string TrainerName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public System.Web.HttpPostedFileBase FileName { get; set; }
    }
}
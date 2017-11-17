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
        public TrainerInformationVM TrainerInformation { get; set; }
        public IEnumerable<Country> countryList { get; set; }

    }

    public class TrainerInformationVM {
        public int TrianerId { get; set; }
        public string Technology { get; set; }
        public string Country { get; set; }
        public HttpPostedFileBase Profile { get; set; }
        public Nullable<decimal> TrainerRate { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public string TrainerName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
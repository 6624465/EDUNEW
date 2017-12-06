using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.CustomerPaymentStatusModel
{
    public class CustomerPaymentStatusVM
    {
        public List<Web.Models.CustomerPayment> customerPayment { get; set; }
        public List<Web.Models.TrainingConfirmation> trainingconf { get; set; }
        public TrainingConfirmDtl trainingconfDetail { get; set; }
    }

    public class TrainingConfirmDtl
    {
        public int Id { get; set; }
        public string TrainingConfirmationID { get; set; }
        public int Product { get; set; }
        public int Course { get; set; }

        public string ProductName { get; set; }
        public string CourseName { get; set; }
        public string TrianerName { get; set; }
    }
}
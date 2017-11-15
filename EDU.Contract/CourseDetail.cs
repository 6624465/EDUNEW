using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class CourseDetail : IContract
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public Int16 Month { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CustomerName { get; set; }

        public int Product { get; set; }

        public int Course { get; set; }

        public string Particulars { get; set; }

        public Int64 Days { get; set; }

        public Int64 NoPax { get; set; }

        public decimal LabRental { get; set; }

        public decimal SalesRev { get; set; }

        public string TrainerName { get; set; }

        public string DeliveredBy { get; set; }
        public decimal Airfare { get; set; }
        public string Hotel { get; set; }
        public decimal TrainerFee { get; set; }
        public string MISCLocal { get; set; }
        public decimal RegionalExpenses { get; set; }
        public string CourseMaterial { get; set; }
        public decimal TotalExp { get; set; }
        public decimal Profit { get; set; }
        public decimal Percentage { get; set; }
        public string RegionInvoice { get; set; }
        public string PaymentStatus { get; set; }

        public bool IsActive {get;set;}
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

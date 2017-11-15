using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
    public class CourseSalesMaster : IContract
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public int Product { get; set; }

        public int Course { get; set; }

        public Int16 NoOfDays { get; set; }

        public Int16? Month { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Int16 MinimumReg { get; set; }

        public Int16 LeadsOnHead { get; set; }

        public Int16 Registered { get; set; }

        public Int16 AvailableSeats { get; set; }

        public DateTime? RegClosingDate { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
        public string LeadsOnHeadRemarks { get; set; }
        public string RegisteredRemarks { get; set; }
        public bool? TOD { get; set; }
        public bool? LVC { get; set; }
    }

    public class CourseSalesMasterViewModel : IContract
    {
        public int Id { get; set; }

        public string CountryName { get; set; }

        public string ProductName { get; set; }

        public string CourseName { get; set; }

        public Int16 NoOfDays { get; set; }

        public Int16? Month { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Int16 MinimumReg { get; set; }

        public Int16 LeadsOnHead { get; set; }

        public Int16 Registered { get; set; }

        public Int16 AvailableSeats { get; set; }

        public DateTime RegClosingDate { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
        public string LeadsOnHeadRemarks { get; set; }
        public string RegisteredRemarks { get; set; }
        public bool? TOD { get; set; }
        public bool? LVC { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.OperationalTransaction
{
    public class OperationalTransactionReportVM
    {
        public List<OperationalTransactionReportByMonth> otReportByMonth { get; set; }
        public OperationalTransactionReportByYear otReportByYear { get; set; }
    }

    public class OperationalTransactionReportByMonth
    {
        public string ParticularName { get; set; }
        public decimal Amount { get; set; }
        public Nullable<short> Month { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
        public int Year { get; set; }
    }

    public class OperationalTransactionReportByYear
    {
        public short Country { get; set; }
        public string CountryName { get; set; }
        public int Year { get; set; }
        public decimal JanAmount { get; set; }
        public decimal FebAmount { get; set; }
        public decimal MarAmount { get; set; }
        public decimal AprAmount { get; set; }
        public decimal MayAmount { get; set; }
        public decimal JunAmount { get; set; }
        public decimal JulAmount { get; set; }
        public decimal AugAmount { get; set; }
        public decimal SepAmount { get; set; }
        public decimal OctAmount { get; set; }
        public decimal NovAmount { get; set; }
        public decimal DecAmount { get; set; }
    }
}
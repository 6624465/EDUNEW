using EDU.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.OperationalTransactionModel
{
    public class OperationalTransactionVM
    {
        public int OperationalTransactionId { get; set; }
        public int CategoryId { get; set; }
        public int ParticularsId { get; set; }
        public int? Year { get; set; }
        public Nullable<short> Month { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CategoryIdDesc { get; set; }
        public string ParticularsIdDesc { get; set; }
        public string MonthDesc { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
    }

    public class OperationalTransactionReportVM
    {
        public int CategoryId { get; set; }
        public int ParticularsId { get; set; }
        public int? Year { get; set; }
        public string CategoryIdDesc { get; set; }
        public string ParticularsIdDesc { get; set; }
        public decimal? AprAmount { get; set; }
        public decimal? MayAmount { get; set; }
        public decimal? JuneAmount { get; set; }
        public decimal? JulyAmount { get; set; }
        public decimal? AugAmount { get; set; }
        public decimal? SepAmount { get; set; }
        public decimal? OctAmount { get; set; }
        public decimal? NovAmount { get; set; }
        public decimal? DecAmount { get; set; }
        public decimal? JanAmount { get; set; }
        public decimal? FebAmount { get; set; }
        public decimal? MarAmount { get; set; }
        public decimal? YTD { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
    }


    public class OTVM
    {
        public int OperationalTransactionId { get; set; }
        public int OperationalExpId { get; set; }
        public int OtherExpId { get; set; }
        public int? Year { get; set; }
        public Nullable<short> Month { get; set; }

        public int SalariesId { get; set; }
        public int TravellingexpId { get; set; }
        public int RentalId { get; set; }
        public int TelephoneexpId { get; set; }
        public int CourierchargesId { get; set; }
        public int InsuranceId { get; set; }
        public int UtilityId { get; set; }
        public int MarketingexpId { get; set; }
        public int DepreciationId { get; set; }
        public int LegalexpId { get; set; }
        public int RepairmaintenanceId { get; set; }
        public int BankchargesId { get; set; }
        public int PrintingstationeryId { get; set; }
        public int StaffwelfareId { get; set; }
        public int OtherexpensesincomeId { get; set; }

        public decimal SalariesAmount { get; set; }
        public decimal TravellingexpAmount { get; set; }
        public decimal RentalAmount { get; set; }
        public decimal TelephoneexpAmount { get; set; }
        public decimal CourierchargesAmount { get; set; }
        public decimal InsuranceAmount { get; set; }
        public decimal UtilityAmount { get; set; }
        public decimal MarketingexpAmount { get; set; }
        public decimal DepreciationAmount { get; set; }
        public decimal LegalexpAmount { get; set; }
        public decimal RepairmaintenanceAmount { get; set; }
        public decimal BankchargesAmount { get; set; }
        public decimal PrintingstationeryAmount { get; set; }
        public decimal StaffwelfareAmount { get; set; }
        public decimal OtherexpensesincomeAmount { get; set; }
        public bool IsActive { get; set; }
        public short Country { get; set; }
        public string CountryName { get; set; }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDU.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EducationEntities : DbContext
    {
        public EducationEntities()
            : base("name=EducationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Revenue> Revenues { get; set; }
        public virtual DbSet<TrainerInformation> TrainerInformations { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<CustomerPayment> CustomerPayments { get; set; }
        public virtual DbSet<VendorPayment> VendorPayments { get; set; }
        public virtual DbSet<OperationalTransaction> OperationalTransactions { get; set; }
        public virtual DbSet<TrainingConfirmation> TrainingConfirmations { get; set; }
        public virtual DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    
        public virtual ObjectResult<usp_OperationalTransactionReport_Result> usp_OperationalTransactionReport(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_OperationalTransactionReport_Result>("usp_OperationalTransactionReport", yearParameter);
        }
    }
}

using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class CompanyBO
    {
        private CompanyDAL companyDAL;
        public CompanyBO()
        {

            companyDAL = new CompanyDAL();
        }

        public List<Company> GetList()
        {
            return companyDAL.GetList();
        }


        public bool SaveCompany(Company newItem)
        {

            return companyDAL.Save(newItem);

        }

        public bool DeleteCompany(Company item)
        {
            return companyDAL.Delete(item);
        }

        public Company GetCompany(Company item)
        {
            return (Company)companyDAL.GetItem<Company>(item);
        }

        public Address GetCompanyAddress(Company item)
        {
            return companyDAL.GetCompanyAddress(item);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.EDU.Contract;
using EZY.EDU.BusinessFactory;

namespace EDU.Web.Controllers
{
    [WebSsnFilter]
    public class CompanyController : BaseController
    {        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [EncryptedActionParameter]
        public ActionResult CompanyProfile(string companyCode = "")
        {
            Company company;
            if (!string.IsNullOrWhiteSpace(companyCode))
                company = new CompanyBO().GetCompany(new Company { CompanyCode = companyCode });
            else
                company = new Company();

            company.CountryList = new CountryBO().GetList().Select(c =>
                                    new SelectListItem
                                    {
                                        Value = c.CountryCode,
                                        Text = c.CountryName
                                    });            
            return View("CompanyProfile", company);
        }

        [ChildActionOnly]        
        public PartialViewResult GetCompanies()
        {
            var companyList = new CompanyBO().GetList();
            return PartialView(companyList);
        }


        [HttpPost]        
        public ActionResult SaveCompanyProfile(Company company)
        {
            company.CreatedBy = USER_ID;
            company.ModifiedBy = USER_ID;
            company.IsActive = true;

            if (company.CompanyAddress.AddressId == 0)
            {
                company.CompanyAddress.AddressType = "Company";
                company.CompanyAddress.SeqNo = 1;
                company.CompanyAddress.AddressLinkID = company.CompanyCode;
            }

            var result = new CompanyBO().SaveCompany(company);
            var encryptedStr = UrlEncryptionHelper.Encrypt(string.Format("companyCode={0}", company.CompanyCode));
            return RedirectToAction("CompanyProfile", "Company", 
                new { q = encryptedStr });

        }

        
        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult BranchProfile(string companyCode, string branchCode)
        {
            var branchprofile = new Branch();
            

            if (branchCode != "NEW")
            {
                branchprofile = new BranchBO().GetBranch(new Branch { BranchCode = branchCode });
            }

            if (branchprofile == null || branchCode == "NEW")
            {
                branchprofile = new Branch();
                branchprofile.BranchID = -1;
                branchprofile.CompanyCode = companyCode;
            }

            branchprofile.CountryList = new CountryBO().GetList().Select(c =>
                                    new SelectListItem
                                    {
                                        Value = c.CountryCode,
                                        Text = c.CountryName
                                    });

            return View("BranchProfile", branchprofile);
        }

        [HttpPost]        
        public ActionResult SaveBranchProfile(Branch branch)
        {

            branch.CreatedBy = USER_ID;
            branch.ModifiedBy = USER_ID;
            branch.IsActive = true;

            if (branch.BranchAddress.AddressId == 0)
            {
                branch.BranchAddress.AddressType = "Branch";
                branch.BranchAddress.SeqNo = 1;
                branch.BranchAddress.IsActive = true;
                branch.BranchAddress.AddressLinkID = branch.BranchCode;

            }
            var result = new BranchBO().SaveBranch(branch);
            var encryptedStr = UrlEncryptionHelper.Encrypt(string.Format("companyCode={0}?branchCode={1}", branch.CompanyCode, branch.BranchCode));
            return RedirectToAction("BranchProfile", "Company", new { q = encryptedStr });
        }

        [HttpPost]
        public ActionResult DeleteBranch(short BranchID)
        {
            var result = new BranchBO().DeleteBranch(new Branch { BranchID = BranchID });
            return RedirectToAction("CompanyProfile");
        }


    }
}
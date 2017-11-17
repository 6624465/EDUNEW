using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDU.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserID")]
        //[EmailAddress]
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }

        [Required]
        [Display(Name = "Branch Code")]
        public short BranchID { get; set; }


        public IEnumerable<System.Web.Mvc.SelectListItem> CompaniesList { get; set; }
        public List<System.Web.Mvc.SelectListItem> BranchList { get; set; }
    }
}
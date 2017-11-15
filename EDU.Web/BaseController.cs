using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.EDU.Contract;
using System.IO;
using EDU.Web.ViewModels.CourseDetail;
using System.Globalization;

namespace EDU.Web
{
    public class BaseController : Controller
    {
        public SessionObject USER_OBJECT
        {
            get
            {
                return (SessionObject)System.Web.HttpContext.Current.Session["SSN_USER"];
            }
            set
            {
                Session["SSN_USER"] = value;
            }
        }

        public List<Securables> USER_SECURABLES
        {
            get
            {
                return (List<Securables>)System.Web.HttpContext.Current.Session["SSN_SECURABLES"];
            }
            set
            {
                Session["SSN_SECURABLES"] = value;
            }
        }

        public string USER_EMAIL
        {
            get
            {
                return USER_OBJECT.Email;
            }
        }

        public string USER_NAME
        {
            get
            {
                return USER_OBJECT.UserName;
            }
        }

        public string USER_ID
        {
            get
            {
                return USER_OBJECT.UserID;
            }
        }

        public short BRANCH_ID
        {
            get
            {
                return USER_OBJECT.BranchId;
            }
        }

        public string BRANCH_NAME
        {
            get
            {
                return USER_OBJECT.BranchName;
            }
        }

        public string COMPANY_ID
        {
            get
            {
                return USER_OBJECT.CompanyId;
            }
        }

        public string COMPANY_NAME
        {
            get
            {
                return USER_OBJECT.CompanyName;
            }
        }

        public string ROLE_CODE
        {
            get
            {
                return USER_OBJECT.RoleCode;
            }
        }

        public void CreateDirectory(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
                dirInfo.Create();
        }

        public IEnumerable<MonthVm> GetMonthData()
        {
            var monthData = DateTimeFormatInfo.CurrentInfo.MonthNames.AsEnumerable();

            List<MonthVm> result = new List<MonthVm>();
            MonthVm monthVm = null;
            Int16 LoopCount = 0;
            foreach (var month in monthData)
            {
                if (string.IsNullOrWhiteSpace(month))
                    continue;

                monthVm = new MonthVm
                {
                    Name = month,
                    Value = LoopCount++
                };

                result.Add(monthVm);
            }

            return result.AsEnumerable<MonthVm>();
        }
    }

    public class SessionObject
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleCode { get; set; }
        public short BranchId { get; set; }
        public string BranchName { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}
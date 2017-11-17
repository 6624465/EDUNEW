using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.EDU.Contract;
using EZY.EDU.BusinessFactory;

namespace EDU.Web.Controllers
{
    public class LayoutMenuRights
    {
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public List<SecurablesRights> securablesLst { get; set; }
    }


    public class SecurablesRights
    {
        public SecurablesRights() { }
        public string SecurableItem { get; set; }
        public string GroupID { get; set; }
        public string Description { get; set; }
        public string ActionType { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public bool hasRight { get; set; }
        public Int32 Sequence { get; set; }
        public Int32 ParentSequence { get; set; }
        public List<SecurablesRights> ActionMenus { get; set; }
    }

    public class RoleRightsMenu
    {
        public RoleRightsMenu() { }
        public string RoleCode { get; set; }
        public string SecurableItem { get; set; }
        public bool hasRight { get; set; }
    }

    [WebSsnFilter]
    public class RolesController : BaseController
    {

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult RoleRights(string Role = "")
        {
            List<LayoutMenuRights> lstMenu = new List<LayoutMenuRights>();
            if (!string.IsNullOrWhiteSpace(Role))
            {
                var securablesAll = (List<Securables>)System.Web.HttpContext.Current.Application["AppSecurables"];

                var securables = new RoleRightsBO().GetSecurableItemsListByRoleCode(Role);


                var menuItems = securablesAll.Where(x => x.ActionType == "TopMenu")
                                    .Select(x => new { securableItem = x.SecurableItem, Icon = x.Icon, GroupId = x.GroupID }).Distinct().ToList();


                for (var i = 0; i < menuItems.Count; i++)
                {
                    LayoutMenuRights item = new LayoutMenuRights();
                    item.MenuName = menuItems[i].securableItem;
                    item.Icon = menuItems[i].Icon;
                    item.securablesLst = securablesAll.Where(x => x.GroupID == menuItems[i].securableItem && (x.ActionType == "Menu"))
                                                   .Select(x => new SecurablesRights
                                                   {
                                                       SecurableItem = x.SecurableItem,
                                                       GroupID = x.GroupID,
                                                       Description = x.Description,
                                                       ActionType = x.ActionType,
                                                       Link = x.Link,
                                                       Icon = x.Icon,
                                                       hasRight = (securables.Where(j => j.SecurableItem == x.SecurableItem).Count() > 0),
                                                       Sequence = x.Sequence,
                                                       ParentSequence = x.ParentSequence,
                                                       ActionMenus = securablesAll.Where(y => y.GroupID == menuItems[i].securableItem && (y.ActionType == "Action") && y.ParentSequence == x.Sequence)
                                                                                   .Select(y => new SecurablesRights
                                                                                   {
                                                                                       SecurableItem = y.SecurableItem,
                                                                                       GroupID = y.GroupID,
                                                                                       Description = y.Description,
                                                                                       ActionType = y.ActionType,
                                                                                       Link = y.Link,
                                                                                       Icon = y.Icon,
                                                                                       hasRight = (securables.Where(jk => jk.SecurableItem == y.SecurableItem).Count() > 0),
                                                                                       Sequence = y.Sequence,
                                                                                       ParentSequence = y.ParentSequence
                                                                                   }).ToList<SecurablesRights>()
                                                   }).OrderBy(x => x.ParentSequence).ToList<SecurablesRights>();

                    if (item.securablesLst.Count > 0)
                    {
                        lstMenu.Add(item);
                    }
                }

                ViewBag.RoleCode = Role;
            }

            return View("RoleRights", lstMenu);
        }

        [HttpPost]
        public ActionResult SaveRights(List<RoleRightsMenu> right)
        {
            try
            {
                var lstRoleRights = new List<RoleRights>();

                right.Where(r => r.hasRight == true)
                    .ToList()
                    .ForEach(r => lstRoleRights.Add(new RoleRights { RoleCode = r.RoleCode, SecurableItem = r.SecurableItem }));

                var result = new RoleRightsBO().SaveRoleRights(lstRoleRights);                

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }

            return RedirectToAction("RoleRights");
        }
    }
}
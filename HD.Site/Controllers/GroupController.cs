using HD.Context;
using HD.Core;
using HD.IdentityManager;
using HD.IdentityManager.IService;
using HD.Site.Common;
using HD.Site.Models;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HD.Site.Controllers
{
    [Authorize]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IRoleService _roleService;
        public GroupController()
        {
            _groupService = IoC.Resolve<IGroupService>();
            _roleService = IoC.Resolve<IRoleService>();
        }

        // GET: Group
        public ActionResult Index(GroupViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _groupService.GetByFilter(model.KeyWord, currentPage, pageSize, out total);
            model.Groups = new PagedList<Group>(lst, currentPage, pageSize, total);

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            var model = new GroupModelDetail();
            var lstRoles = new List<RolesModelDetail>();
            foreach (var item in _roleService.GetAll())
            {
                lstRoles.Add(new RolesModelDetail
                {
                    Id = item.Id,
                    Descreption = item.Descreption,
                    IsCheck = false
                });
            }
            model.Group = new Group();
            model.Group.Status = true;
            model.Roles = lstRoles;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateNew(GroupModelDetail model)
        {
            if (!model.Roles.Any(n => n.IsCheck == true))
            {
                AlertWarning("Hãy chọn các quyền mà nhóm người dùng này được phép hoạt động.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                _groupService.BeginTran();
                try
                {
                    var check = _groupService.CheckContain(model.Group.Name);
                    if (check)
                    {
                        AlertWarning(InfoString.Instance.SetContainString("Tên nhóm"));
                        return View(model);
                    }

                    var group = new Group();
                    group.Name = model.Group.Name;
                    group.Descreption = model.Group.Descreption;
                    group.CreateBy = CurrentInstance.Instance.CurrentUser.UserName;
                    group.CreateDate = DateTime.Now;
                    group.Status = model.Group.Status;
                    _groupService.CreateNew(group);
                    _groupService.CommitChanges();

                    //add roles to group
                    foreach (var role in model.Roles)
                    {
                        if (role.IsCheck)
                        {
                            _roleService.AddRoleToGroup(role.Id, group.Id);
                        }
                    }
                    _roleService.CommitChanges();
                    _groupService.CommitTran();
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    _groupService.RollbackTran();
                    AlertError(InfoString.ERROR_SYSTEM);
                }
                AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            AlertWarning(InfoString.INVALID_INFO);
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var group = _groupService.GetByKey(id);
            var lstRoles = new List<RolesModelDetail>();
            var lstRoleGroupOfGroup = _roleService.GetRolesByGroupId(id);
            foreach (var item in _roleService.GetAll())
            {
                var roleMd = new RolesModelDetail();
                roleMd.Id = item.Id;
                roleMd.Descreption = item.Descreption;
                roleMd.IsCheck = false;
                var flag = lstRoleGroupOfGroup.Any(n => n.RoleId == item.Id);
                if (flag)
                {
                    roleMd.IsCheck = true;
                }

                lstRoles.Add(roleMd);
            }
            var model = new GroupModelDetail();
            model.Group = group;
            model.Roles = lstRoles;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(GroupModelDetail model)
        {
            if (!model.Roles.Any(n => n.IsCheck == true))
            {
                AlertWarning("Hãy chọn các quyền mà nhóm người dùng này được phép hoạt động.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                _groupService.BeginTran();
                try
                {
                    var check = _groupService.CheckContain(model.Group.Id, model.Group.Name);
                    if (check)
                    {
                        AlertWarning(InfoString.Instance.SetContainString("Tên nhóm"));
                        return View(model);
                    }

                    _groupService.DeleteRoleOfGroup(model.Group.Id);
                    _groupService.CommitChanges();
                    model.Group.UpdateBy = CurrentInstance.Instance.CurrentUser.UserName;
                    model.Group.UpdateDate = DateTime.Now;
                    _groupService.Update(model.Group);
                    _groupService.CommitChanges();

                    //add roles to group
                    foreach (var role in model.Roles)
                    {
                        if (role.IsCheck)
                        {
                            _roleService.AddRoleToGroup(role.Id, model.Group.Id);
                        }
                    }
                    _roleService.CommitChanges();
                    _groupService.CommitTran();

                }
                catch (Exception ex)
                {
                    LogError(ex);
                    _groupService.RollbackTran();
                    AlertError(InfoString.ERROR_SYSTEM);
                }
                AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }

            AlertWarning(InfoString.INVALID_INFO);
            return View(model);
        }

        [HttpPost]
        public JsonResult ViewDetail(int id)
        {
            var model = _groupService.GetByKey(id);
            return Json(new
            {
                groupName = model.Name,
                descreption = model.Descreption,
                createDate = model.CreateDate.Value.ToString("dd-MM-yyyy hh:mm:ss"),
                createBy = model.CreateBy,
                updateDate = model.UpdateDate.HasValue ? model.UpdateDate.Value.ToString("dd-MM-yyyy hh:mm:ss") : string.Empty,
                updateBy = model.UpdateBy,
                status = model.Status ? "Kích hoạt" : "Tạm khóa"
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _groupService.BeginTran();
            try
            {
                var flag = _groupService.CheckGroupInAnyUser(id);
                if (flag)
                {
                    AlertInfo("Nhóm người dùng này đang được sử dụng, xóa thất bại.");
                }
                else
                {
                    _groupService.DeleteRoleOfGroup(id);
                    _groupService.CommitChanges();
                    _groupService.Delete(id);
                    _groupService.CommitChanges();
                    _groupService.CommitTran();
                    AlertSuccess(InfoString.DELETE_SUCCESSFULL);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                _groupService.RollbackTran();
            }

            return RedirectToAction("Index");
        }
    }
}
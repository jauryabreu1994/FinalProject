using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using PosLibrary.Model.Entities.Users;
using System;
using System.Linq;

namespace PosLibrary.Controller.Users
{
    public class GroupPermissionController : IMainController
    {
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.GroupPermission.Where(x => x.Id == Id).FirstOrDefault();
                    var data_2 = line.UserGroup;

                    return new CommonResult(true, string.Empty, line);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }            
        }

        public CommonResult GetList() 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var lines = ctx.GroupPermission.Where(a => !a.Deleted && a.Condition_Status).ToList();

                    return new CommonResult(true, string.Empty, lines);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }

        public CommonResult GetList(string filter)
        {
            try
            {
                return GetList();
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }
        public CommonResult GetList(int UserGroupId)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var lines = ctx.GroupPermission.Where(a => a.UserGroupId == UserGroupId && !a.Deleted && a.Condition_Status).ToList();

                    return new CommonResult(true, string.Empty, lines);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }
        public CommonResult Save(object data) 
        {
            try
            {
                GroupPermission user = (GroupPermission)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (!ctx.GroupPermission.Any(a=> a.UserGroupId == user.UserGroupId && a.PermissionCode == user.PermissionCode))
                    {
                        ctx.GroupPermission.Add(user);
                    }
                    else
                    {
                        var currentGroupPermission = ctx.GroupPermission.Where(a => a.UserGroupId == user.UserGroupId && a.PermissionCode == user.PermissionCode).FirstOrDefault();
                        if (currentGroupPermission != null)
                        {
                            currentGroupPermission.UserGroupId = user.UserGroupId;
                            currentGroupPermission.PermissionCode = user.PermissionCode;
                            currentGroupPermission.UpdatedDate = DateTime.Now;
                            currentGroupPermission.Condition_Status = true;
                            currentGroupPermission.Deleted = false;
                        }
                        else
                            return new CommonResult(false, "GroupPermission Not Exist",null);

                    }
                    ctx.SaveChanges();

                    return new CommonResult(true, string.Empty, null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }

        public CommonResult ChangeStatus(int Id)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {

                    var currentGroupPermission = ctx.GroupPermission.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentGroupPermission != null)
                    {
                        currentGroupPermission.Condition_Status = false;
                    }
                    else
                        return new CommonResult(false, "GroupPermission Not Exist", null);


                    ctx.SaveChanges();

                    return new CommonResult(true, string.Empty, null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }

        public CommonResult ChangeGeneralStatus(int UserGroupId)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {

                    var currentGroupPermission = ctx.GroupPermission.Where(x => x.UserGroupId == UserGroupId).ToList();
                    if (currentGroupPermission != null)
                    {
                        foreach (var item in currentGroupPermission)
                            item.Condition_Status = false;
                    }
                    else
                        return new CommonResult(false, "GroupPermission not found", null);


                    ctx.SaveChanges();

                    return new CommonResult(true, string.Empty, null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }


        public CommonResult Delete(int Id)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {

                    var currentGroupPermission = ctx.GroupPermission.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentGroupPermission != null)
                    {
                        currentGroupPermission.Condition_Status = false;
                        currentGroupPermission.Deleted = true;
                    }
                    else
                        return new CommonResult(false, "GroupPermission Not Exist", null);


                    ctx.SaveChanges();

                    return new CommonResult(true, string.Empty, null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }

    }
}

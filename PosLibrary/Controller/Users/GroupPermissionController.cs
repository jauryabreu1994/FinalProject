using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using PosLibrary.Model.Entities.User;
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
                    var data_1 = line.Permission;
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

        public CommonResult Save(object data) 
        {
            try
            {
                GroupPermission user = (GroupPermission)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (user.Id == 0)
                    {
                        ctx.GroupPermission.Add(user);
                    }
                    else
                    {
                        var currentGroupPermission = ctx.GroupPermission.Where(x => x.Id == user.Id).FirstOrDefault();
                        if (currentGroupPermission != null)
                        {
                            currentGroupPermission.UserGroupId = user.UserGroupId;
                            currentGroupPermission.PermissionId = user.PermissionId;
                            currentGroupPermission.UpdatedDate = DateTime.Now;
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

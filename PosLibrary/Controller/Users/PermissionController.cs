using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using PosLibrary.Model.Entities.User;
using System;
using System.Linq;

namespace PosLibrary.Controller.Users
{
    public class PermissionController : IMainController
    {
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.Permission.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.GroupPermissions.ToList();

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
                    var lines = ctx.Permission.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                if (string.IsNullOrEmpty(filter))
                    return GetList();

                using (MainDbContext ctx = new MainDbContext())
                {
                    var lines = ctx.Permission.Where(a =>(a.Name.Contains(filter) || 
                                                   a.Code.Contains(filter)) &&  
                                                   !a.Deleted && a.Condition_Status).ToList();

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
                Permission user = (Permission)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (user.Id == 0)
                    {
                        ctx.Permission.Add(user);
                    }
                    else
                    {
                        var currentPermission = ctx.Permission.Where(x => x.Id == user.Id).FirstOrDefault();
                        if (currentPermission != null)
                        {
                            currentPermission.Name = user.Name;
                            currentPermission.Code = user.Code;
                            currentPermission.UpdatedDate = DateTime.Now;
                        }
                        else
                            return new CommonResult(false, "Permission Not Exist",null);

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

                    var currentPermission = ctx.Permission.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentPermission != null)
                    {
                        currentPermission.Condition_Status = false;
                    }
                    else
                        return new CommonResult(false, "Permission Not Exist", null);


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

                    var currentPermission = ctx.Permission.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentPermission != null)
                    {
                        currentPermission.Condition_Status = false;
                        currentPermission.Deleted = true;
                    }
                    else
                        return new CommonResult(false, "Permission Not Exist", null);


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

using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using PosLibrary.Model.Entities.User;
using System;
using System.Linq;

namespace PosLibrary.Controller.Users
{
    public class UserGroupController : IMainController
    {
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.UserGroup.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.Users.ToList();
                    var data_2 = line.GroupPermissions.ToList();

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
                    var lines = ctx.UserGroup.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.UserGroup.Where(a =>(a.Name.Contains(filter)) &&  
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
                UserGroup user = (UserGroup)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (user.Id == 0)
                    {
                        ctx.UserGroup.Add(user);
                    }
                    else
                    {
                        var currentUserGroup = ctx.UserGroup.Where(x => x.Id == user.Id).FirstOrDefault();
                        if (currentUserGroup != null)
                        {
                            currentUserGroup.Name = user.Name;
                            currentUserGroup.UpdatedDate = DateTime.Now;
                        }
                        else
                            return new CommonResult(false, "UserGroup Not Exist",null);

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

                    var currentUserGroup = ctx.UserGroup.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentUserGroup != null)
                    {
                        currentUserGroup.Condition_Status = false;
                    }
                    else
                        return new CommonResult(false, "UserGroup Not Exist", null);


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

                    var currentUserGroup = ctx.UserGroup.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentUserGroup != null)
                    {
                        currentUserGroup.Condition_Status = false;
                        currentUserGroup.Deleted = true;
                    }
                    else
                        return new CommonResult(false, "UserGroup Not Exist", null);


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

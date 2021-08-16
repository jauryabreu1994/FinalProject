using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using PosLibrary.Model.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HashLib;

namespace PosLibrary.Controller.Users
{
    public class UserController : IMainController
    {
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.User.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.UserGroup;

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
                    var lines = ctx.User.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.User.Where(a =>(a.FirstName.Contains(filter) || 
                                                   a.LastName.Contains(filter) || 
                                                   a.UserId.Contains(filter)) &&  
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
                User user = (User)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (user.Id == 0)
                    {
                        user.Password = EncryptPassword("12345");
                        ctx.User.Add(user);
                    }
                    else
                    {
                        var currentUser = ctx.User.Where(x => x.Id == user.Id).FirstOrDefault();
                        if (currentUser != null)
                        {
                            currentUser.FirstName = user.FirstName;
                            currentUser.LastName = user.LastName;
                            currentUser.Address = user.Address;
                            currentUser.UpdatedDate = DateTime.Now;
                            currentUser.Email = user.Email;
                            currentUser.Gender = user.Gender;
                            currentUser.Phone = user.Phone;
                            currentUser.VatNumber = user.VatNumber;
                            currentUser.UserGroupId = user.UserGroupId;
                        }
                        else
                            return new CommonResult(false, "User Not Exist",null);

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

                    var currentUser = ctx.User.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentUser != null)
                    {
                        currentUser.Condition_Status = false;
                    }
                    else
                        return new CommonResult(false, "User Not Exist", null);


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

                    var currentUser = ctx.User.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentUser != null)
                    {
                        currentUser.Condition_Status = false;
                        currentUser.Deleted = true;
                    }
                    else
                        return new CommonResult(false, "User Not Exist", null);


                    ctx.SaveChanges();

                    return new CommonResult(true, string.Empty, null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }


        public CommonResult LogIn(string userId, string password)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var user = ctx.User.Where(x => x.UserId == userId || x.Email == userId).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.Password == EncryptPassword(password)) 
                        {
                            return new CommonResult(true, string.Empty, user);
                        }
                        else
                            return new CommonResult(false, "Contraseña Incorrecta", null);
                    }
                    else
                        return new CommonResult(false, "Usuario o Correo no existe", null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }

        public CommonResult UpdatePassword(int Id, string currentPassword, string newPassword)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var user = ctx.User.Where(x => x.Id == Id).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.Password == EncryptPassword(currentPassword))
                        {
                            user.Password = EncryptPassword(newPassword);
                            ctx.SaveChanges();

                            return new CommonResult(true, string.Empty, user);
                        }
                        else
                            return new CommonResult(false, "Contraseña Incorrecta", null);
                    }
                    else
                        return new CommonResult(false, "Usuario no existe", null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }


        private string EncryptPassword(string password) 
        {
            IHash hash = HashFactory.Crypto.SHA3.CreateKeccak512();
            HashResult res = hash.ComputeString(password, Encoding.ASCII);
            return res.ToString();
        }
    }
}

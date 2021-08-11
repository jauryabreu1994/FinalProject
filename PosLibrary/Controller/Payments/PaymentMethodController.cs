using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Payments;

namespace PosLibrary.Controller.Items
{
    public class PaymentMethodController : IMainController
    {
        private string message = "Department not Exist";
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.PaymentMethod.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.TransactionPayments.ToList();
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
                    var lines = ctx.PaymentMethod.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.PaymentMethod.Where(a =>(a.Name.Contains(filter)) &&  
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
                PaymentMethod _data = (PaymentMethod)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (_data.Id == 0)
                    {
                        ctx.PaymentMethod.Add(_data);
                    }
                    else
                    {
                        var currentDepartment = ctx.PaymentMethod.Where(x => x.Id == _data.Id).FirstOrDefault();
                        if (currentDepartment != null)
                        {
                            currentDepartment.Name = _data.Name;
                            currentDepartment.UpdatedDate = DateTime.Now;
                        }
                        else
                            return new CommonResult(false, message,null);

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

                    var currentDepartment = ctx.PaymentMethod.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentDepartment != null)
                    {
                        currentDepartment.Condition_Status = false;
                    }
                    else
                        return new CommonResult(false, message, null);


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

                    var currentDepartment = ctx.PaymentMethod.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentDepartment != null)
                    {
                        currentDepartment.Condition_Status = false;
                        currentDepartment.Deleted = true;
                    }
                    else
                        return new CommonResult(false, message, null);


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

using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Customers;

namespace PosLibrary.Controller.Customers
{
    public class CustomerController : IMainController
    {
        private string message = "Customer not Exist";
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.Customer.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.TransactionHeaders.ToList();
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
                    var lines = ctx.Customer.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.Customer.Where(a =>(a.FirstName.Contains(filter) || 
                                                   a.LastName.Contains(filter) || 
                                                   a.CustomerId.Contains(filter)) &&  
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
                Customer _data = (Customer)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (_data.Id == 0)
                    {
                        ctx.Customer.Add(_data);
                    }
                    else
                    {
                        var currentCustomer = ctx.Customer.Where(x => x.Id == _data.Id).FirstOrDefault();
                        if (currentCustomer != null)
                        {
                            currentCustomer.Address = _data.Address;
                            currentCustomer.CompanyName = _data.CompanyName;
                            currentCustomer.DateBorn = _data.DateBorn;
                            currentCustomer.Email = _data.Email;
                            currentCustomer.FirstName = _data.FirstName;
                            currentCustomer.LastName = _data.LastName;
                            currentCustomer.Phone = _data.Phone;
                            currentCustomer.VatNumber = _data.VatNumber;
                            currentCustomer.UpdatedDate = DateTime.Now;
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

                    var currentCustomer = ctx.Customer.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentCustomer != null)
                    {
                        currentCustomer.Condition_Status = false;
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

                    var currentCustomer = ctx.Customer.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentCustomer != null)
                    {
                        currentCustomer.Condition_Status = false;
                        currentCustomer.Deleted = true;
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

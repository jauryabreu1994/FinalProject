using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Vendors;

namespace PosLibrary.Controller.Vendors
{
    public class VendorController : IMainController
    {
        private string message = "Vendor not Exist";
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.Vendor.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.Items.ToList();
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
                    var lines = ctx.Vendor.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.Vendor.Where(a =>(a.FirstName.Contains(filter) || 
                                                   a.LastName.Contains(filter) || 
                                                   a.VendorId.Contains(filter)) &&  
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
                Vendor _data = (Vendor)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (_data.Id == 0)
                    {
                        ctx.Vendor.Add(_data);
                    }
                    else
                    {
                        var currentVendor = ctx.Vendor.Where(x => x.Id == _data.Id).FirstOrDefault();
                        if (currentVendor != null)
                        {
                            currentVendor.FirstName = _data.FirstName;
                            currentVendor.LastName = _data.LastName;
                            currentVendor.Address = _data.Address;
                            currentVendor.UpdatedDate = DateTime.Now;
                            currentVendor.CompanyName = _data.CompanyName;
                            currentVendor.Phone = _data.Phone;
                            currentVendor.VatNumber = _data.Phone;
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

                    var currentVendor = ctx.Vendor.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentVendor != null)
                    {
                        currentVendor.Condition_Status = false;
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

                    var currentVendor = ctx.Vendor.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentVendor != null)
                    {
                        currentVendor.Condition_Status = false;
                        currentVendor.Deleted = true;
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

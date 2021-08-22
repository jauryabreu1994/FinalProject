using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Items;
using System.Data.Entity;

namespace PosLibrary.Controller.Items
{
    public class ItemController : IMainController
    {
        private string message = "Item not Exist";
        public CommonResult Get(int Id)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.Item.Where(x => x.Id == Id)
                                       .Include(v=>v.Vendor)
                                       .Include(id => id.ItemDepartment)
                                       .Include(id => id.ItemDiscount)
                                       .Include(it => it.ItemTax)
                                       .FirstOrDefault();
                    //var data_1 = line.Vendor;
                    //var data_2 = line.ItemDepartment;
                    //var data_3 = line.ItemDiscount;
                    //var data_4 = line.ItemTax;
                    return new CommonResult(true, string.Empty, line);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }

        public CommonResult Get(string sku)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.Item.Where(x => x.Sku == sku).FirstOrDefault();
                    var data_1 = line.Vendor;
                    var data_2 = line.ItemDepartment;
                    var data_3 = line.ItemDiscount;
                    var data_4 = line.ItemTax;
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
                    var lines = ctx.Item.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.Item.Where(a =>(a.Name.Contains(filter) || 
                                                   a.Sku.Contains(filter)) &&  
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
                Item _data = (Item)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (_data.Id == 0)
                    {
                        ctx.Item.Add(_data);
                    }
                    else
                    {
                        var currentItem = ctx.Item.Where(x => x.Id == _data.Id).FirstOrDefault();
                        if (currentItem != null)
                        {
                            currentItem.Sku = _data.Sku;
                            currentItem.Name = _data.Name;
                            currentItem.Price = _data.Price;
                            currentItem.UpdatedDate = DateTime.Now;
                            currentItem.VendorId = _data.VendorId;
                            currentItem.ItemTaxId = _data.ItemTaxId;
                            currentItem.ItemDiscountId = _data.ItemDiscountId;
                            currentItem.ItemDepartmentId = _data.ItemDepartmentId;
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

                    var currentItem = ctx.Item.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentItem != null)
                    {
                        currentItem.Condition_Status = false;
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

                    var currentItem = ctx.Item.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentItem != null)
                    {
                        currentItem.Condition_Status = false;
                        currentItem.Deleted = true;
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

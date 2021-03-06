using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.StoreSetting;
using PosLibrary.Model.Entities.Enums;

namespace PosLibrary.Controller.StoreSetting
{
    public class StoreController : IMainController
    {
        private string message = "Store not Exist";
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = Id != 0 ?
                            ctx.Store.Where(x => x.Id == Id).FirstOrDefault() :
                            ctx.Store.FirstOrDefault();
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
                    var lines = ctx.Store.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.Store.Where(a =>(a.Name.Contains(filter) || 
                                                   a.Phone.Contains(filter) ||
                                                   a.VatNumber.Contains(filter) ||
                                                   a.CompanyName.Contains(filter)) &&  
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
                Store _data = (Store)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (_data.Id == 0)
                    {
                        ctx.Store.Add(_data);
                    }
                    else
                    {
                        var currentStore = ctx.Store.Where(x => x.Id == _data.Id).FirstOrDefault();
                        if (currentStore != null)
                        {
                            currentStore.Address = _data.Address;
                            currentStore.CompanyName = _data.CompanyName;
                            currentStore.Email = _data.Email;
                            currentStore.Name = _data.Name;
                            currentStore.Phone = _data.Phone;
                            currentStore.VatNumber = _data.VatNumber;
                            currentStore.UpdatedDate = DateTime.Now;
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

                    var currentStore = ctx.Store.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentStore != null)
                    {
                        currentStore.Condition_Status = false;
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

                    var currentStore = ctx.Store.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentStore != null)
                    {
                        currentStore.Condition_Status = false;
                        currentStore.Deleted = true;
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

        public CommonResult GenerateId(StoreTableType type) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    Store store = ctx.Store.FirstOrDefault();
                    string valueToReturn = string.Empty;

                    switch (type)
                    {
                        case StoreTableType.Customer:
                            valueToReturn = String.Format("C{0:000000000}", store.CustomerId);
                            store.CustomerId++;
                            break;
                        case StoreTableType.Receipt:
                            valueToReturn = String.Format("T{0:000000000}", store.ReceiptId);
                            store.ReceiptId++;
                            break;
                        case StoreTableType.Vendor:
                            valueToReturn = String.Format("V{0:000000000}", store.VendorId);
                            store.VendorId++;
                            break;
                    }

                    ctx.SaveChanges();
                    return new CommonResult(true, string.Empty, valueToReturn);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }
    }
}

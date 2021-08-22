using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Transactions;
using System.Collections.Generic;
using System.Data.Entity;

namespace PosLibrary.Controller.Transactions
{
    public class TransactionLinesController : IMainController
    {
        private string message = "Transaction Lines not Exist";
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.TransactionLines.Where(x => x.Id == Id)
                                    .Include(a => a.Item)
                                    .Include(a => a.TransactionHeader)
                                    .FirstOrDefault();
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
                    var lines = ctx.TransactionLines.
                        Where(a => !a.Deleted && a.Condition_Status)
                                    .Include(a => a.Item)
                                    .Include(a => a.TransactionHeader)
                                    .ToList();

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
                    var lines = ctx.TransactionLines.Where(a =>(a.ReceiptId.Contains(filter)) &&  
                                                   !a.Deleted && a.Condition_Status)
                                    .Include(a => a.Item)
                                    .Include(a => a.TransactionHeader)
                                    .ToList();

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
                TransactionLines _data = (TransactionLines)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    _data.Id = 0;
                    ctx.TransactionLines.Add(_data);
                    ctx.SaveChanges();

                    return new CommonResult(true, string.Empty, null);
                }
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message, null);
            }
        }

        public CommonResult SaveList(object data)
        {
            try
            {
                var collection = data as List<TransactionLines>;

                using (MainDbContext ctx = new MainDbContext())
                {
                    foreach (var item in collection)
                    {
                        item.Id = 0;
                        item.TransactionHeader = null;
                        item.Item = null;
                        ctx.TransactionLines.Add(item);
                    }
                    ctx.SaveChanges();
                    return new CommonResult(true, string.Empty, collection);
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

                    var currentTransactionLines = ctx.TransactionLines.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentTransactionLines != null)
                    {
                        currentTransactionLines.Condition_Status = false;
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

                    var currentTransactionLines = ctx.TransactionLines.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentTransactionLines != null)
                    {
                        currentTransactionLines.Condition_Status = false;
                        currentTransactionLines.Deleted = true;
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

using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Transactions;
using System.Data.Entity;

namespace PosLibrary.Controller.Transactions
{
    public class TransactionHeaderController : IMainController
    {
        private string message = "Transaction Header not Exist";
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.TransactionHeader.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.TransactionLines.ToList();
                    var data_2 = line.TransactionPayments.ToList();
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
                    var lines = ctx.TransactionHeader.Where(a => !a.Deleted && 
                                                                  a.Condition_Status)
                                                        .Include(a=> a.Customer)
                                                        .Include(a => a.TransactionLines)
                                                        .Include(a => a.TransactionPayments)
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
                    var lines = ctx.TransactionHeader.Where(a =>(a.ReceiptId.Contains(filter)) &&  
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
                TransactionHeader _data = (TransactionHeader)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    _data.Id = 0;
                    _data.Customer = null;
                    _data.TransactionLines = null;
                    _data.TransactionPayments = null;
                    ctx.TransactionHeader.Add(_data);
                    ctx.SaveChanges();

                    return new CommonResult(true, string.Empty, _data);
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

                    var currentTransactionHeader = ctx.TransactionHeader.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentTransactionHeader != null)
                    {
                        currentTransactionHeader.Condition_Status = false;
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

                    var currentTransactionHeader = ctx.TransactionHeader.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentTransactionHeader != null)
                    {
                        currentTransactionHeader.Condition_Status = false;
                        currentTransactionHeader.Deleted = true;
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

using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Transactions;
using System.Collections.Generic;
using System.Data.Entity;

namespace PosLibrary.Controller.Transactions
{
    public class TransactionPaymentsController : IMainController
    {
        private string message = "Transaction Payments not Exist";
        public CommonResult Get(int Id) 
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.TransactionPayments.Where(x => x.Id == Id)
                                    .Include(a => a.PaymentMethod)
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
                    var lines = ctx.TransactionPayments.Where(a => !a.Deleted && a.Condition_Status)
                                    .Include(a => a.PaymentMethod)
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
                    var lines = ctx.TransactionPayments.Where(a =>(a.ReceiptId.Contains(filter)) &&  
                                                   !a.Deleted && a.Condition_Status)
                                    .Include(a => a.PaymentMethod)
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
                TransactionPayments _data = (TransactionPayments)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    _data.Id = 0;
                    ctx.TransactionPayments.Add(_data);
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
                var collection = data as List<TransactionPayments>;

                using (MainDbContext ctx = new MainDbContext())
                {
                    foreach (var pay in collection)
                    {
                        pay.Id = 0;
                        pay.TransactionHeader = null;
                        pay.PaymentMethod = null;
                        ctx.TransactionPayments.Add(pay);
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

                    var currentTransactionPayments = ctx.TransactionPayments.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentTransactionPayments != null)
                    {
                        currentTransactionPayments.Condition_Status = false;
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

                    var currentTransactionPayments = ctx.TransactionPayments.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentTransactionPayments != null)
                    {
                        currentTransactionPayments.Condition_Status = false;
                        currentTransactionPayments.Deleted = true;
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

using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Fiscal;

namespace PosLibrary.Controller.Fiscal
{
    public class NcfTypeController : IMainController
    {
        private string message = "Ncf Sequence Detail not Exist";
        public CommonResult Get(int Id)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.NcfType.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.NcfSequenceDetails.ToList();
                    var data_2 = line.NcfHistories.ToList();
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
                    var lines = ctx.NcfType.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.NcfType.Where(a =>(a.Description.Contains(filter)) &&  
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
                NcfType _data = (NcfType)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (_data.Id == 0)
                    {
                        ctx.NcfType.Add(_data);
                    }
                    else
                    {
                        var currentNcfType = ctx.NcfType.Where(x => x.Id == _data.Id).FirstOrDefault();
                        if (currentNcfType != null)
                        {
                            currentNcfType.Description = _data.Description;
                            currentNcfType.NcfId = _data.NcfId;
                            currentNcfType.IsDefaultSale = _data.IsDefaultSale;
                            currentNcfType.IsDefaultCreditMemo = _data.IsDefaultCreditMemo;
                            currentNcfType.UpdatedDate = DateTime.Now;
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

                    var currentNcfType = ctx.NcfType.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentNcfType != null)
                    {
                        currentNcfType.Condition_Status = false;
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

                    var currentNcfType = ctx.NcfType.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentNcfType != null)
                    {
                        currentNcfType.Condition_Status = false;
                        currentNcfType.Deleted = true;
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

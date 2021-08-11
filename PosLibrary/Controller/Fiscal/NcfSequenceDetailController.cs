using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Fiscal;

namespace PosLibrary.Controller.Fiscal
{
    public class NcfSequenceDetailController : IMainController
    {
        private string message = "Ncf Sequence Detail not Exist";
        public CommonResult Get(int Id)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var line = ctx.NcfSequenceDetail.Where(x => x.Id == Id).FirstOrDefault();
                    var data_1 = line.NcfType;
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
                    var lines = ctx.NcfSequenceDetail.Where(a => !a.Deleted && a.Condition_Status).ToList();

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
                    var lines = ctx.NcfSequenceDetail.Where(a =>(a.DGIIDescription.Contains(filter)) &&  
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
                NcfSequenceDetail _data = (NcfSequenceDetail)data;

                using (MainDbContext ctx = new MainDbContext())
                {
                    if (_data.Id == 0)
                    {
                        ctx.NcfSequenceDetail.Add(_data);
                    }
                    else
                    {
                        var currentNcfSequenceDetail = ctx.NcfSequenceDetail.Where(x => x.Id == _data.Id).FirstOrDefault();
                        if (currentNcfSequenceDetail != null)
                        {
                            currentNcfSequenceDetail.DateEnd = _data.DateEnd;
                            currentNcfSequenceDetail.DateStart = _data.DateStart;
                            currentNcfSequenceDetail.DGIIDescription = _data.DGIIDescription;
                            currentNcfSequenceDetail.UpdatedDate = DateTime.Now;
                            currentNcfSequenceDetail.NcfId = _data.NcfId;
                            currentNcfSequenceDetail.SeqEnd = _data.SeqEnd;
                            currentNcfSequenceDetail.SeqNext = _data.SeqNext;
                            currentNcfSequenceDetail.SeqStart = _data.SeqStart;
                            currentNcfSequenceDetail.SeqStatus = _data.SeqStatus;
                            currentNcfSequenceDetail.Serie = _data.Serie;
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

                    var currentNcfSequenceDetail = ctx.NcfSequenceDetail.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentNcfSequenceDetail != null)
                    {
                        currentNcfSequenceDetail.Condition_Status = false;
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

                    var currentNcfSequenceDetail = ctx.NcfSequenceDetail.Where(x => x.Id == Id).FirstOrDefault();
                    if (currentNcfSequenceDetail != null)
                    {
                        currentNcfSequenceDetail.Condition_Status = false;
                        currentNcfSequenceDetail.Deleted = true;
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

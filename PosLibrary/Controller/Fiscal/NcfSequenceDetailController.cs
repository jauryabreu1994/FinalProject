using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using System;
using System.Linq;
using PosLibrary.Model.Entities.Fiscal;
using System.Data.SqlClient;
using System.Collections.Generic;

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

        public CommonResult GetList(int ncfId)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {
                    var lines = ctx.NcfSequenceDetail.Where(a => a.NcfId == ncfId &&
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

        public NcfSequenceDetail GetNcfStatus(int ncfId, int process)
        {
            try
            {
                using (MainDbContext ctx = new MainDbContext())
                {

                    //var parameters = new List<SqlParameter>();
                    //parameters.Add(new SqlParameter("@_NCFID", ncfId));
                    //parameters.Add(new SqlParameter("@_PROCESS", process));
                    //var lines = ctx.Database.SqlQuery<NcfSequenceDetail>(" Exec usp_ApplyNCFToUse @_NCFID, @_PROCESS", parameters);
                    string query = string.Format("Exec usp_ApplyNCFToUse {0}, {1}", ncfId, process);
                    var lines = ctx.Database.SqlQuery<NcfSequenceDetail>(query);

                    return lines.FirstOrDefault(); 
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return GetNcfStatus(ncfId, process);
            }


        }

        public string ValidateNCFSequence(int ncfId)
        {
            try
            {
                NcfSequenceDetail nCF = GetNcfStatus(ncfId, 0);
                if(nCF == null)
                    return "No tiene secuencia disponible de NCF...";

                else if (nCF.SeqStatus == 2)
                {
                    return "Ultima secuencia de NCF utilizada...";
                }
                else if (nCF.SeqStatus == 3)
                {
                    return "Fecha de secuencia  NCF vencida por rango de fecha...";
                }
                else if (string.IsNullOrEmpty(nCF.Serie))
                {
                    return "No tiene secuencia disponible de NCF...";
                }
                else if (nCF.SeqEnd < nCF.SeqNext)
                {
                    return "Ultimo NCF utilizado - Debe agregar una sequencia de NCF (" + nCF.DGIIDescription + "): \n"
                                        + nCF.SeqNext + " de " + nCF.SeqEnd + " - Caja. ";
                }
                else if (ValidateNCFInUse(nCF.SeqNext, nCF.SeqEnd))
                {
                    return "Debe agregar una sequencia de NCF - Sencuencias NCF disponibles ("
                                   + nCF.DGIIDescription + "): " + Math.Abs(nCF.SeqEnd - nCF.SeqNext) + " Disponibles. ";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return "No tiene secuencia disponible de NCF...";
            }
        }

        private bool ValidateNCFInUse(int InUse, int Top)
        {
            int StartAlet = 100;
            int IntervalAlert = 10;

            if (Top - StartAlet == InUse)
                return true;
            else if (Top - StartAlet < InUse)
            {
                int intervalo = StartAlet / IntervalAlert;

                for (int i = 0; i <= intervalo; i++)
                {
                    if ((i * IntervalAlert) == (Top - InUse))
                        return true;
                }
            }
            return false;
        }
    }
}

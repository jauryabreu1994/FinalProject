using PosLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosLibrary.Controller
{
    public interface IMainController
    {
        CommonResult Get(int Id);
        CommonResult GetList();
        CommonResult GetList(string filter);
        CommonResult Save(object data);
        CommonResult ChangeStatus(int Id);
        CommonResult Delete(int Id);
    }
}

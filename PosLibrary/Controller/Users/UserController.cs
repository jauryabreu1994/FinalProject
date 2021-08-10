using PosLibrary.Model.Context;
using PosLibrary.Model.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosLibrary.Controller.Users
{
    public class UserController
    {
        public User Get() 
        {
            using (MainDbContext ctx = new MainDbContext())
            {
                var line = ctx.User.FirstOrDefault();
                var u = line.UserGroup.Name;
                //var list = new GenerateReport().GetCustomerBirthDay(_webHostEnvironment, ctx);
                return line;
            }
        }
    }
}

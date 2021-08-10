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
        public User Get(int ID) 
        {
            using (MainDbContext ctx = new MainDbContext())
            {
                var line = ctx.User.Where(x => x.Id == ID).FirstOrDefault();
                var a = line.UserGroup;
                
                return line;
            }
        }

        public List<User> Getlist(int ID) {

            using (MainDbContext ctx = new MainDbContext()) {

                var lines = ctx.User.Where(a => !a.Deleted && a.Condition_Status).ToList();

                return lines;


            }

        }
    }
}

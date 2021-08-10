using PosLibrary.Model.Context;
using PosLibrary.Model.Entities;
using PosLibrary.Model.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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

        public OperationResult Update (int ID)
        {

            using (MainDbContext ctx = new MainDbContext())
            {

                ctx.Entry(ID).State = EntityState.Modified;
                
                ctx.SaveChanges();

                return new OperationResult() { Success = true};
            }

        }

        public User Create(User user) 
        {
            using (MainDbContext ctx = new MainDbContext())
            {
                ctx.Entry(user).State = EntityState.Added;

                return user;
            }
        }

        public OperationResult Delete(User user)
        {
            using (MainDbContext ctx = new MainDbContext())
            {
                ctx.User.Remove(user);

                ctx.SaveChanges();

                return new OperationResult() { Success = true };

            }
        }

    }
}

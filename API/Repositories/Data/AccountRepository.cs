using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class AccountRepository
    {
        MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public UserRole Login(string email, string password)
        {
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Employee)
                .SingleOrDefault(x => x.User.Employee.Email.Equals(email) && x.User.Password.Equals(password));
            return data;
        }

        public int Register(Register register)
        {
            // preparation default role
            var level = myContext.Levels.Min(x => x.Value);
            var roleDefault = myContext.Roles.SingleOrDefault(x => x.Level.Value.Equals(level)).Id;

            // check duplicate
            var checking = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email));
            if(checking != null)
            {
                return 0;
            }

            Employee employee = new Employee(register.FirstName, register.LastName, register.Email, register.PhoneNumber, register.HireDate, register.Salary);

            // insert data employee to database
            myContext.Employees.Add(employee);
            var registeringEmployee = myContext.SaveChanges();

            // if inserted
            if(registeringEmployee > 0)
            {
                // preparation assigning role
                var registeredEmployee = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email)).Id;

                User user = new User()
                {
                    Id = registeredEmployee,
                    Username = register.Username,
                    Password = register.Password
                };

                //myContext.Users.Add(user);
                //var registeringUser = myContext.SaveChanges();
                #region If
                //if (registeringUser > 0)
                //{
                //    UserRole userRole = new UserRole();
                //    userRole.RoleId = roleDefault;
                //    userRole.UserId = registeredEmployee;

                //    // assigning role
                //    myContext.UserRoles.Add(userRole);
                //    var assigningRole = myContext.SaveChanges();
                //    if (assigningRole > 0)
                //    {
                //        return assigningRole;
                //    }
                //}
                //else
                //{
                //    // if assigning role have a problem 
                //    var dataEmployee = myContext.Employees.Find(registeredEmployee);
                //    myContext.Employees.Remove(dataEmployee);
                //    var deletingEmployee = myContext.SaveChanges();
                //}
                #endregion If

                #region trycatch
                try
                {
                    myContext.Users.Add(user);
                    var registeringUser = myContext.SaveChanges();

                    if (registeringUser > 0)
                    {
                        UserRole userRole = new UserRole();
                        userRole.RoleId = roleDefault;
                        userRole.UserId = registeredEmployee;

                        // assigning role
                        myContext.UserRoles.Add(userRole);
                        var assigningRole = myContext.SaveChanges();
                        if (assigningRole > 0)
                        {
                            return assigningRole;
                        }
                    }
                }
                catch(Exception ex)
                {
                    // if assigning role have a problem 
                    var dataEmployee = myContext.Employees.Find(registeredEmployee);
                    myContext.Employees.Remove(dataEmployee);
                    var deletingEmployee = myContext.SaveChanges();
                }
                #endregion trycatch
            }
            return 0;
        }

        public int ChangePassword(string email, string oldPassword, string newPassword)
        {
            var data = myContext.Users.Include(x => x.Employee).SingleOrDefault(x => x.Employee.Email.Equals(email));
            if(data == null)
            {
                return 0;
            }
            if(data.Password == oldPassword)
            {
                data.Password = newPassword;
            }
            myContext.Entry(data).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                return result;
            return 0;
        }

        public int ForgotPassword(string email)
        {
            var data = myContext.Users.Include(x => x.Employee).SingleOrDefault(x => x.Employee.Email.Equals(email));
            if(data == null)
            {
                return 0;
            }
            data.Password = "M3tr0dat4";
            myContext.Entry(data).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                return result;
            return 0;
        }
    }
}

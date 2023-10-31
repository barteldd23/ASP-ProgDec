using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot log in with these credentials. Your IP Address has been saved.")
        {

        }
        public LoginFailureException(string message) : base(message)
        {

        }

    }


    public static class UserManager
    {
        public static string GetHash(string password)
        {
            using(var hasher = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public static int DeleteAll()
        {
            try
            {
                using(ProgDecEntities dc = new ProgDecEntities()) 
                { 
                    dc.tblUsers.RemoveRange(dc.tblUsers.ToList());
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(User user, bool rollback = false)
        {
            int results;
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //for rollback on ut files
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser entity = new tblUser();
                    entity.Id = dc.tblUsers.Any() ? dc.tblUsers.Max(s => s.Id) + 1 : 1;  // get last ID in table and add 1, or set Id to 1 because there are no Values in the table.
                    entity.FirstName = user.FirstName;
                    entity.LastName = user.LastName;
                    entity.UserId = user.UserId;
                    entity.Password = GetHash(user.Password);


                    // IMPORTANT - BACK FILL THE ID
                    user.Id = entity.Id; //do this because the first Insert is id by reference


                    //Add entity to database
                    dc.tblUsers.Add(entity);
                    // commit the changes
                    results = dc.SaveChanges();

                    //only for ut files
                    if (rollback) transaction.Rollback();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }

        public static bool Login(User user)
        {
            try
            {
                if(!string.IsNullOrEmpty(user.UserId))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using(ProgDecEntities dc = new ProgDecEntities())
                        {
                            tblUser tblUser = dc.tblUsers.FirstOrDefault(u => u.UserId == user.UserId);
                            if(tblUser != null)
                            {
                                if(tblUser.Password == GetHash(user.Password))
                                {
                                    // Login successful
                                    //back fill user datat to the 'user' object that will be the session variable
                                    user.Id = tblUser.Id;
                                    user.FirstName = tblUser.FirstName;
                                    user.LastName = tblUser.LastName;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                }
                            }
                            else
                            {
                                throw new LoginFailureException("UserId was not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new LoginFailureException("Password was not set.");
                    }
                }
                else
                {
                    throw new LoginFailureException("UserId was not set.");
                }
            }
            catch (LoginFailureException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Seed()
        {
            using(ProgDecEntities dc = new ProgDecEntities())
            {
                if( !dc.tblUsers.Any())
                {
                    User user = new User()
                    {
                        UserId = "DBartel",
                        FirstName = "Dean",
                        LastName = "Bartel",
                        Password = "password"
                    };
                    Insert(user);

                    user = new User()
                    {
                        UserId = "bfoote",
                        FirstName = "Brian",
                        LastName = "Foote",
                        Password = "maple"
                    };
                    Insert(user);

                }
            }
        }
    }
}

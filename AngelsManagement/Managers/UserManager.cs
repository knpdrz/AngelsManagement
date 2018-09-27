using AngelsManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AngelsManagement.Globals;

namespace AngelsManagement.Managers
{
    public class UserManager
    {
        public void PrepareDatabase()
        {
            //create app data directory (do nothing if it already exists)
            Directory.CreateDirectory(appDataFolderPath);

            //perform first migration for users data
            using (UsersContext context = new UsersContext())
            {
                context.Database.Migrate();

            }
        }

        public void CreateUser(string login, string password)
        {
            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            //hashing password with appended salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password+mySalt);

            UserCredentials userCredentials = new UserCredentials
            {
                Salt = mySalt,
                HashedPassword = hashedPassword,
                Login = login
            };

            using (var ctx = new UsersContext())
            {
                ctx.UserCredentials.Add(userCredentials);
                ctx.SaveChanges();
            }
        }

        public bool CanUserLogin(string login, string submittedPassword)
        {
            //get user salt and password
            UserCredentials userCredentials = GetUserCredentialsFromDb(login);

            //user with such login doesn't exist in the db
            if (userCredentials == null) return false;

            string salt = userCredentials.Salt;
            string hashedPassword = userCredentials.HashedPassword;

            bool passwordValid = BCrypt.Net.BCrypt.Verify(submittedPassword + salt, hashedPassword);
            return passwordValid;
        }

        private UserCredentials GetUserCredentialsFromDb(string login)
        {
            UserCredentials userCredentials = null;
            using (var ctx = new UsersContext())
            {
                userCredentials = ctx.UserCredentials.FirstOrDefault(uc => uc.Login.Equals(login));
            }

            return userCredentials;
        }

    }
}

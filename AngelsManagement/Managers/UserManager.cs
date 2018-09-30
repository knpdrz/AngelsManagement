using AngelsManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using static AngelsManagement.Globals;

namespace AngelsManagement.Managers
{
    public static class UserManager
    {
        public static void EnsureAdminExists()
        {
            //if admins' account doesn't exist- create it with default password
            if (!LoginExistsInDb(AdminUsername)){
                CreateUser(AdminUsername, AdminDefaultPassword);
            }
        }

        public static void PrepareDatabase()
        {
            //create app data directory (do nothing if it already exists)
            Directory.CreateDirectory(appDataFolderPath);

            //perform first migration for users data
            using (UsersContext context = new UsersContext())
            {
                context.Database.Migrate();
            }
        }

        public static void CreateUser(string login, string password)
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

        public static void ChangeUserPassword(string login, string newPassword)
        {
            string newSalt = BCrypt.Net.BCrypt.GenerateSalt();
            //hashing password with appended salt
            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword + newSalt);

            using (var ctx = new UsersContext())
            {
                UserCredentials newUserCredentials = 
                    ctx.UserCredentials.SingleOrDefault(uc => uc.Login.Equals(login));
                newUserCredentials.Salt = newSalt;
                newUserCredentials.HashedPassword = newHashedPassword;

                ctx.UserCredentials.Update(newUserCredentials);
                ctx.SaveChanges();
            }
        }

        public static bool CanUserLogin(string login, string submittedPassword)
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

        private static UserCredentials GetUserCredentialsFromDb(string login)
        {
            UserCredentials userCredentials = null;
            using (var ctx = new UsersContext())
            {
                userCredentials = ctx.UserCredentials.FirstOrDefault(uc => uc.Login.Equals(login));
            }

            return userCredentials;
        }

        public static bool LoginExistsInDb(string login)
        {
            bool loginExists = false;

            using (var ctx = new UsersContext())
            {
                var userCredentials = ctx.UserCredentials.FirstOrDefault(uc => uc.Login.Equals(login));
                if(userCredentials != null)
                {
                    loginExists = true;
                }
            }

            return loginExists;
        }

        public static bool IsUserAdmin(string login)
        {
            return (login.Equals(AdminUsername));
        }
    }
}

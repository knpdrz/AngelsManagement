using System;

namespace AngelsManagement.Model
{
    public class UserCredentials
    {
        public Int64 UserCredentialsId { get; set; }
        public string Login { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public UserCredentials() { }
        
        public UserCredentials(string login, string salt, string hashedPassword)
        {
            Login = login;
            Salt = salt;
            HashedPassword = hashedPassword;
        }
    }
}
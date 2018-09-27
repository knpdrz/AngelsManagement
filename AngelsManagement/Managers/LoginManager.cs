using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AngelsManagement.Globals;
namespace AngelsManagement.Managers
{
    public class LoginManager
    {
        private readonly string Login;
        private readonly string Password;

        private List<String> ValidationErrorReason = new List<string>();
        public bool ValidationOk;

        public LoginManager(string login, string password)
        {
            Login = login;
            Password = password;

            ValidationErrorReason = new List<string>();
            ValidationOk = false;

            ValidateUser();
        }

        private void ValidateUser()
        {
            if (String.IsNullOrEmpty(Login))
            {

                ValidationErrorReason.Add(EmptyLoginErrorText);
                return;
            }

            if (String.IsNullOrEmpty(Password))
            {
                ValidationErrorReason.Add(EmptyPasswordErrorText);
                return;
            }

            if (CheckLoginPasswordMatch())
            {
                ValidationOk = true;
            }
            else
            {
                ValidationErrorReason.Add(WrongLoginOrPasswordErrorText);
            }
        }

        private bool CheckLoginPasswordMatch()
        {
            return (Login.Equals(Password));
        }

        public string GetValidationErrorString()
        {
            return String.Join("\n", ValidationErrorReason.ToArray());
        }
    }
}

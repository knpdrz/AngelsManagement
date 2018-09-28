using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AngelsManagement.Globals;
namespace AngelsManagement.Managers
{
    public class UserCredentialsValidationManager
    {
        private readonly string Login;
        private readonly string Password;

        private List<String> ValidationErrorReason = new List<string>();

        public UserCredentialsValidationManager(string login, string password)
        {
            UserManager.PrepareDatabase();

            Login = login;
            Password = password;

            ValidationErrorReason = new List<string>();
        }

        private bool IsLoginValid()
        {
            return !(String.IsNullOrEmpty(Login));
        }

        private bool IsPasswordValid()
        {
            return !(String.IsNullOrEmpty(Password));
        }

        private bool DoLoginAndPasswordMatch()
        {
            return UserManager.CanUserLogin(Login, Password);
        }

        public string GetValidationErrorString()
        {
            return String.Join("\n", ValidationErrorReason.ToArray());
        }

        public bool AreCredentialsValidOnLogin()
        {
            ValidationErrorReason.Clear();

            if (!IsLoginValid())
            {
                ValidationErrorReason.Add(WrongLoginErrorText);
            }

            if (!IsPasswordValid())
            {
                ValidationErrorReason.Add(WrongPasswordErrorText);
            }

            if (!DoLoginAndPasswordMatch())
            {
                ValidationErrorReason.Clear();
                ValidationErrorReason.Add(WrongLoginOrPasswordErrorText);
            }

            return (ValidationErrorReason.Count == 0);

        }

        public bool AreCredentialsValidOnPasswordChange()
        {
            ValidationErrorReason.Clear();

            if (!IsLoginValid())
            {
                ValidationErrorReason.Add(WrongLoginErrorText);
            }

            if (!IsPasswordValid())
            {
                ValidationErrorReason.Add(WrongPasswordErrorText);
            }

            if (!UserManager.LoginExistsInDb(Login))
            {
                ValidationErrorReason.Clear();
                ValidationErrorReason.Add(UserDoesNotExistsErrorText);
            }

            return (ValidationErrorReason.Count == 0);
        }

        //validates user credentials on user creation
        public bool AreCredentialsValidOnSignUp()
        {
            ValidationErrorReason.Clear();

            if (!IsLoginValid())
            {
                ValidationErrorReason.Add(WrongLoginErrorText);
            }

            if (!IsPasswordValid())
            {
                ValidationErrorReason.Add(WrongPasswordErrorText);
            }

            if (UserManager.LoginExistsInDb(Login))
            {
                ValidationErrorReason.Clear();
                ValidationErrorReason.Add(UserAlreadyExistsErrorText);
            }
           
            return (ValidationErrorReason.Count == 0);
        }

    }
}

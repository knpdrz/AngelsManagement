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
        public bool ValidationOk;

        public bool IsNewUser { get; set; }//flag indicating that user doesn't exist yet (and we're trying to create them after this validation)

        public UserCredentialsValidationManager(string login, string password, bool isNewUser)
        {            
            Login = login;
            Password = password;
            IsNewUser = isNewUser;

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

            if (IsNewUser)
            {
                if (UserManager.LoginExistsInDb(Login))
                {
                    ValidationErrorReason.Clear();
                    ValidationErrorReason.Add(UserAlreadyExistsErrorText);
                }
                else
                {
                    ValidationOk = true;
                    ValidationErrorReason.Clear();
                }
                return;
            }
            else
            {
                if (CheckLoginPasswordMatch())
                {
                    ValidationOk = true;
                    ValidationErrorReason.Clear();
                }
                else
                {
                    ValidationErrorReason.Clear();
                    ValidationErrorReason.Add(WrongLoginOrPasswordErrorText);
                }
            }
        }

        private bool CheckLoginPasswordMatch()
        {
            return UserManager.CanUserLogin(Login, Password);
        }

        public string GetValidationErrorString()
        {
            return String.Join("\n", ValidationErrorReason.ToArray());
        }
        
    }
}

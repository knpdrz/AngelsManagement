using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AngelsManagement.Globals;
namespace AngelsManagement.Managers
{
    public static class ValidationManager
    {
        //validates first name or last name
        //criteria of a good name: 
        //has only letters,
        //is not null or empty
        public static bool IsNameValid(string name)
        {
            bool allGood = true;
            allGood &= !(String.IsNullOrEmpty(name));

            bool isDigitPresent = name.Any(c => char.IsDigit(c));
            allGood &= !isDigitPresent;

            return allGood;
        }

        //validates school name
        //if it's neither null nor empty
        public static bool IsSchoolNameValid(string name)
        {
            bool allGood = true;
            allGood &= !(String.IsNullOrEmpty(name));

            return allGood;
        }

        //and is neither empty nor null
        //todo some length check?
        public static bool IsPhoneNumberValid(string phone)
        {
            bool allGood = true;
            allGood &= !(String.IsNullOrEmpty(phone));

           // allGood &= !(Regex.Match(phone, @"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/").Success);
            return allGood;
        }

        //correct year is neither empty nor null
        //and is between MinPersonYear and MaxPersonYear
        public static bool IsBirthYearValid(string birthYear)
        {
            bool allGood = true;
            allGood &= !(String.IsNullOrEmpty(birthYear));

            Int64 parsedYear;
            allGood &= Int64.TryParse(birthYear, out parsedYear);

            if (allGood)
            {
                allGood &= (parsedYear >= MinPersonYear)
                    && (parsedYear <= MaxPersonYear);
            }
            return allGood;
        }

        //validates email
        //based on: https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        public static bool IsEmailValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsAddressValid(string address)
        {
            bool allGood = true;
            allGood &= !(String.IsNullOrEmpty(address));

            return allGood;
        }
    }
}

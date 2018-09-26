using AngelsManagement.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AngelsManagement.Globals;

namespace AngelsManagement.Model
{
    public class Guardian
    {
        public Int64 GuardianId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public String City { get; set; }

        public ICollection<StuPar> StudentGuardians { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Guardian other = (Guardian)obj;
            return (GuardianId == other.GuardianId);
        }

        public override int GetHashCode()
        {
            //todo maybe some better implementation?
            return (int)(GuardianId * Int32.Parse(PhoneNumber[0].ToString()));
        }

        //returns a list of string explanations of why were some/all 
        //input fields invalid (while getting data to create guardian object)
        public static List<String> FindGuardianValidationErrors(string firstName, 
            string lastName, string phoneNumber)
        {
            List<String> errorReason = new List<string>();

            if (!ValidationManager.IsNameValid(firstName))
            {
                errorReason.Add(FirstNameErrorText + firstName);
            }

            if (!ValidationManager.IsNameValid(lastName))
            {
                errorReason.Add(LastNameErrorText + lastName);
            }

            if (!ValidationManager.IsPhoneNumberValid(phoneNumber))
            {
                errorReason.Add(PhoneErrorText + phoneNumber);
            }

            return errorReason;
        }

        //constructor that takes user input and parses it accordingly
        public Guardian(string firstName,
            string lastName, string phoneNumber, string city)
        {
            //based on: https://stackoverflow.com/questions/4427483/how-to-lowercase-a-string-except-for-first-character-with-c-sharp
            FirstName = new String(firstName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            LastName = new String(lastName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());

            PhoneNumber = phoneNumber;

            City = city;//todo is it worth changing to enum?
        }

        public Guardian(long guardianId, string firstName,
            string lastName, string phoneNumber, string city)
        {
            GuardianId = guardianId;
            //based on: https://stackoverflow.com/questions/4427483/how-to-lowercase-a-string-except-for-first-character-with-c-sharp
            FirstName = new String(firstName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            LastName = new String(lastName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());

            PhoneNumber = phoneNumber;

            City = city;//todo is it worth changing to enum?
        }
    }
}

using AngelsManagement.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using static AngelsManagement.Globals;
namespace AngelsManagement.Model
{
    public class Volunteer
    {
        public Int64 VolunteerId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int64 BirthYear { get; set; }
        public String City { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }

        public ICollection<VolStu> VolunteerStudents { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Volunteer other = (Volunteer)obj;
            return (VolunteerId == other.VolunteerId);
        }

        public override int GetHashCode()
        {
            return (int)(VolunteerId * BirthYear);
        }

        //returns a list of string explanations of why were some/all 
        //input fields invalid (while getting data to create volunteer object)
        public static List<String> FindVolunteerValidationErrors(string firstName,
            string lastName, string birthYear, string address, string email)
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

            if (!ValidationManager.IsBirthYearValid(birthYear))
            {
                errorReason.Add(YearErrorText + birthYear);
            }

            if (!ValidationManager.IsEmailValid(email))
            {
                errorReason.Add(EmailErrorText + email);
            }

            if (!ValidationManager.IsAddressValid(address))
            {
                errorReason.Add(AddressErrorText + address);
            }
            
            return errorReason;
        }

        public Volunteer(long volunteerId, string firstName, string lastName, 
            long birthYear, string city, string address, string email)
        {
            VolunteerId = volunteerId;
            FirstName = firstName;
            LastName = lastName;
            BirthYear = birthYear;
            City = city;
            Address = address;
            Email = email;
        }

        public Volunteer(long volunteerId, string firstName, string lastName, 
            string birthYear, string city, string address, string email)
        {
            VolunteerId = volunteerId;
            //based on: https://stackoverflow.com/questions/4427483/how-to-lowercase-a-string-except-for-first-character-with-c-sharp
            FirstName = new String(firstName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            LastName = new String(lastName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            BirthYear = Int64.Parse(birthYear);
            City = city;//todo to enum?
            Address = address;
            Email = email;
        }

        //constructor that takes user input and parses it accordingly
        public Volunteer(string firstName,
            string lastName, string birthYear, string city, string address, string email)
        {
            //based on: https://stackoverflow.com/questions/4427483/how-to-lowercase-a-string-except-for-first-character-with-c-sharp
            FirstName = new String(firstName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            LastName = new String(lastName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            Email = email;
            BirthYear = Int64.Parse(birthYear);
            Address = address;
            City = city;//todo is it worth changing to enum?
        }
    }
}

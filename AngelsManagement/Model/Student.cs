using AngelsManagement.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AngelsManagement.Globals;

namespace AngelsManagement.Model
{
    public class Student
    {
        public Int64 StudentId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int64 BirthYear { get; set; }
        public String City { get; set; }
        public String School { get; set; }

        public ICollection<VolStu> VolunteerStudents { get; set; }
        public ICollection<StuPar> StudentGuardians { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Student other = (Student)obj;
            return (StudentId == other.StudentId);
        }

        public override int GetHashCode()
        {
            return (int)(StudentId*BirthYear);
        }

        //returns a list of string explanations of why were some/all 
        //input fields invalid (while getting data to create guardian object)
        public static List<String> FindStudentValidationErrors(string firstName,
            string lastName, string birthYear, string school)
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

            if (!ValidationManager.IsSchoolNameValid(school))
            {
                errorReason.Add(SchoolErrorText + school);
            }

            return errorReason;
        }

        public Student(long studentId, string firstName, string lastName, long birthYear, string city, string school)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            BirthYear = birthYear;
            City = city;
            School = school;
        }

        public Student(long studentId, string firstName, string lastName, string birthYear, string city, string school)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            BirthYear = Int64.Parse(birthYear);
            City = city;
            School = school;
        }

        //constructor that takes user input and parses it accordingly
        public Student(string firstName, string lastName, 
            string birthYear, string city, string school)
        {
            //based on: https://stackoverflow.com/questions/4427483/how-to-lowercase-a-string-except-for-first-character-with-c-sharp
            FirstName = new String(firstName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            LastName = new String(lastName.Select((ch, index) => (index == 0) ? ch : Char.ToLower(ch)).ToArray());
            BirthYear = Int64.Parse(birthYear);
            School = school;
            City = city;//todo is it worth changing to enum?
        }
    }
}

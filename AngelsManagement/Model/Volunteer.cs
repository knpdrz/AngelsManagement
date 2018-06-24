using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

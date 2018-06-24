using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ICollection<StuPar> StudentParents { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelsManagement.Model
{
    public class VolStu
    {
        public Int64 VolunteerId { get; set; }
        public Int64 StudentId { get; set; }

        public Volunteer Volunteer { get; set; }
        public Student Student { get; set; }
    }
}

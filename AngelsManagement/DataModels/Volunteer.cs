using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelsManagement.DataModels
{
    public class Volunteer
    {
        public Int64 ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int64 BirthYear { get; set; }
        public String City { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
    }
}

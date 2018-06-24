using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelsManagement.Model
{
    public class Parent
    {
        public Int64 ParentId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public String City { get; set; }

        public ICollection<StuPar> StudentParents { get; set; }

    }
}

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

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Parent other = (Parent)obj;
            return (ParentId == other.ParentId);
        }

        public override int GetHashCode()
        {
            //todo maybe some better implementation?
            return (int)(ParentId * Int32.Parse(PhoneNumber[0].ToString()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelsManagement.Model
{
    public class StuPar
    {
        public Int64 StudentId { get; set; }
        public Int64 GuardianId { get; set; }
        public Student Student { get; set; }
        public Guardian Guardian { get; set; }

    }
}

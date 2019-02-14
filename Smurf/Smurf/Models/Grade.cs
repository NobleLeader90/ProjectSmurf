using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smurf.Models
{
    public class Grade
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int EventID { get; set; }
        public int State { get; set; }
        public int SectionID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smurf.Models
{
    public class Section
    {
        public int ID { get; set; }
        public string CourseName { get; set; }
        public int Period { get; set; }
        public string TeacherName { get; set; }
    }
}

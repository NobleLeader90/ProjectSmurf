using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int Period { get; set; }
        public string TeacherName { get; set; }
        public List<Student> Roster { get; set; }
    }
}

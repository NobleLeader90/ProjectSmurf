using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public char Gender { get; set; }
        public int StudentId { get; set; }

        public int Period1 { get; set; }
        public int Period2 { get; set; }
        public int Period3 { get; set; }
        public int Period4 { get; set; }
        public int Period5 { get; set; }
        public int Period6 { get; set; }
    }
}

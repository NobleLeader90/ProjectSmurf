using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OperationSmurf.Models;

namespace OperationSmurf.ViewModels
{
    public class ClassroomGrid
    {
        static int MAX_STUDENTS = 35;
        static int MAX_EVENNTS = 50;
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public List<Models.Event> Columns { get; set; }
        public List<string> StudentNames { get; set; }
        public Models.Grade[,] GradeGrid   { get; set; }

        public ClassroomGrid(string courseName, string teacherName, List<Event> columns, List<string> studentNames, Grade[,] gradeGrid)
        {
            CourseName = courseName ?? throw new ArgumentNullException(nameof(courseName));
            TeacherName = teacherName ?? throw new ArgumentNullException(nameof(teacherName));
            Columns = columns ?? throw new ArgumentNullException(nameof(columns));
            StudentNames = studentNames ?? throw new ArgumentNullException(nameof(studentNames));
            GradeGrid = gradeGrid ?? throw new ArgumentNullException(nameof(gradeGrid));
        }

    }
}

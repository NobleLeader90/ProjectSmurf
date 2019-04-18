using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OperationSmurf.Models;

namespace OperationSmurf.ViewModels
{
    public class ClassroomGrid
    {
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public List<Models.Event> Columns { get; set; }
        public List<Student> Students { get; set; }
        public List<string> StudentNames { get; set; }
        public List<int> StudentIds { get; set; }
        public Models.Grade[,] GradeGrid   { get; set; }

       // public Student TargetStudent { get; set; }
       // public Event TargetEvent { get; set; }
       // public Grade TargetGrade { get; set; }

        public ClassroomGrid(string courseName, string teacherName, List<Event> columns, List<string> studentNames, List<int> studentIds, Grade[,] gradeGrid)
        {
            CourseName = courseName ?? throw new ArgumentNullException(nameof(courseName));
            TeacherName = teacherName ?? throw new ArgumentNullException(nameof(teacherName));
            Columns = columns ?? throw new ArgumentNullException(nameof(columns));
            StudentNames = studentNames ?? throw new ArgumentNullException(nameof(studentNames));
            StudentIds = studentIds;
            GradeGrid = gradeGrid ?? throw new ArgumentNullException(nameof(gradeGrid));
           // TargetEvent = new Event();
           // TargetStudent = new Student();
           // TargetGrade = new Grade();         
        }

        public ClassroomGrid()
        {
            CourseName = "";
            TeacherName = "";
            Columns = new List<Models.Event>();
            StudentNames = new List<string>();
            StudentIds = new List<int>();
            GradeGrid = new Models.Grade[50,50];  //need some constant parameters here for max size
            //TargetEvent = new Event();
            //TargetStudent = new Student();
            //TargetGrade = new Grade();
        }

    }
}

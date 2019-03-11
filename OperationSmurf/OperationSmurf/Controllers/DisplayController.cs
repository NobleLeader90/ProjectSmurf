using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OperationSmurf.Models;
using OperationSmurf.ViewModels;

namespace OperationSmurf.Controllers
{
    //public class DisplayController : Controller
    //{
    //    public ClassroomGrid bucket { get; set; }

    //    private readonly EventContext _Eventcontext;
    //    private readonly GradeContext _Gradecontext;
    //    private readonly SectionContext _Sectioncontext;
    //    private readonly StudentContext _Studentcontext;

    //    public DisplayController(EventContext context1, GradeContext context2, SectionContext context3, StudentContext context4 )
    //    {
    //        _Eventcontext = context1;
    //        _Gradecontext = context2;
    //        _Sectioncontext = context3;
    //        _Studentcontext = context4;

    //        bucket = new ClassroomGrid();

    //    }




    //    public IActionResult Index(int Id)
    //    {

    //        //Testing bucket fill of Student names with format build for output string insifr lambda of query!! :)
    //        var names = _Studentcontext.Student.OrderBy(a => a.LastName).Select(b => b.FirstName + " " + b.LastName).ToList();
    //        foreach(string name in names)
    //        {
    //            bucket.StudentNames.Add(name);
    //        }

    //        //Now experimenting with 2-deep table issue because we need a list of students as a roster in each section. 
    //        //First testing how to get roster list out if I manually add a roster while I work out the roster view issues
    //        //Manually building 2 students to inject as a test roster
    //        Student s1 = new Student();
    //        Student s2 = new Student();
    //        s1.FirstName = "Bob";
    //        s1.LastName = "Smith";
    //        s2.FirstName = "Alice";
    //        s2.LastName = "Jones";

    //        var section = _Sectioncontext.Section.FirstOrDefault(a => a.CourseName == "IDT");
    //        //section.Roster[0] = s1;
    //        //section.Roster[1] = s2;
    //        //foreach (Student student in section.Roster)
    //        //{
    //        //    bucket.StudentNames.Add(student.FirstName + " " + student.LastName);
    //        //}





    //        return View(bucket);
    //    }
    //}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperationSmurf.Models;
using OperationSmurf.ViewModels;

namespace OperationSmurf.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventContext _context;
        private readonly GradeContext _gradeContext;
        private readonly StudentContext _studentContext;
        private readonly SectionContext _sectionContext;

        public HomeController(EventContext ec, GradeContext gc, StudentContext sc, SectionContext ssc)
        {
            _context = ec;
            _gradeContext = gc;
            _studentContext = sc;
            _sectionContext = ssc;
        }
                     
        public IActionResult Index(int ?id=1)
        {
            //Sections Button Prep for View

            //This is where we are preparing the data to send the sections available 
            //to dispaly as buttons to the view.
            var classes = new List<Section>();         
            foreach(Section s in _sectionContext.Section)
            {
                classes.Add(s);
            }
            ViewData["classes"] = classes;
            ViewData["tracer"] = "This is the ID: " + id.ToString();

            ClassroomGrid myClass = new ClassroomGrid();
            myClass = getViewModel(id);
            //ClassroomGrid classToView = new ClassroomGrid();
            //classToView.StudentNames = new List<string>();
            //classToView.Columns = new List<Event>();

            ////Todo: calculate dynamic size
            //classToView.GradeGrid = new Grade[50,50];

            ////Loading current section found by ID of button clicked in View
            //var s2 =  _sectionContext.Section
            //    .FirstOrDefault(m => m.Id == id);

            //if (s2 == null)
            //{
            //    s2 = _sectionContext.Section.FirstOrDefault(m => m.Id == 1);
            //}  
           

            //Loading Columns of Events
            //var cols = _context.Event.Where(e => e.EventCode == s2.EventCode);
            //foreach (Event ev in cols)
            //{
            //    classToView.Columns.Add(ev);
            //}

            ////Loading Students for Grades

            //classToView.Students = _studentContext.Student.Where(m => 
            //(m.Period1 == id) ||
            //(m.Period2 == id) ||
            //(m.Period3 == id) ||
            //(m.Period4 == id) ||
            //(m.Period5 == id) ||
            //(m.Period6 == id) 
            //).ToList();

            ////Loading Students
            //foreach (Student student in classToView.Students)
            //{
            //    classToView.StudentNames.Add(student.FirstName + " " + student.LastName);
            //}

            //int numStuds = classToView.Students.Count();

            
            //for(int x=0; x < numStuds; x++)
            //{

            //    for(int y=0; y<classToView.Columns.Count; y++)
            //    {
            //        //Grades pulled that match the section and match the student
            //        classToView.GradeGrid[x, y] = _gradeContext.Grade.First(g => (g.EventId == classToView.Columns[y].Id) && (g.StudentId == classToView.Students[x].Id));
            //    }
                
            //}
            return View(myClass);
        }

        public ClassroomGrid getViewModel(int ?id = 1)
        {
            ////Loading current section found by ID of button clicked in View
            var s2 =  _sectionContext.Section
                .FirstOrDefault(m => m.Id == id);

            if (s2 == null)
            {
                s2 = _sectionContext.Section.FirstOrDefault(m => m.Id == 1);
            }  


            Section section = _sectionContext.Section.Find(id);
            ClassroomGrid classToView = new ClassroomGrid();

            classToView.StudentNames = new List<string>();
            classToView.Columns = new List<Event>();

            //Loading Columns of Events
            var cols = _context.Event.Where(e => e.EventCode == s2.EventCode);
            foreach (Event ev in cols)
            {
               classToView.Columns.Add(ev);
            }



            classToView.Students = _studentContext.Student.Where(m =>
            (m.Period1 == id) ||
            (m.Period2 == id) ||
            (m.Period3 == id) ||
            (m.Period4 == id) ||
            (m.Period5 == id) ||
            (m.Period6 == id)
            ).ToList();
            classToView.GradeGrid = new Grade[classToView.Students.Count, classToView.Columns.Count];

            //Loading Students
            foreach (Student student in classToView.Students)
            {
                classToView.StudentNames.Add(student.FirstName + " " + student.LastName);
            }

            int numStuds = classToView.Students.Count();
            for (int x = 0; x < numStuds; x++)
            {

                for (int y = 0; y < classToView.Columns.Count; y++)
                {

                    try
                    {
                        //Grades pulled that match the section and match the student
                        classToView.GradeGrid[x, y] = _gradeContext.Grade.First(g => (g.EventId == classToView.Columns[y].Id) && (g.StudentId == classToView.Students[x].Id));

                        if (classToView.GradeGrid == null)
                        {
                            classToView.GradeGrid[0, 0] = new Grade();
                            ViewData["LoadError"] = "Grade grid was empty for this section...";
                        }


                    }catch(Exception e)
                    {
                        ViewData["LoadError"] = "Grade grid was empty for this section...";
                    }
                    
                }

            }



            return classToView;
        }













        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

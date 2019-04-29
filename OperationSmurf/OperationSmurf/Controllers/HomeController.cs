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

        private ClassroomGrid grid;

        public HomeController(EventContext ec, GradeContext gc, StudentContext sc, SectionContext ssc)
        {
            _context = ec;
            _gradeContext = gc;
            _studentContext = sc;
            _sectionContext = ssc;
            grid = new ClassroomGrid();
        }

        public IActionResult Index(int? id = 1 )
        {
            //ClassroomGrid myClass = new ClassroomGrid();
            if (grid == null)
            {
                this.grid = new ClassroomGrid();
                this.grid = getViewModel(id);
            } else
            {
                this.grid = getViewModel(id);
            }
            
            //Sections Button Prep for View

            //This is where we are preparing the data to send the sections available 
            //to dispaly as buttons to the view.
            var classes = new List<Section>();
            foreach (Section s in _sectionContext.Section)
            {
                classes.Add(s);
            }
            ViewData["classes"] = classes;
            ViewData["tracer"] = "This is the ID: " + id.ToString();
            ViewData["sectionId"] = id;
            

            return View(this.grid);
        }


        public  IActionResult ChangeGrade([Bind("x,y,z,ChangeState,sectionId")] int ?id)
           
        {
            //getViewModel(id);
            int x = -1;
            int y = -1;
            int studId = -1;
            int newState = -1;
            int sectionId = -1;

            if (int.TryParse(HttpContext.Request.Form["x"], out x))
            {

            }
            else
            {

            }
            if (int.TryParse(HttpContext.Request.Form["y"], out y))
            {

            }
            else
            {

            }
            if (int.TryParse(HttpContext.Request.Form["z"], out studId))
            {

            }
            else
            {

            }
            if (int.TryParse(HttpContext.Request.Form["ChangeState"], out newState))
            {

            }
            else
            {

            }
            //if (int.TryParse(HttpContext.Request.Form["sectionId"], out sectionId))
            //{

            //}
            //else
            //{

            //}

            //Load proper grade
            var newGrade =  _gradeContext.Grade.First(t => 

            (t.SectionId == id) && (t.EventId == y) && (t.StudentId == studId)

            );
            if (newGrade != null)
            { 
                newGrade.State = newState-1;
                _gradeContext.Update(newGrade);
                _gradeContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
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
            //ClassroomGrid classToView = new ClassroomGrid();
            this.grid.CourseName = section.CourseName;
            this.grid.StudentNames = new List<string>();
            this.grid.Columns = new List<Event>();

            //Loading Columns of Events
            var cols = _context.Event.Where(e => e.EventCode == s2.EventCode);
            foreach (Event ev in cols)
            {
               this.grid.Columns.Add(ev);
            }



            this.grid.Students = _studentContext.Student.Where(m =>
            (m.Period1 == id) ||
            (m.Period2 == id) ||
            (m.Period3 == id) ||
            (m.Period4 == id) ||
            (m.Period5 == id) ||
            (m.Period6 == id)
            ).ToList();
            this.grid.GradeGrid = new Grade[this.grid.Students.Count, this.grid.Columns.Count];

            //Loading Students
            foreach (Student student in this.grid.Students)
            {
                this.grid.StudentNames.Add(student.FirstName + " " + student.LastName);
                this.grid.StudentIds.Add(student.Id);
            }

            int numStuds = this.grid.Students.Count();
            for (int x = 0; x < numStuds; x++)
            {

                for (int y = 0; y < this.grid.Columns.Count; y++)
                {

                    try
                    {
                        //Grades pulled that match the section and match the student
                        this.grid.GradeGrid[x, y] = _gradeContext.Grade.First(g => (g.EventId == this.grid.Columns[y].Id) && (g.StudentId == this.grid.Students[x].Id));

                        if (this.grid.GradeGrid == null)
                        {
                            this.grid.GradeGrid[0, 0] = new Grade();
                            ViewData["LoadError"] = "Grade grid was empty for this section...";
                        }


                    }catch(Exception e)
                    {
                        ViewData["LoadError"] = "Grade grid was empty for this section...";
                    }
                    
                }

            }



            return this.grid;
        }



        //Action to fill targets in ViewModel and return to popup launch
        public ActionResult ModalPopUp(int ?id=1)
        {
            

            return PartialView();
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

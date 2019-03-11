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

            //Now that we know we are selecting the appropriate Section,
            //we must instantiate the viewmodel and then populate it for viewing.

            //Todo: We will still need to figure out how to make cells editable,
            //But first we need to get the data from all the contexts into the viewmodel
            //Successfully. That starts here:

            ClassroomGrid classToView = new ClassroomGrid();
            classToView.StudentNames = new List<string>();

            //Loading current section found by ID of button clicked in View
            var s2 =  _sectionContext.Section
                .FirstOrDefault(m => m.Id == id);

            if (s2 == null)
            {
                s2 = _sectionContext.Section.FirstOrDefault(m => m.Id == 1);
            }  

            //Loading Students
            var studs =  _studentContext.Student.Where(s =>
                   (s.Period1 == id) ||
                   (s.Period2 == id) ||
                   (s.Period3 == id) ||
                   (s.Period4 == id) ||
                   (s.Period5 == id) ||
                   (s.Period6 == id));

            foreach(Student student in studs) {
                classToView.StudentNames.Add(student.FirstName + " " + student.LastName);
            }

            //Loading Columns of Events
         //   var cols = _context.Event.Where(e => e.)








            return View(classToView);
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

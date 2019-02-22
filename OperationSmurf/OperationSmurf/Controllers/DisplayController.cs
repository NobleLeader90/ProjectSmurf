using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OperationSmurf.Models;
using OperationSmurf.ViewModels;

namespace OperationSmurf.Controllers
{
    public class DisplayController : Controller
    {
        public ClassroomGrid bucket { get; set; }

        private readonly EventContext _Eventcontext;
        private readonly GradeContext _Gradecontext;
        private readonly SectionContext _Sectioncontext;
        private readonly StudentContext _Studentcontext;

        public DisplayController(EventContext context1, GradeContext context2, SectionContext context3, StudentContext context4 )
        {
            _Eventcontext = context1;
            _Gradecontext = context2;
            _Sectioncontext = context3;
            _Studentcontext = context4;

            bucket = new ClassroomGrid();

        }




        public IActionResult Index(int Id)
        {
            var names = _Studentcontext.Student.OrderBy(a => a.LastName).Select(b => b.FirstName + " " + b.LastName).ToList();
            foreach(string name in names)
            {
                bucket.StudentNames.Add(name);
            }
            
            
           
            return View(bucket);
        }
    }
}
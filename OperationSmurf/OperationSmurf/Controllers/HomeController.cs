using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OperationSmurf.Models;

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
                     
        public IActionResult Index(int ?id)
        {
            var classes = new List<Section>();
            //todo: Add array of links to load each class' view
            foreach(Section section in _sectionContext.Section)
            {
                classes.Add(section);
            }
            ViewData["classes"] = classes;

            ViewData["tracer"] = "This is the ID: " + id.ToString();

            return View();
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

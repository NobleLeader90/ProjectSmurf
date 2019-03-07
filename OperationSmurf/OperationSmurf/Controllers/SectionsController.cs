using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OperationSmurf.Models;
using OperationSmurf.ViewModels;

namespace OperationSmurf.Controllers
{
    public class SectionsController : Controller
    {
        //Fields of the Controller
        private readonly SectionContext _context;
        private readonly StudentContext _studContext;
        

        //public string LastName { get; private set; }

        //Constructor
        public SectionsController(SectionContext context, StudentContext studContext)     //<---------------added formal parameter for studentContext injection
        {
            _context = context;
            _studContext = studContext;     //<---------------added injection
        }

        // GET: Sections
        public async Task<IActionResult> Index()
        {           
           
            return View(await _context.Section.ToListAsync());

        }

        // GET: Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .FirstOrDefaultAsync(m => m.Id == id);
   
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,Period,TeacherName,Roster")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }

        // GET: Sections/ShowRoster/5
        public async Task<IActionResult> ShowRoster(int? id)
        {
            Roster roster = new Roster();

            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            roster.Section = section;
            //This is the best piece of code I have written all year :)
            var studs = _studContext.Student.Where(t =>

                (t.Period1 == section.Id) || (t.Period2 == section.Id) || (t.Period3 == section.Id) ||
                (t.Period4 == section.Id) || (t.Period5 == section.Id) || (t.Period6 == section.Id)

                );

            roster.Students = new List<Student>();

            foreach (Student r in studs)
            {
                roster.Students.Add(r);
            }

            //We must perform ALL sql queries and writes in each controller as needed.  
            //I now have a simplified setup. See above code for the cleanest way to do this for what is
            //a build-type query.  Note I had to ADD A STUDENT CONTEXT to this controller. (in now
            //uses _context (the sectionContext) and the NEW _studContext (studentContext) which I added
            //as a field and an input parameter for its injection, and the requisit assignment in the constructor.
            return View(roster);
        }


        // GET: Sections/EditRoster/5
        public async Task<IActionResult> EditRoster(int? id)
        {
            Roster roster = new Roster();

            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            roster.Section = section;
            //This is the best piece of code I have written all year :)
            var studs = _studContext.Student.Where(t =>

                (t.Period1 == section.Id) || (t.Period2 == section.Id) || (t.Period3 == section.Id) ||
                (t.Period4 == section.Id) || (t.Period5 == section.Id) || (t.Period6 == section.Id)

                );

            roster.Students = new List<Student>();

            foreach (Student r in studs)
            {
                roster.Students.Add(r);
            }

            //We must perform ALL sql queries and writes in each controller as needed.  
            //I now have a simplified setup. See above code for the cleanest way to do this for what is
            //a build-type query.  Note I had to ADD A STUDENT CONTEXT to this controller. (in now
            //uses _context (the sectionContext) and the NEW _studContext (studentContext) which I added
            //as a field and an input parameter for its injection, and the requisit assignment in the constructor.
            return View(roster);
        }


        //POST: Sections/EditRoster/5
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoster(int id, [Bind("Section,Students,[FromFormAttribute]FirstName,[FromFormAttribute]LastName,[FromFormAttribute]StudentId,[FromFormAttribute]StudRemover")] Roster roster)
        {
            if (id != roster.Section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {      
                    if (roster.Students == null)
                    {
                        roster.Students = new List<Student>();
                        var studs = _studContext.Student.Where(t =>
                        
                        (t.Period1 == roster.Section.Id) || (t.Period2 == roster.Section.Id) || (t.Period3 == roster.Section.Id) ||
                        (t.Period4 == roster.Section.Id) || (t.Period5 == roster.Section.Id) || (t.Period6 == roster.Section.Id)

                        );

                        foreach (Student f in studs)
                        {
                            roster.Students.Add(f);
                        }

                    }

                    Student s;
                    try
                    {
                         s = _studContext.Student.First(r =>

                            ((r.FirstName == HttpContext.Request.Form["FirstName"].ToString()) &&

                            (r.LastName == HttpContext.Request.Form["LastName"].ToString())) ||                   

                            (r.StudentId == HttpContext.Request.Form["StudentId"])

                            );
                    } catch
                    {
                        ViewBag.Message = "Student Not Found in Database...";
                             return (View(roster));
                    }

                   

                    switch (roster.Section.Period)
                    {
                        case 1:
                            s.Period1 = roster.Section.Id;
                            break;
                        case 2:
                            s.Period2 = roster.Section.Id;
                            break;
                        case 3:
                            s.Period3 = roster.Section.Id;
                            break;
                        case 4:
                            s.Period4 = roster.Section.Id;
                            break;
                        case 5:
                            s.Period5 = roster.Section.Id;
                            break;
                        case 6:
                            s.Period6 = roster.Section.Id;
                            break;

                        default:
                            break;
                    }
                    roster.Students.Add(s);

                    _context.Update(roster.Section);
                    _studContext.Update(s);
                    await _studContext.SaveChangesAsync();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(roster.Section.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.Message = "";
                return RedirectToAction(nameof(EditRoster));
            }
            ViewBag.Message = "";
            return View(roster);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,Period,TeacherName,Roster")] Section section)
        {
            if (id != section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }

        // GET: Sections/RemoveStudent/5
        public async Task<IActionResult> RemoveStudent(int? id, Roster roster)  
        {
            if (id == null)
            {
                return NotFound();
            }
               
            var section = await _context.Section
               .FirstOrDefaultAsync(m => m.Id == id);

            if (section == null)
            {
                return NotFound();
            }
            //Roster roster = new Roster();
            //roster.Section = section;
            roster.Students = new List<Student>();

            if (roster.Students.Count == 0)
            {
                roster.Students = new List<Student>();
                var studs = _studContext.Student.Where(t =>

                (t.Period1 == roster.Section.Id) || (t.Period2 == roster.Section.Id) || (t.Period3 == roster.Section.Id) ||
                (t.Period4 == roster.Section.Id) || (t.Period5 == roster.Section.Id) || (t.Period6 == roster.Section.Id)

                );

                foreach (Student f in studs)
                {
                    roster.Students.Add(f);
                    await _context.SaveChangesAsync();
                    
                }
                return RedirectToAction(nameof(EditRoster),id);
            }

            return View(roster);
             
        }

        // Post: Sections/RemoveStudent/5
        [HttpPost, ActionName("RemoveStudent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmRemoveStudent(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var section = await _context.Section
               .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }
            Roster roster = new Roster();
            roster.Section = section;
            roster.Students = new List<Student>();

            if (roster.Students.Count == 0)
            {
                roster.Students = new List<Student>();
                var studs = _studContext.Student.Where(t =>

                (t.Period1 == roster.Section.Id) || (t.Period2 == roster.Section.Id) || (t.Period3 == roster.Section.Id) ||
                (t.Period4 == roster.Section.Id) || (t.Period5 == roster.Section.Id) || (t.Period6 == roster.Section.Id)

                );

                String s = HttpContext.Request.Form["StudRemover"];
                int p = Convert.ToInt32(s);

                foreach (Student f in studs)
                {
                    if (f.Id == p)
                    {
                        if (f.Period1 == roster.Section.Id) { f.Period1 = 0; }
                        if (f.Period2 == roster.Section.Id) { f.Period2 = 0; }
                        if (f.Period3 == roster.Section.Id) { f.Period3 = 0; }
                        if (f.Period4 == roster.Section.Id) { f.Period4 = 0; }
                        if (f.Period5 == roster.Section.Id) { f.Period5 = 0; }
                        if (f.Period6 == roster.Section.Id) { f.Period6 = 0; }
                    }
                }
                await _context.SaveChangesAsync();
                await _studContext.SaveChangesAsync();
                return RedirectToAction(nameof(EditRoster), id);
            }

            return View(roster);

        }








        // GET: Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Section.FindAsync(id);
            _context.Section.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
            return _context.Section.Any(e => e.Id == id);
        }
    }
}

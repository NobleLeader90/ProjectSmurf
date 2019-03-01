using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OperationSmurf.Models;

namespace OperationSmurf.Controllers
{
    public class SectionsController : Controller
    {
        private readonly SectionContext _context;
        private Student s1;
        private Student s2;
        Section section;

        public SectionsController(SectionContext context)
        {
            _context = context;

            //Now experimenting with 2-deep table issue because we need a list of students as a roster in each section. 
            //First testing how to get roster list out if I manually add a roster while I work out the roster view issues
            //Manually building 2 students to inject as a test roster
             s1 = new Student();
             s2 = new Student();
            s1.FirstName = "Bob";
            s1.LastName = "Smith";
            s2.FirstName = "Alice";
            s2.LastName = "Jones";
            section = new Section();
            


        }

        // GET: Sections
        public async Task<IActionResult> Index()
        {
            //TEST CODE Checks 4.0
            section = _context.Section.FirstOrDefault(a => a.CourseName == "IDT");
            section.Roster.Add(s1);
            section.Roster.Add(s2);

            //This successfully get RAW output to screen First name only for now
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
            section.Roster.Add(s1);
            section.Roster.Add(s2);


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

        // GET: Sections/Roster/5
        public async Task<IActionResult> SetRoster(int? id)
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


            //Added to deal with null pointers
            if (section.Roster.Count == 0)
            {
                section.Roster.Add(new Student());
            }

            return View(section);
        }








        // GET: Sections/Edit/5
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

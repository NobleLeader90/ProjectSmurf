﻿using System;
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
        public async Task<IActionResult> EditRoster(int id, [Bind("Section,Students,[FromFormAttribute]FirstName,[FromFormAttribute]LastName,[FromFormAttribute]StudentId")] Roster roster)
        {
            if (id != roster.Section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {      
                    //if (roster.Students != null)
                    //{
                       
                    //}

                    var s = _studContext.Student.First(r => r.LastName == HttpContext.Request.Form["LastName"].ToString());


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
                    //_studContext.Update(s);
                   // await _studContext.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
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

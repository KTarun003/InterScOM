using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterScOM.Areas.Forum.Models;
using InterScOM.Data;

namespace InterScOM.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class MainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forum/Main
        public async Task<IActionResult> Index()
        {
            return View(await _context.Queries.ToListAsync());
        }

        // GET: Forum/Main/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            var answers = await _context.Answers.ToListAsync();
            foreach (var answer in answers)
            {
                if (answer.QueryId == query.Id) 
                    query.Answers.Add(answer);
            }
            if (query == null)
            {
                return NotFound();
            }
            return View(query);
        }

       

        // GET: Forum/Main/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forum/Main/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Topic,UserName,Question,UpVotes,DownVotes")] Query query)
        {
            if (ModelState.IsValid)
            {
                _context.Add(query);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(query);
        }


    }
}

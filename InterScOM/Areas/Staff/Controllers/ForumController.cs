using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterScOM.Areas.Forum.Models;
using InterScOM.Data;
using Microsoft.AspNetCore.Authorization;

namespace InterScOM.Areas.Staff.Controllers
{
    [Authorize(Roles = "staff")]
    [Area("Staff")]
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Staff/Forum
        public async Task<IActionResult> Index()
        {
            return View(await _context.Queries.ToListAsync());
        }

        // GET: Staff/Forum/Details/5
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

        // GET: Staff/Forum/Create
        public IActionResult Create(int id)
        {
            Answer answer = new Answer()
            {
                QueryId = id
            };
            return View(answer);
        }

        // POST: Staff/Forum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QueryId,UserName,ThreadAnswer,UpVotes,DownVotes")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(answer);
        }

        // GET: Staff/Forum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: Staff/Forum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QueryId,UserName,ThreadAnswer,UpVotes,DownVotes")] Answer answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
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
            return View(answer);
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}

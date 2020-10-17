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

namespace InterScOM.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Forum
        public async Task<IActionResult> Index()
        {
            return View(await _context.Queries.ToListAsync());
        }

        // GET: Admin/Forum/Details/5
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

        // GET: Admin/Forum/CreateQ
        public IActionResult CreateQ()
        {
            return View();
        }

        // POST: Admin/Forum/CreateQ
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQ([Bind("Id,Topic,UserName,Question,UpVotes,DownVotes")] Query query)
        {
            if (ModelState.IsValid)
            {
                _context.Add(query);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(query);
        }

        // GET: Admin/Forum/Create
        public IActionResult Create(int id)
        {
            Answer answer = new Answer()
            {
                QueryId = id
            };
            return View(answer);
        }

        // POST: Admin/Forum/Create
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
                // after making answering returning to the details page

                return RedirectToAction("Details", new { id = answer.QueryId});
            }
            return View(answer);
        }

        // GET: Admin/Forum/Edit/5
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

        // POST: Admin/Forum/Edit/5
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

        // GET: Admin/Forum/DeleteQ/5
        public async Task<IActionResult> DeleteQ(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queries = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (queries == null)
            {
                return NotFound();
            }

            return View(queries);
        }

        // POST: Admin/Forum/DeleteQ/5
        [HttpPost, ActionName("DeleteQ")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queries = await _context.Queries.FindAsync(id);
            _context.Queries.Remove(queries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Forum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Admin/Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAnswer(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answers);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = answers.QueryId });
        }

        // GET: Forum/Main/Details/5
        public async Task<IActionResult> UpVote(int? id, string type)
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
            if (type.Equals("Answer"))
            {
                var answer = await _context.Answers.FindAsync(id);
                answer.DownVotes++;
                _context.Answers.Update(answer);
            }
            else
            {
                query.DownVotes++;
                _context.Queries.Update(query);
            }
            await _context.SaveChangesAsync();
            return View("Details", query);
        }

        // GET: Forum/Main/Details/5
        public async Task<IActionResult> DownVote(int? id, string type)
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
            if (type.Equals("Answer"))
            {
                var answer = await _context.Answers.FindAsync(id);
                answer.DownVotes++;
                _context.Answers.Update(answer);
            }
            else
            {
                query.DownVotes++;
                _context.Queries.Update(query);
            }
            await _context.SaveChangesAsync();
            return View("Details", query);
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}

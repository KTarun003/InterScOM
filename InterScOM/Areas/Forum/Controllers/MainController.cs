using InterScOM.Areas.Forum.Models;
using InterScOM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            var list = await _context.Queries.ToListAsync();
            list = list.OrderByDescending(q => q.UpVotes).ToList();
            return View(list);
        }

        // GET: Forum/Main/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Query query = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            System.Collections.Generic.List<Answer> answers = await _context.Answers.ToListAsync();
            foreach (Answer answer in answers)
            {
                if (answer.QueryId == query.Id)
                {
                    query.Answers.Add(answer);
                }
            }

            query.Answers = query.Answers.OrderByDescending(a => a.UpVotes).ToList();
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

        // GET: Forum/Main/Details/5
        public async Task<IActionResult> UpVote(int? id, string type)
        {
            if (id == null)
            {
                return NotFound();
            }

            Query query = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            System.Collections.Generic.List<Answer> answers = await _context.Answers.ToListAsync();
            foreach (Answer answer in answers)
            {
                if (answer.QueryId == query.Id)
                {
                    query.Answers.Add(answer);
                }
            }
            if (query == null)
            {
                return NotFound();
            }
            if (type.Equals("Answer"))
            {
                Answer answer = await _context.Answers.FindAsync(id);
                answer.UpVotes++;
                _context.Answers.Update(answer);
            }
            else
            {
                query.UpVotes++;
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
            Query query = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            System.Collections.Generic.List<Answer> answers = await _context.Answers.ToListAsync();
            foreach (Answer answer in answers)
            {
                if (answer.QueryId == query.Id)
                {
                    query.Answers.Add(answer);
                }
            }
            if (query == null)
            {
                return NotFound();
            }
            if (type.Equals("Answer"))
            {
                Answer answer = await _context.Answers.FindAsync(id);
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
    }
}

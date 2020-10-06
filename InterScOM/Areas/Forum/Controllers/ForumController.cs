using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InterScOM.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

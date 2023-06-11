using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chosen.Models;


namespace chosen.Controllers
{
    public class BreakingNewsController : Controller
    {
        private readonly FinalProjectContext _context;

        public BreakingNewsController(FinalProjectContext context)
        {
            _context = context;
        }

        //GET: BreakingNews
        public async Task<IActionResult> Index()
        {
            return _context.BreakingNews != null ?
                        View(await _context.BreakingNews.ToListAsync()) :
                        Problem("Entity set 'Index'  掛了.");

        }

        public async Task<IActionResult> Description(int id)
        {
            return _context.BreakingNews != null ?
                View(await _context.BreakingNews.Where(s => s.BreakingNewsId == id).FirstAsync()) :
                        Problem("Entity set 'Description'  掛了.");

        }



    }
}

using chosen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace chosen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FinalProjectContext _context;

        public HomeController(ILogger<HomeController> logger, FinalProjectContext context)
        {
            _logger = logger;
            _context = context; 
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.BreakingNews.OrderByDescending(s=>s.Date).Take(5).ToListAsync());                   
        }

        public IActionResult GetPrizePool()
        {
            DateTime oneDayAgo = DateTime.Now.AddDays(-1);
            DateTime oneWeekAgo = DateTime.Now.AddDays(-7);
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);

            var prizePoolData = _context.ShowRawards
                .Select(sr => new
                {
                    ShowRaward = sr,
                    TotalItems = sr.ShowRawardItems.Sum(sri => sri.Num),
                    RemainingItems = sr.ShowRawardItems.Sum(sri => sri.LaveNum),
                    FactoryName = sr.Factory.Name, // 添加FactoryName
                    TotalDrawRecords1Day = sr.DrawRecords.Count(dr => dr.DrawTime > oneDayAgo && dr.DrawCount > 0), // 添加TotalDrawRecords1Day
                    TotalDrawRecords1Week = sr.DrawRecords.Count(dr => dr.DrawTime > oneWeekAgo && dr.DrawCount > 0), // 添加TotalDrawRecords1Week
                    TotalDrawRecords1Month = sr.DrawRecords.Count(dr => dr.DrawTime > oneMonthAgo && dr.DrawCount > 0), // 添加TotalDrawRecords1Month

                    TotalDrawCount1Day = sr.DrawRecords.Where(dr => dr.DrawTime > oneDayAgo && dr.DrawCount > 0).Sum(dr => dr.DrawCount), // 修改为TotalDrawCount1Day
                    TotalDrawCount1Week = sr.DrawRecords.Where(dr => dr.DrawTime > oneWeekAgo && dr.DrawCount > 0).Sum(dr => dr.DrawCount), // 修改为TotalDrawCount1Week
                    TotalDrawCount1Month = sr.DrawRecords.Where(dr => dr.DrawTime > oneMonthAgo && dr.DrawCount > 0).Sum(dr => dr.DrawCount) // 修改为TotalDrawCount1Month
                })
                .ToList();

            return Json(prizePoolData);
        }


        public IActionResult empty()
        {
            return View();
        }

        public IActionResult signup()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Aboutus()
        {
            return View();
        }

        public IActionResult Prizepool()
        {
            return View();
        }

        public IActionResult Trading()
        {
            return View();
        }

        public IActionResult Game()
        {
            return View();
        }

        public IActionResult Customerservice()
        {
            return View();
        }

        public IActionResult Classification()
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
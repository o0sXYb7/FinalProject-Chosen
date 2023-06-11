using chosen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chosen.Controllers
{
    public class PrizePoolDetailController : Controller
    {
        private readonly FinalProjectContext _context;

        public PrizePoolDetailController(FinalProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult getShowRawards(int id)
        {
            var showRaward = _context.ShowRawards
                .Where(r => r.ShowRawardId == id)
                .Join(
                    _context.Factories,
                    showR => showR.FactoryId,
                    fa => fa.FactoryId,
                    (showR, fa) => new
                    {
                        showR.ShowRawardId,
                        showR.AllowStoreDay,
                        showR.OneDrawMoney,
                        showR.AddProbability,
                        showR.NowProbability,
                        showR.HasSelectNumber,
                        showR.Image,
                        showR.Series,
                        showR.Name,
                        showR.Fare,
                        showR.Introduction,
                        FactoryName = fa.Name
                    }
                )
                .ToList();

            return Json(showRaward);
        }

        [HttpGet]
        public IActionResult getShowRawardsItems(int id)
        {
            var showRawardItems = _context.ShowRawardItems
                .Where(r => r.ShowRawardId == id)
                .ToList();

            return Json(showRawardItems);
        }

    }
}

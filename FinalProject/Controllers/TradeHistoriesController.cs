using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class TradeHistoriesController : Controller
    {
        private readonly FinalProjectContext _context;

        public TradeHistoriesController(FinalProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies["EmployeeId"] != null)
            {
                return _context.TradeHistories != null ?
                        View(_context.TradeHistories) :
                        Problem("Entity set 'FinalProjectContext.TradeHistories'  is null.");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IEnumerable<TradeHistory>> getTradeHistories()
        {
            return _context.TradeHistories != null ?
                          _context.TradeHistories :
                          null;
        }

        [HttpGet]
        public IActionResult OPenModalToUpdate(int? id)
        {
            var tradeHistoriesWithImage = _context.TradeHistories
                .Where(th => th.TradeHistoryId == id)
                .Join(
                    _context.Commodities,
                    th => th.CommodityId,
                    c => c.CommodityId,
                    (th, c) => new { TradeHistory = th, Commodity = c }
                )
                .Join(
                    _context.TempStorages,
                    t => t.Commodity.TempStorageId,
                    ts => ts.TempStorageId,
                    (t, ts) => new { t.TradeHistory, t.Commodity, TempStorage = ts }
                )
                .Join(
                    _context.ShowRawardItems,
                    ts => ts.TempStorage.PrizeId,
                    sri => sri.ShowRawardItemId,
                    (ts, sri) => new
                    {
                        TradeHistory = ts.TradeHistory,
                        Image = sri.Image
                    }
                )
                .ToList();

            return Json(tradeHistoriesWithImage);
        }

        private bool CommodityExists(int id)
        {
            return (_context.TradeHistories?.Any(e => e.TradeHistoryId == id)).GetValueOrDefault();
        }
    }
}

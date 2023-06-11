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
    public class CommoditiesController : Controller
    {
        private readonly FinalProjectContext _context;

        public CommoditiesController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: Commodities
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["EmployeeId"] != null)
            {
                return _context.Commodities != null ?
                        View(await _context.Commodities.ToListAsync()) :
                        Problem("Entity set 'FinalProjectContext.Commodities'  is null.");
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public async Task<IEnumerable<Commodity>> getCommodities()
        {
            return _context.Commodities != null ?
                          _context.Commodities :
                          null;
        }

        [HttpGet]
        public IActionResult OPenModalToUpdate(int? id)
        {
            var Commodity = _context.Commodities
                .Where(th => th.CommodityId == id)
                .Join(
                    _context.TempStorages,
                    t => t.TempStorageId,
                    ts => ts.TempStorageId,
                    (t, ts) => new {
                        Commodity = t ,
                        TempStorage = ts
                    }
                )
                .Join(
                    _context.ShowRawardItems,
                    ts => ts.TempStorage.PrizeId,
                    sri => sri.ShowRawardItemId,
                    (ts, sri) => new
                    {
                        Commodity = ts.Commodity,
                        Image = sri.Image
                    }
                )
                .ToList();

            return Json(Commodity);
        }

        private bool CommodityExists(int id)
        {
            return (_context.Commodities?.Any(e => e.CommodityId == id)).GetValueOrDefault();
        }
    }
}

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
    public class TempStoragesController : Controller
    {
        private readonly FinalProjectContext _context;

        public TempStoragesController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: TempStorages
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["EmployeeId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IEnumerable<TempStorage>> getTempStorages()
        {
            return _context.TempStorages != null ?
                          _context.TempStorages :
                          null;
        }

        private bool TempStorageExists(int id)
        {
            return (_context.TempStorages?.Any(e => e.TempStorageId == id)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<TempStorage> OPenModalToUpdate(int? id)
        {
            var TempStorage = await _context.TempStorages.FindAsync(id);

            return TempStorage != null ? TempStorage : null;
        }
    }
}

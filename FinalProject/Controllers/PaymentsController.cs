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
    public class PaymentsController : Controller
    {
        private readonly FinalProjectContext _context;

        public PaymentsController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var finalProjectContext = _context.Payments.Include(p => p.Member);
            return View(await finalProjectContext.ToListAsync());
        }

        public async Task<IEnumerable<Payment>> getPayments()
        {
            return _context.Payments != null ?
                          _context.Payments :
                          null;
        }

        [HttpGet]
        public async Task<Payment> OPenModalToUpdate(int? id)
        {
            var payment = await _context.Payments.FindAsync(id);

            return payment != null ? payment : null;
        }

    }
}

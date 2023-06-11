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
    public class CarouselsController : Controller
    {
        private readonly FinalProjectContext _context;

        public CarouselsController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: Carousels
        public async Task<IActionResult> Index()
        {
              return _context.Carousels != null ? 
                          View(await _context.Carousels.ToListAsync()) :
                          Problem("Entity set 'ProductContext.Carousel'  is null.");
        }

        // GET: Carousels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carousels == null)
            {
                return NotFound();
            }

            var carousel = await _context.Carousels
                .FirstOrDefaultAsync(m => m.CarouselId == id);
            if (carousel == null)
            {
                return NotFound();
            }

            return View(carousel);
        }

        // GET: Carousels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carousels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarouselID,PictureName,Url")] Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carousel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carousel);
        }

        // GET: Carousels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carousels == null)
            {
                return NotFound();
            }

            var carousel = await _context.Carousels.FindAsync(id);
            if (carousel == null)
            {
                return NotFound();
            }
            return View(carousel);
        }

        // POST: Carousels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarouselID,PictureName,Url")] Carousel carousel)
        {
            if (id != carousel.CarouselId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carousel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarouselExists(carousel.CarouselId))
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
            return View(carousel);
        }

        // GET: Carousels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carousels == null)
            {
                return NotFound();
            }

            var carousel = await _context.Carousels
                .FirstOrDefaultAsync(m => m.CarouselId == id);
            if (carousel == null)
            {
                return NotFound();
            }

            return View(carousel);
        }

        // POST: Carousels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carousels == null)
            {
                return Problem("Entity set 'ProductContext.Carousel'  is null.");
            }
            var carousel = await _context.Carousels.FindAsync(id);
            if (carousel != null)
            {
                _context.Carousels.Remove(carousel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarouselExists(int id)
        {
          return (_context.Carousels?.Any(e => e.CarouselId == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace FinalProject.Controllers
{
    public class WishlistsController : Controller
    {
        private readonly FinalProjectContext _context;

        public WishlistsController(FinalProjectContext context)
        {
            _context = context;


        }



        // GET: Wishlists
        public async Task<IActionResult> Index()
        {
            return _context.Wishlist != null ?
                        View(await _context.Wishlist.ToListAsync()) :
                        Problem("Entity set 'FinalProjectContext.Wishlist'  is null.");
        }

        // GET: Wishlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wishlist == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // GET: Wishlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wishlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,PraductId,Itemtitle,Productdescription,Price,ImagePath,AddedTime,CustomerId")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wishlist);
        }

        //新增從獎池加入
        // POST: Wishlists/FrontCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> FrontCreate([Bind("ShowRawardId")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wishlist);
        }

        // GET: Wishlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wishlist == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,PraductId,Itemtitle,Productdescription,Price,ImagePath,AddedTime,CustomerId")] Wishlist wishlist)
        {
            if (id != wishlist.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistExists(wishlist.ItemId))
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
            return View(wishlist);
        }

        // GET: Wishlists/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Wishlist == null)
        //    {
        //        return NotFound();
        //    }

        //    var wishlist = await _context.Wishlist
        //        .FirstOrDefaultAsync(m => m.ItemId == id);
        //    if (wishlist == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wishlist);
        //}

        // POST: Wishlists/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Wishlist == null)
            {
                return Problem("Entity set 'FinalProjectContext.Wishlist'  is null.");
            }
            var wishlist = await _context.Wishlist.FindAsync(id);
            if (wishlist != null)
            {
                _context.Wishlist.Remove(wishlist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(int id)
        {
            return (_context.Wishlist?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }

        public IActionResult GetWishlist()
        {
            var wishlists = _context.Wishlist
                .Join(
                    _context.ShowRawards,
                    w => w.PraductId,
                    sr => sr.ShowRawardId,
                    (w, sr) => new
                    {
                        w.ItemId,
                        w.PraductId,
                        sr.ShowRawardId,
                        sr.Name,
                        sr.OneDrawMoney,
                        sr.CreateTime,
                        sr.Image,
                        sr.Series,
                        w.CustomerId,
                    }
                )
                .ToList();

            return Json(wishlists);
        }
    }
}

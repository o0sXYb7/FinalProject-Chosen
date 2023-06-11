using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chosen.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace chosen.Controllers
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
            return View();
        }

        // GET: Wishlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wishlists == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists
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

        public class WishView
        {
            public int wId { get; set; }
        }

        //新增從獎池加入
        // POST: Wishlists/FrontCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string FrontCreate([FromBody] WishView wishView)
        {
            Wishlist wishlist = new Wishlist();

            int? memberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]);

            wishlist.CustomerId = memberId;

            wishlist.PraductId = wishView.wId; // 賦值 ShowRawardId 給 ProductId

            if (ModelState.IsValid)
            {
                _context.Wishlists.Add(wishlist);
                _context.SaveChangesAsync();
                return "成";
            }
            return "敗";
        }

        // GET: Wishlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wishlists == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists.FindAsync(id);
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
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Wishlists == null)
            {
                return Problem("Entity set 'FinalProjectContext.Wishlist'  is null.");
            }
            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist != null)
            {
                _context.Wishlists.Remove(wishlist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(int id)
        {
            return (_context.Wishlists?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }

        public IActionResult GetWishlist()
        {
            var wishlists = _context.Wishlists
                .Join(
                    _context.ShowRawards,
                    w => w.PraductId,
                    sr => sr.ShowRawardId,
                    (w, sr) => new
                    {
                        w.CustomerId,
                        w.ItemId,
                        w.PraductId,
                        sr.ShowRawardId,
                        sr.Name,
                        sr.OneDrawMoney,
                        sr.CreateTime,
                        sr.Image,
                    }
                )
                .ToList();

            return Json(wishlists);
        }
    }
}

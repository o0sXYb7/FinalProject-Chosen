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
    public class TempStoragesController : Controller
    {
        private readonly FinalProjectContext _context;

        public TempStoragesController(FinalProjectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["MemberId"] == null)
            {
                return RedirectToAction("LoginCookieCheck", "Member"); // 導向登入頁面
            }

            int? memberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]); // 查當前登入的會員ID

            var tempStorages = _context.TempStorages
                .Where(t => t.Receive == false && t.MemberId == memberId)
                .Join(_context.ShowRawardItems,
                    t => t.PrizeId,
                    s => s.ShowRawardItemId,
                    (t, s) => new TempStoragesViewModel
                    {
                        TempStorageId = t.TempStorageId,
                        MemberId = t.MemberId,
                        PrizeName = t.PrizeName,
                        PrizeQuantity = t.PrizeQuantity,
                        ShowRawardName = s.Name, // 使用 JOIN 來連接 TempStorage 和 ShowRaward，取得 ShowRaward 的名稱
                        Deadline = t.Deadline,
                        TempStorage = t,
                    })
                .ToList();

            return View(tempStorages);
        }


        public async Task<IActionResult> IndexPartial_unReceive(bool? showReceived)
        {
            int? memberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]); // 查當前登入的會員ID

            var tempStorages = _context.TempStorages
                .Where(t => t.Receive == false && t.MemberId == memberId)
                .Join(_context.ShowRawards,
                    t => t.PrizePoolId,
                    s => s.ShowRawardId,
                    (t, s) => new TempStoragesViewModel
                    {
                        TempStorageId = t.TempStorageId,
                        MemberId = t.MemberId,
                        PrizeName = t.PrizeName,
                        PrizeQuantity = t.PrizeQuantity,
                        ShowRawardName = s.Name, // 使用 JOIN 來連接 TempStorage 和 ShowRaward，取得 ShowRaward 的名稱
                        Deadline = t.Deadline,
                        TempStorage = t,
                    })
                .ToList();

            return PartialView("_TempStoragesIndexPartial", tempStorages);
        }


        public async Task<IActionResult> IndexPartial_Receive(bool? showReceived)
        {
            int? memberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]); // 查當前登入的會員ID

            var tempStorages = _context.TempStorages
                .Where(t => t.Receive == true && t.MemberId == memberId)
                .Join(_context.ShowRawards,
                    t => t.PrizePoolId,
                    s => s.ShowRawardId,
                    (t, s) => new TempStoragesViewModel
                    {
                        TempStorageId = t.TempStorageId,
                        MemberId = t.MemberId,
                        PrizeName = t.PrizeName,
                        PrizeQuantity = t.PrizeQuantity,
                        ShowRawardName = s.Name, // 使用 JOIN 來連接 TempStorage 和 ShowRaward，取得 ShowRaward 的名稱
                        Deadline = t.Deadline,
                        TempStorage = t,
                    })
                .ToList();

            return PartialView("_TempStoragesIndexPartial", tempStorages);
        }

        // GET: TempStorages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TempStorages == null)
            {
                return NotFound();
            }

            var tempStorage = await _context.TempStorages
                .FirstOrDefaultAsync(m => m.TempStorageId == id);
            if (tempStorage == null)
            {
                return NotFound();
            }

            return View(tempStorage);
        }

        // GET: TempStorages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TempStorages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TempStorageId,MemberId,PrizePoolId,PrizeId,PrizeName,PrizeQuantity,PrizePicture,AwardDate,Deadline,Receive")] TempStorage tempStorage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tempStorage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tempStorage);
        }

        // GET: TempStorages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TempStorages == null)
            {
                return NotFound();
            }

            var tempStorage = await _context.TempStorages.FindAsync(id);
            if (tempStorage == null)
            {
                return NotFound();
            }
            return View(tempStorage);
        }

        // POST: TempStorages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TempStorageId,MemberId,PrizePoolId,PrizeId,PrizeName,PrizeQuantity,PrizePicture,AwardDate,Deadline,Receive")] TempStorage tempStorage)
        {
            if (id != tempStorage.TempStorageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tempStorage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TempStorageExists(tempStorage.TempStorageId))
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
            return View(tempStorage);
        }

        // GET: TempStorages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TempStorages == null)
            {
                return NotFound();
            }

            var tempStorage = await _context.TempStorages
                .FirstOrDefaultAsync(m => m.TempStorageId == id);
            if (tempStorage == null)
            {
                return NotFound();
            }

            return View(tempStorage);
        }

        // POST: TempStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TempStorages == null)
            {
                return Problem("Entity set 'FinalProjectContext.TempStorages'  is null.");
            }
            var tempStorage = await _context.TempStorages.FindAsync(id);
            if (tempStorage != null)
            {
                _context.TempStorages.Remove(tempStorage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TempStorageExists(int id)
        {
          return (_context.TempStorages?.Any(e => e.TempStorageId == id)).GetValueOrDefault();
        }

        public IActionResult CreateCommodity(int id)
        {
            var tempStorageData = _context.TempStorages.FirstOrDefault(t => t.TempStorageId == id);

            var viewModel = new TempStoragesViewModel()
            {   
                // 將TempStorage的資料存在ViewModel中
                TempStorageId = id, 
                PrizeName = tempStorageData.PrizeName,
                PrizeQuantity = tempStorageData.PrizeQuantity,
                MemberId = (int)tempStorageData.MemberId,
                Deadline = (DateTime)tempStorageData.Deadline,
                TempStorage = tempStorageData,
            };

            //return View(viewModel);
            return PartialView("_CreateCommodityPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCommodity([Bind("TempStorageId,MemberId,Commodity")] TempStoragesViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.TempStorage = _context.TempStorages.FirstOrDefault(t => t.TempStorageId == model.TempStorageId);
                model.PrizeName = model.TempStorage.PrizeName;
                model.PrizeQuantity = model.TempStorage.PrizeQuantity;
                //return RedirectToAction("CreateCommodity", new { id = model.TempStorageId, viewModel = model });
                return PartialView("CreateCommodity", new { id = model.TempStorageId, viewModel = model });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // 输出错误信息
                    Console.WriteLine(error.ErrorMessage);
                }
                // TODO: Return the view with the invalid model
                model.TempStorage = _context.TempStorages.FirstOrDefault(t => t.TempStorageId == model.TempStorageId);
                model.PrizeName = model.TempStorage.PrizeName;
                model.PrizeQuantity = model.TempStorage.PrizeQuantity;
                return View("CreateCommodity", model);
            }
        }

        [HttpPost]
        public IActionResult MarkReceived(int id)
        {
            var tempStorage = _context.TempStorages.Find(id);
            if (tempStorage == null)
            {
                return NotFound();
            }

            // message = "尚有處於上架狀態的一番賞，無法領取";

            if (_context.Commodities.Any(c => c.TempStorageId == id && (c.CommodityQuantity != 0 || (bool)c.OnShelves)))
            {
                
                return Json(new { success = false });
            }

            tempStorage.Receive = true;
            _context.SaveChanges();

            //message = "領取成功。";
            return Json(new { success = true });

        }

    }
}

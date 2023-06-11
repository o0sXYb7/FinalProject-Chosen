using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chosen.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Numerics;
using System.Drawing;

namespace chosen.Controllers
{
    public class CommoditiesController : Controller
    {
        private readonly FinalProjectContext _context;
        //private readonly yoyodbContext _context;

        public CommoditiesController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: Commodities
        public async Task<IActionResult> Index()
        {
            //return _context.Commodities != null ? 
            //            View(await _context.Commodities.ToListAsync()) :
            //            Problem("Entity set 'FinalProjectContext.Commodities'  is null.");
            // 若TempStorage中，相同TempStorageId的Receive為false 則不顯示。
            var commodities = await _context.Commodities
                .Where(c => c.TempStorageId != null)
                .Join(_context.TempStorages.Where(t => t.Receive == false),
                      c => c.TempStorageId, t => t.TempStorageId,
                      (c, t) => c)
                .ToListAsync();

            return View(commodities);
        }

        // GET: Commodities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Commodities == null)
            {
                return NotFound();
            }

            var commodity = await _context.Commodities
                .FirstOrDefaultAsync(m => m.CommodityId == id);
            if (commodity == null)
            {
                return NotFound();
            }

            return View(commodity);
        }

        // GET: Commodities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commodities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommodityId,TempStorageId,MemberId,CommodityName,CommodityQuantity,CommodityUnitPrice")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commodity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commodity);
        }

        // GET: Commodities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Commodities == null)
            {
                return NotFound();
            }

            var commodity = await _context.Commodities.FindAsync(id);
            if (commodity == null)
            {
                return NotFound();
            }
            //return View(commodity);

            var tempStorage = await _context.TempStorages.FirstOrDefaultAsync(t => t.TempStorageId == commodity.TempStorageId);
            if (tempStorage == null)
            {
                return NotFound();
            }

            var viewModel = new TempStoragesViewModel
            {
                TempStorageId = tempStorage.TempStorageId,
                PrizeName = tempStorage.PrizeName,
                PrizeQuantity = tempStorage.PrizeQuantity,
                TempStorage = tempStorage,
                Commodity = commodity,
            };

            //return View(viewModel);
            return PartialView("_EditCommodityPartial", viewModel);
        }

        // POST: Commodities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Commodity")] TempStoragesViewModel viewModel)
        {
            if (id != viewModel.Commodity.CommodityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var tempStorage = await _context.TempStorages.FindAsync(viewModel.Commodity.TempStorageId);
                    //var commodityNow = _context.Commodities.FirstOrDefault(t => t.TempStorageId == viewModel.Commodity.TempStorageId);
                    //var totalQuantity = tempStorage.PrizeQuantity + commodityNow.CommodityQuantity;
                    //var diff = viewModel.Commodity.CommodityQuantity - commodityNow.CommodityQuantity;
                    var tempStorage = await _context.TempStorages.FindAsync(viewModel.Commodity.TempStorageId);
                    var commodityNow = _context.Commodities.Find(viewModel.Commodity.CommodityId);
                    var totalQuantity = tempStorage.PrizeQuantity + commodityNow.CommodityQuantity;
                    var diff = viewModel.Commodity.CommodityQuantity - commodityNow.CommodityQuantity;

                    if (diff > 0)
                    {
                        //減少
                        tempStorage.PrizeQuantity -= (int)diff;

                    }
                    else
                    {
                        //增加
                        tempStorage.PrizeQuantity += (int)(-diff);
                    }

                    //_context.Update(viewModel.Commodity);
                    //await _context.SaveChangesAsync();
                    _context.Entry(commodityNow).State = EntityState.Detached;
                    _context.Update(viewModel.Commodity);
                    _context.Entry(viewModel.Commodity).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommodityExists(viewModel.Commodity.CommodityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["PartialViewName"] = "_CommoditiesIndexPartial";
                return RedirectToAction("Index", "TempStorages");
            }else
            {
                var tempStorage = await _context.TempStorages.FindAsync(viewModel.Commodity.TempStorageId);
                viewModel.PrizeName = tempStorage.PrizeName;
                viewModel.PrizeQuantity = tempStorage.PrizeQuantity;

                return PartialView("_EditCommodityPartial", viewModel);
            }
            //return PartialView("_EditCommodityPartial", viewModel);
        }

        // GET: Commodities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Commodities == null)
            {
                return NotFound();
            }

            var commodity = await _context.Commodities
                .FirstOrDefaultAsync(m => m.CommodityId == id);
            if (commodity == null)
            {
                return NotFound();
            }

            return View(commodity);
        }

        // POST: Commodities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Commodities == null)
            {
                return Problem("Entity set 'FinalProjectContext.Commodities'  is null.");
            }
            var commodity = await _context.Commodities.FindAsync(id);
            if (commodity != null)
            {
                _context.Commodities.Remove(commodity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommodityExists(int id)
        {
          return (_context.Commodities?.Any(e => e.CommodityId == id)).GetValueOrDefault();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCommodity(TempStoragesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // 搜尋資料庫中是否已有相同的TempId
                var existingShelf = await _context.Commodities.FirstOrDefaultAsync(s => s.TempStorageId == viewModel.Commodity.TempStorageId);
                if (existingShelf == null)
                {
                    //搜尋tempStorage資料庫中相同TempId的資料，若存在則減少數量。
                    var tempStorage = await _context.TempStorages.FindAsync(viewModel.Commodity.TempStorageId);
                    if (tempStorage != null)
                    {
                        // 減少Quantity數量
                        tempStorage.PrizeQuantity -= (int)viewModel.Commodity.CommodityQuantity;
                        tempStorage.Created = true;

                        // 將更改保存到資料庫中
                        _context.Update(tempStorage);
                    }

                    //保存商品數據到資料庫中
                    _context.Add(viewModel.Commodity);
                    await _context.SaveChangesAsync();
                    //return Json(new { success = true, message = "已成功上架此商品。" });
                    return Json(new { success = true });
                }
                else
                {
                    // 若已存在則顯示提示訊息
                    //TempData["Message"] = "已上架過此商品!，請至上架管理中修改。";
                    //return Json(new { success = false, message = "已上架過此商品!，請至上架管理中修改。" });
                    return Json(new { success = false });
                }
            }
            else
            {
                // 模型綁定失敗，輸出錯誤訊息
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine($"{key}: {error.ErrorMessage}");
                    }
                }

                var tempStorage = await _context.TempStorages.FindAsync(viewModel.Commodity.TempStorageId);
                viewModel.PrizeName = tempStorage.PrizeName;
                viewModel.PrizeQuantity = tempStorage.PrizeQuantity;

                return PartialView("_CreateCommodityPartial", viewModel);
            }
        }


        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SaveCommodity(Commodity Commodity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // 搜尋資料庫中是否已有相同的TempId
        //        var existingShelf = await _context.Commodities.FirstOrDefaultAsync(s => s.TempStorageId == Commodity.TempStorageId);
        //        if (existingShelf == null)
        //        {
        //            //搜尋tempStorage資料庫中相同TempId的資料，若存在則減少數量。
        //            var tempStorage = await _context.TempStorages.FindAsync(Commodity.TempStorageId);
        //            if (tempStorage != null)
        //            {
        //                // 減少Quantity數量
        //                tempStorage.PrizeQuantity -= (int)Commodity.CommodityQuantity;
        //                tempStorage.Created = true;

        //                // 將更改保存到資料庫中
        //                _context.Update(tempStorage);
        //            }

        //            //保存商品數據到資料庫中
        //            _context.Add(Commodity);
        //            await _context.SaveChangesAsync();
        //            //return Json(new { success = true, message = "已成功上架此商品。" });
        //            return PartialView("_CreateCommodityPartial", Commodity);
        //        }
        //        else
        //        {
        //            // 若已存在則顯示提示訊息
        //            //TempData["Message"] = "已上架過此商品!，請至上架管理中修改。";
        //            //return Json(new { success = false, message = "已上架過此商品!，請至上架管理中修改。" });
        //            return PartialView("_CreateCommodityPartial", Commodity);
        //        }
        //    }
        //    else
        //    {
        //        return PartialView("_CreateCommodityPartial", Commodity);
        //    }

        //    //return View(Commodity);
        //    //return PartialView("_CreateCommodityPartial", Commodity);
        //}


        [HttpPost]
        public IActionResult RemoveShelves(int id)
        {
            var commodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == id);

            if (commodity == null)
            {
                return NotFound();
            }

            var tempStorage = _context.TempStorages.FirstOrDefault(t => t.TempStorageId == commodity.TempStorageId);

            if (tempStorage == null)
            {
                return NotFound();
            }

            tempStorage.PrizeQuantity += commodity.CommodityQuantity ?? 0;
            commodity.CommodityQuantity = 0;
            commodity.OnShelves = false;

            _context.SaveChanges();

            return Json(new { success = true });
        }

        public IActionResult IndexPartial()
        {
            int? memberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]);

            var unReceivedItems = _context.Commodities
                .Where(c => c.TempStorageId != null && c.MemberId == memberId)
                .Join(_context.TempStorages.Where(t => t.Receive == false),
                    c => c.TempStorageId, t => t.TempStorageId,
                    (c, t) => new { Commodity = c, TempStorage = t })
                .Join(_context.ShowRawardItems,
                    ct => ct.TempStorage.PrizeId,
                    s => s.ShowRawardItemId,
                    (ct, s) => new CommoditiesViewModel
                    {
                        ShowRawardItemImage = s.Image,
                        TempStorage = ct.TempStorage,
                        Commodity = ct.Commodity
                    })
                .ToList();

            return PartialView("_CommoditiesIndexPartial", unReceivedItems);
        }

        public async Task<IActionResult> AuctionCenter()
        {
            
            return View();
        }

        public IActionResult GetCommodities()
        {
            var commodities = _context.Commodities
                .Where(c => c.OnShelves == true && c.Deadline > DateTime.Now)
                .Join(
                    _context.TempStorages,
                    c => c.TempStorageId,
                    t => t.TempStorageId,
                    (c, t) => new { Commodity = c, TempStorage = t }
                )
                .Join(
                    _context.ShowRawardItems,
                    temp => temp.TempStorage.PrizeId,
                    sr => sr.ShowRawardItemId,
                    (temp, sr) => new
                    {
                        temp.Commodity.CommodityId,
                        temp.Commodity.CommodityName,
                        temp.Commodity.CommodityUnitPrice,
                        temp.TempStorage,
                        temp.Commodity,
                        sr.Image,
                    }
                )
                .Join(
                    _context.ShowRawards,
                    temp => temp.TempStorage.PrizePoolId,
                    sr => sr.ShowRawardId,
                    (temp, sr) => new
                    {
                        temp.Commodity.CommodityId,
                        temp.Commodity.CommodityName,
                        temp.Commodity.CommodityUnitPrice,
                        temp.Image,
                        Deadline = temp.Commodity.Deadline,
                        sr.Series,
                        sr.FactoryId,
                        PrizeId = temp.TempStorage.PrizeId,
                    }
                )
                .Join(
                    _context.Factories,
                    sr => sr.FactoryId,
                    f => f.FactoryId,
                    (sr, f) => new
                    {
                        sr.CommodityId,
                        sr.CommodityName,
                        sr.CommodityUnitPrice,
                        sr.Image,
                        sr.Deadline,
                        sr.Series,
                        sr.PrizeId,
                        FactoryName = f.Name
                    }
                )
                .ToList();

            return Json(commodities);
        }

        public async Task<IActionResult> Ichiban()    
        {
            return View();
        }

        public IActionResult GetPrizePool()
        {
            var PrizePool = _context.ShowRawards
            .Where(s => s.IsOpen) // 過濾 IsOpen 為 true
            .Select(s => new {
                s.ShowRawardId,
                s.Name,
                //s.Firm,
                s.Image,
                s.Series,
                s.OneDrawMoney,
                s.FactoryId,
                s.CreateTime,
                TotalItems = s.ShowRawardItems.Sum(sri => sri.Num),
                RemainingItems = s.ShowRawardItems.Sum(sri => sri.LaveNum),
                //s.picture,
            })
            .Join(
                _context.Factories,
                s => s.FactoryId,
                fa => fa.FactoryId,
                (s, f) => new
                {
                    s.ShowRawardId,
                    s.Name,
                    s.Image,
                    s.Series,
                    s.OneDrawMoney,
                    s.CreateTime,
                    FactoryName = f.Name,
                    s.TotalItems,
                    s.RemainingItems,
                }

            )
            .ToList();

            return Json(PrizePool);
        }

        public async Task<IActionResult> Commodity(int? id)
        {
            //if (HttpContext.Request.Cookies["MemberId"] == null)
            //{
            //    return RedirectToAction("LoginCookieCheck", "Member"); // 導向登入頁面
            //}
            return View();
        }

        public IActionResult GetPrizeItems(int? id)
        {
            int? nowMemberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]);

            var prizeItems = _context.Commodities
                .Where(c => c.CommodityId == id)
                .Join(
                    _context.TempStorages,
                    c => c.TempStorageId,
                    t => t.TempStorageId,
                    (c, t) => new { Commodity = c, TempStorage = t }
                )
                .Join(
                    _context.ShowRawardItems,
                    temp => temp.TempStorage.PrizeId,
                    sri => sri.ShowRawardItemId,
                    (temp, sri) => new { temp.Commodity, temp.TempStorage, ShowRawardItem = sri }
                )
                .Join(
                    _context.ShowRawards,
                    temp => temp.ShowRawardItem.ShowRawardId,
                    sr => sr.ShowRawardId,
                    (temp, sr) => new
                    {
                        temp.Commodity.CommodityId,
                        temp.Commodity.CommodityName,
                        temp.Commodity.CommodityQuantity,
                        temp.Commodity.CommodityUnitPrice,
                        temp.Commodity.Deadline,
                        temp.Commodity.MemberId,
                        sr.Fare,
                        sr.Series,
                        sr.FactoryId,
                        //sr.Firm,
                        temp.ShowRawardItem.Image
                    }
                )
                .Join(
                    _context.Factories,
                    sr => sr.FactoryId,
                    fa => fa.FactoryId,
                    (sr, fa) => new
                    {
                        sr.CommodityId,
                        sr.CommodityName,
                        sr.CommodityQuantity,
                        sr.CommodityUnitPrice,
                        sr.Deadline,
                        sr.Fare,
                        sr.Series,
                        ShowRawardItemImage = sr.Image,
                        FactoryName = fa.Name,
                        Seller = sr.MemberId,
                        Buyer = nowMemberId,
                    }
                )
                .FirstOrDefault();

            return Json(prizeItems);
        }

        [HttpPost]
        public IActionResult TradePreSteps([FromBody] dynamic payload)
        {
            int? memberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]);
            int commodityId = payload.GetProperty("CommodityId").GetInt32();

            // 根据买家的memberId找到对应的MemberInfo数据
            MemberInfo buyer = _context.MemberInfos.FirstOrDefault(m => m.MemberId == memberId);

            if (buyer == null)
            {
                return Json(new { SUCCESS = 4 });
            }

            // 根据payload中的CommodityId找到对应的Commodity数据
            Commodity commodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == commodityId);

            // 根据TempStorageId找到对应的TempStorage数据
            TempStorage tempStorage = _context.TempStorages.FirstOrDefault(t => t.TempStorageId == commodity.TempStorageId);

            if (tempStorage == null)
            {
                return Json(new { SUCCESS = 0 });
            }

            //運費支付確認
            var tempStorageFilter = _context.TempStorages.Where(ts => ts.MemberId == memberId && ts.PrizePoolId == tempStorage.PrizePoolId);

            var receiveFalseData = tempStorageFilter.FirstOrDefault(ts => ts.Receive == false);

            if (receiveFalseData == null)
            {
                return Json(new { SUCCESS = 5 });
            }

            return Json(new { SUCCESS = 0 });
        }

        [HttpPost]
        public IActionResult CommodityTrade([FromBody] dynamic payload)
        {
            int? memberId = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]);
            int commodityId = payload.GetProperty("CommodityId").GetInt32();
            int quantity = payload.GetProperty("Quantity").GetInt32();

            // 根据payload中的CommodityId找到对应的Commodity数据
            Commodity commodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == commodityId);

            if (commodity == null)
            {
                return Json(new { SUCCESS = 0 });
            }

            int? sellerId = commodity.MemberId;
            int unitPrice = (commodity.CommodityUnitPrice ?? 0) * quantity;

            // 根据卖家的MemberId找到对应的MemberInfo数据
            MemberInfo seller = _context.MemberInfos.FirstOrDefault(m => m.MemberId == sellerId);
            if (seller == null)
            {
                return Json(new { SUCCESS = 0 });
            }

            // 根据买家的memberId找到对应的MemberInfo数据
            MemberInfo buyer = _context.MemberInfos.FirstOrDefault(m => m.MemberId == memberId);
            if (buyer == null)
            {
                return Json(new { SUCCESS = 4 });
            }

            // 檢查買家點數是否足夠
            if (buyer.Point < unitPrice)
            {
                return Json(new { SUCCESS = 2 });
            }

            // 開始事件，确保数据的完整性和一致性
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // 增加賣家點數
                seller.Point += unitPrice;

                // 減少買家點數
                buyer.Point -= unitPrice;

                // 更新CommodityQuantity (檢查商品是否足夠)
                if (commodity.CommodityQuantity < quantity)
                {
                    return Json(new { SUCCESS = 3 });
                }
                commodity.CommodityQuantity -= quantity;

                // 根据TempStorageId找到对应的TempStorage数据
                TempStorage tempStorage = _context.TempStorages.FirstOrDefault(t => t.TempStorageId == commodity.TempStorageId);

                if (tempStorage == null)
                {
                    return Json(new { SUCCESS = 0 });
                }

                // 根据TempStorageId找到对应的ShowRawards数据
                var showRaward = _context.ShowRawards.FirstOrDefault(sr => sr.ShowRawardId == tempStorage.PrizePoolId);

                // 创建新的TempStorage数据
                TempStorage newTempStorage = new TempStorage
                {
                    MemberId = memberId ?? 0,
                    PrizePoolId = tempStorage.PrizePoolId,
                    PrizeId = tempStorage.PrizeId,
                    PrizeName = tempStorage.PrizeName,
                    PrizeQuantity = quantity,
                    PrizePicture = tempStorage.PrizePicture,
                    AwardDate = tempStorage.AwardDate,
                    Deadline = tempStorage.Deadline,
                    Receive = false,
                    Created = false
                };

                // 创建新的TradeHistory数据
                TradeHistory newTradeHistory = new TradeHistory
                {
                    Seller = Convert.ToInt32(sellerId),
                    Buyer = memberId ?? 0,
                    CommodityId = commodity.CommodityId,
                    CommodityQuantity = quantity,
                    CommodityUnitPrice = Convert.ToInt32(commodity.CommodityUnitPrice),
                    TradeTime = DateTime.Now
                };

                //運費支付確認
                var tempStorageFilter = _context.TempStorages.Where(ts => ts.MemberId == memberId && ts.PrizePoolId == tempStorage.PrizePoolId);

                var receiveFalseData = tempStorageFilter.FirstOrDefault(ts => ts.Receive == false);

                if (receiveFalseData == null)
                {
                    DrawRecord DrawRecord = new DrawRecord
                    {
                        ShowRawardId = tempStorage.PrizePoolId,
                        FactoryId = showRaward.FactoryId,
                        MemberId = memberId ?? 0,
                        DrawCount = 0,
                        Point = showRaward.Fare,
                        DrawTime = DateTime.Now,
                        Settlement = false,
                        SettlementTime = null,
                    };

                    _context.DrawRecords.Add(DrawRecord);
                    _context.SaveChanges();
                    buyer.Point -= showRaward.Fare;
                }

                // 创建新的DrawRecord数据
                _context.TempStorages.Add(newTempStorage);
                _context.TradeHistories.Add(newTradeHistory);
                
                _context.SaveChanges();

                //// 更新卖家的积分
                //_context.MemberInfos.Update(seller);

                //// 更新买家的积分
                //_context.MemberInfos.Update(buyer);

                transaction.Commit();

                return Json(new { SUCCESS = 1 });

            }
            catch (Exception)
            {
                transaction.Rollback();
                return Json(new { SUCCESS = 0 });
            }
        }



    }
}

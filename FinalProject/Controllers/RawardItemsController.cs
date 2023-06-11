using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Controllers
{
    [Route("RawardItems/{action}/{id?}")]
    public class RawardItemsController : Controller
    {
        private readonly FinalProjectContext _context;

        public RawardItemsController(FinalProjectContext context)
        {
            _context = context;
        }

        // 畫面跳轉到指定RawardId
        [HttpGet]
        public IActionResult Index(int id)
        {
            if (HttpContext.Request.Cookies["EmployeeId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Loginview", "Login");
            }
        }

        // 進入該細項畫面後藉由路由位置抓取指定內容
        [HttpGet]
        public async Task<List<RawardItem>> getRawardItems(int id)
        {
            var rawardItems = await _context.RawardItems.Where(r => r.RawardId == id).ToListAsync();

            return rawardItems;
        }

        [HttpGet]
        public async Task<RawardItem> OPenModalToUpdate(int? id)
        {
            var rawardItems = await _context.RawardItems.FindAsync(id);

            return rawardItems;
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRawardItems(int id, [FromBody] RawardItem rawardItem)
        {
            ModelState.Remove("Raward");
            if (ModelState.IsValid)
            {
                RawardItem raw = await _context.RawardItems.FindAsync(id);
                raw.RawardId = rawardItem.RawardId;
                raw.RawardItemId = rawardItem.RawardItemId;
                raw.Name = rawardItem.Name;
                raw.RawardLevel = rawardItem.RawardLevel;
                raw.Num = rawardItem.Num;
                raw.IsJackpot = rawardItem.IsJackpot;
                raw.Image = rawardItem.Image;

                _context.Entry(raw).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawardItemExists(id))
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            else
            {
                // 如果模型验证失败，则返回错误消息
                return BadRequest(ModelState);
            }
        }

        // DELETE: RawardLibs/Delete/5
        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            var rawardItem = await _context.RawardItems.FindAsync(id);
            if (rawardItem == null)
            {
                return "noData";
            }

            try
            {
                _context.RawardItems.Remove(rawardItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "error";
            }

            return "success";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] RawardItem rawardItem)
        {
            ModelState.Remove("Raward");

            if (ModelState.IsValid)
            {
                RawardItem raw = new RawardItem();
                raw.RawardId = rawardItem.RawardId;
                raw.RawardItemId = rawardItem.RawardItemId;
                raw.Name = rawardItem.Name;
                raw.RawardLevel = rawardItem.RawardLevel;
                raw.Num = rawardItem.Num;
                raw.IsJackpot = rawardItem.IsJackpot;
                raw.Image = rawardItem.Image;

                try
                {
                    _context.Add(raw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawardItemExists(raw.RawardId))
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            else
            {
                // 如果模型验证失败，则返回错误消息
                return BadRequest(ModelState);
            }
        }

        private bool RawardItemExists(int id)
        {
          return (_context.RawardItems?.Any(e => e.RawardItemId == id)).GetValueOrDefault();
        }
    }
}

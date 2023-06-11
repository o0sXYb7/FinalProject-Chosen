using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.ViewModels;
using System.Xml.Linq;

namespace FinalProject.Controllers
{
    [Route("ShowRawardItem/{action}/{id?}")]
    public class ShowRawardItemsController : Controller
    {
        private readonly FinalProjectContext _context;

        public ShowRawardItemsController(FinalProjectContext context)
        {
            _context = context;
        }

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

        [HttpGet]
        public async Task<List<ShowRawardItem>> getShowItems(int id)
        {
            var showRawardItem = await _context.ShowRawardItems.Where(r => r.ShowRawardId == id).ToListAsync();

            return showRawardItem;
        }

        [HttpGet]
        public async Task<ShowRawardItem> OPenModalToUpdate(int? id)
        {
            var showRawardItem = await _context.ShowRawardItems.FindAsync(id);

            return showRawardItem;
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRawardItems(int id, [FromBody] ShowRawardItem showRawardItem)
        {
            ModelState.Remove("ShowRaward");
            if (ModelState.IsValid)
            {
                ShowRawardItem showItem = await _context.ShowRawardItems.FindAsync(id);
                showItem.ShowRawardId = showRawardItem.ShowRawardId;
                showItem.ShowRawardItemId = showRawardItem.ShowRawardItemId;
                showItem.Name = showRawardItem.Name;
                showItem.RawardLevel = showRawardItem.RawardLevel;
                showItem.Num = showRawardItem.Num;
                showItem.LaveNum = showRawardItem.LaveNum;
                showItem.IsJackpot = showRawardItem.IsJackpot;
                showItem.Image = showRawardItem.Image;

                _context.Entry(showItem).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowRawardItemExists(id))
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] ShowRawardItem showRawardItem)
        {
            showRawardItem.LaveNum = showRawardItem.Num;
            ModelState.Remove("ShowRaward");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(showRawardItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowRawardItemExists(showRawardItem.ShowRawardId))
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

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            var showRawardItems = await _context.ShowRawardItems.FindAsync(id);
            if (showRawardItems == null)
            {
                return "noData";
            }

            try
            {
                _context.ShowRawardItems.Remove(showRawardItems);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "error";
            }

            return "success";
        }

        //GET
        [HttpGet]
        public async Task<List<RawardLib>> getInsert()
        {
                var rawardLib = await _context.RawardLibs  
            .ToListAsync();

            return rawardLib;
        }

        public class str {
            public int id { get; set; }
            public int rawardId { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> insert([FromBody]str str)
        {
            var selectList = await _context.RawardItems
                .Where(c => c.RawardId == str.rawardId)
                .ToListAsync();

            if (selectList.Count() > 0)
            {
                foreach (var item in selectList)
                {
                    //var SelectRawardItem = _context.RawardItems.FirstOrDefault(c => c.RawardItemId == item.RawardItemId);
                    var ShowRawardItem = new ShowRawardItem()
                    {
                        ShowRawardId = str.id,
                        Name = item.Name,
                        RawardLevel = item.RawardLevel,
                        IsJackpot = item.IsJackpot,
                        Num = item.Num,
                        LaveNum = item.Num,
                        Image = item.Image,
                    };
                    _context.ShowRawardItems.Add(ShowRawardItem);
                }
                await _context.SaveChangesAsync();
                return $"{selectList.Count()}";
            }
            else
            {
                return "0";
            }

        }     

        private bool ShowRawardItemExists(int id)
        {
          return (_context.ShowRawardItems?.Any(e => e.ShowRawardItemId == id)).GetValueOrDefault();
        }
    }
}

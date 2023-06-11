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
    public class ShowRawardsController : Controller
    {
        private readonly FinalProjectContext _context;

        public ShowRawardsController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: ShowRawards
        public IActionResult Index()
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

        public async Task<IEnumerable<ShowRaward>> getShowRaward()
        {
            return _context.ShowRawards;
        }

        [HttpGet]
        public async Task<ShowRaward> OPenModalToUpdate(int? id)
        {
            var showRaward = await _context.ShowRawards.FindAsync(id);

            return showRaward;
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRawardLibs(int id, [FromBody] ShowRaward showRaward)
        {
            ModelState.Remove("Factory");
            //使用 AsNoTracking() 的方式可以告訴 Entity Framework Core 不要追蹤這個資料，這樣就不會產生 "The instance of entity type 'ShowRaward' cannot be tracked because another instance with the same key value for {'ShowRawardId'} is already being tracked." 的錯誤。
            var originalData = await _context.ShowRawards.AsNoTracking().SingleOrDefaultAsync(e => e.ShowRawardId == id);

            // 檢查需要鎖定的欄位的值是否與原始值相同
            if (originalData == null)
            {
                return NotFound();
            }
            if (showRaward.CreateTime != originalData.CreateTime)
            {
                ModelState.AddModelError(nameof(showRaward.CreateTime), "請勿修改建立時間");              
            }
            if (showRaward.NowProbability != originalData.NowProbability)
            {
                ModelState.AddModelError(nameof(showRaward.NowProbability), "請勿任意修改當前增加之機率");              
            }
            if (showRaward.HasSelectNumber != originalData.HasSelectNumber)
            {
                ModelState.AddModelError(nameof(showRaward.HasSelectNumber), "請勿任意修改當前已抽出之選號");
            }
            if (ModelState.IsValid)
            {            
                ShowRaward showRaw = await _context.ShowRawards.FindAsync(id);
                showRaw.ShowRawardId = showRaward.ShowRawardId;
                showRaw.Series = showRaward.Series;
                showRaw.Name = showRaward.Name;
                showRaw.FactoryId = showRaward.FactoryId;
                showRaw.AddProbability = showRaward.AddProbability;
                showRaw.NowProbability = showRaward.NowProbability;
                showRaw.AllowStoreDay = showRaward.AllowStoreDay;
                showRaw.Fare = showRaward.Fare;
                showRaw.CreateTime = showRaward.CreateTime;
                showRaw.OneDrawMoney = showRaward.OneDrawMoney;
                showRaw.IsOpen = showRaward.IsOpen;
                showRaw.Introduction = showRaward.Introduction;
                showRaw.Image = showRaward.Image;
                showRaw.HasSelectNumber = showRaward.HasSelectNumber;

                _context.Entry(showRaw).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowRawardExists(id))
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
        public async Task<IActionResult> Create([FromBody] ShowRaward showRaward)
        {
            // 強制將這些內容填上預設值
            showRaward.CreateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
            showRaward.NowProbability = 0;
            showRaward.HasSelectNumber = "";
            ModelState.Remove("Factory");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(showRaward);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowRawardExists(showRaward.ShowRawardId))
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
            var showRawards = await _context.ShowRawards.FindAsync(id);
            if (showRawards == null)
            {
                return "noData";
            }

            try
            {
                _context.ShowRawards.Remove(showRawards);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "error";
            }

            return "success";
        }   

        private bool ShowRawardExists(int id)
        {
          return (_context.ShowRawards?.Any(e => e.ShowRawardId == id)).GetValueOrDefault();
        }
    }
}

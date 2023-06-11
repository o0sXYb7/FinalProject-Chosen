using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace FinalProject.Controllers
{
    public class DrawRecordsController : Controller
    {
        private readonly FinalProjectContext _context;

        public DrawRecordsController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: DrawRecords
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

        [HttpGet]
        public IActionResult getRawardLibs()
        {
            var drawRecords = _context.DrawRecords
                .Join(
                    _context.ShowRawards,
                    c => c.ShowRawardId,
                    t => t.ShowRawardId,
                    (c, t) => new
                    {
                        c.DrawId,
                        c.FactoryId,
                        c.MemberId,
                        c.DrawCount,
                        c.DrawTime,
                        c.Point,
                        c.Settlement,
                        c.SettlementTime,
                        c.ShowRawardId,
                        ShowRawardName = t.Name
                    }
                )
                .Join(
                    _context.MemberInfos,
                    c => c.MemberId,
                    t => t.MemberId,
                    (c, t) => new
                    {
                        c.DrawId,
                        c.FactoryId,
                        c.DrawCount,
                        c.DrawTime,
                        c.Point,
                        c.Settlement,
                        c.SettlementTime,
                        c.ShowRawardName,
                        c.ShowRawardId,
                        c.MemberId,
                        t.MemberName,
                    }
                )
                .Join(
                    _context.Factories,
                    c => c.FactoryId,
                    t => t.FactoryId,
                    (c, t) => new
                    {
                        c.DrawId,
                        c.DrawCount,
                        c.DrawTime,
                        c.Point,
                        c.Settlement,
                        c.SettlementTime,
                        c.ShowRawardName,
                        c.MemberName,
                        c.MemberId,
                        c.ShowRawardId,
                        c.FactoryId,
                        FactoryName = t.Name
                    }
                );

            return Json(drawRecords);
        }

        public class drawR
        {
            public int DrawId { get; set; }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRawardLibs([FromBody] drawR drawR)
        {
            DrawRecord dra = await _context.DrawRecords.FindAsync(drawR.DrawId);
            dra.Settlement = true;
            dra.SettlementTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Entry(dra).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrawRecordExists(drawR.DrawId))
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { success = "ok" });
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
            var drawRecords = await _context.DrawRecords.FindAsync(id);
            if (drawRecords == null)
            {
                return "noData";
            }

            try
            {
                _context.DrawRecords.Remove(drawRecords);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "error";
            }

            return "success";
        }

        private bool DrawRecordExists(int id)
        {
            return (_context.DrawRecords?.Any(e => e.DrawId == id)).GetValueOrDefault();
        }
    }
}

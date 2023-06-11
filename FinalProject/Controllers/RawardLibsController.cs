using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Controllers
{
    public class RawardLibsController : Controller
    {
        private readonly FinalProjectContext _context;

        public RawardLibsController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: RawardLibs
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

        public async Task<IEnumerable<RawardLib>> getRawardLibs()
        {
            return _context.RawardLibs;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] RawardLib rawardLib)
        {
            ModelState.Remove("RawardItems");
            if (ModelState.IsValid)
            {              
                try
                {
                    _context.Add(rawardLib);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawardLibExists(rawardLib.RawardId))
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

        [HttpGet]
        public async Task<RawardLib> OPenModalToUpdate(int? id)
        {
            var rawardLib = await _context.RawardLibs.FindAsync(id);

            return rawardLib;
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRawardLibs(int id,[FromBody] RawardLib rawardLib)
        {
            if (ModelState.IsValid)
            {
                RawardLib raw = await _context.RawardLibs.FindAsync(id);
                raw.RawardId = rawardLib.RawardId;
                raw.Series = rawardLib.Series;
                raw.Name = rawardLib.Name;

                _context.Entry(raw).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawardLibExists(id))
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
            var rawardLib = await _context.RawardLibs.FindAsync(id);
            if (rawardLib == null)
            {
                return "noData";
            }

            try
            {
                _context.RawardLibs.Remove(rawardLib);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "error";
            }

            return "success";
        }

        private bool RawardLibExists(int id)
        {
          return (_context.RawardLibs?.Any(e => e.RawardId == id)).GetValueOrDefault();
        }
    }
}

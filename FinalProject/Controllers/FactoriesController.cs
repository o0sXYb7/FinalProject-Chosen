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
    public class FactoriesController : Controller
    {
        private readonly FinalProjectContext _context;

        public FactoriesController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: Factories
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

        public async Task<IEnumerable<Factory>> getShowRaward()
        {
            return _context.Factories != null ?
                          _context.Factories :
                          null;
        }

        [HttpGet]
        public async Task<Factory> OPenModalToUpdate(int? id)
        {
            var factory = await _context.Factories.FindAsync(id);

            return factory != null ? factory : null;
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRawardLibs(int id, [FromBody] Factory factory)
        {
            if (ModelState.IsValid)
            {
                Factory faty = await _context.Factories.FindAsync(id);
                faty.FactoryId = id;
                faty.Name = factory.Name;
                faty.Phone = factory.Phone;
                faty.ContactPerson = factory.ContactPerson;

                _context.Entry(faty).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactoryExists(id))
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
        public async Task<IActionResult> Create([FromBody] Factory factory)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(factory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactoryExists(factory.FactoryId))
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
            var factories = await _context.Factories.FindAsync(id);
            if (factories == null)
            {
                return "noData";
            }

            try
            {
                _context.Factories.Remove(factories);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return "error";
            }

            return "success";
        }

        // 提供給其他 View select 的內容
        [HttpGet]
        public async Task<List<Factory>> getFactory()
        {
            var factories = await _context.Factories
        .ToListAsync();

            return factories;
        }
        private bool FactoryExists(int id)
        {
          return (_context.Factories?.Any(e => e.FactoryId == id)).GetValueOrDefault();
        }
    }
}

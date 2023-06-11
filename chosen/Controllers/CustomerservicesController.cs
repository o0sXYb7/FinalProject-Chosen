using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chosen.Models;
using chosen.ViewModel;

namespace chosen.Controllers
{
    public class CustomerservicesController : Controller
    {
        private readonly FinalProjectContext _context;

        public CustomerservicesController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: Customerservices
        public async Task<IActionResult> Index()
        {
              return _context.Customerservices != null ? 
                          View(await _context.Customerservices.ToListAsync()) :
                          Problem("Entity set 'ProductContext.Customerservice'  is null.");
        }

        public async Task<IActionResult> QuestionFilter(string className)
        {
            var query = _context.Customerservices.AsQueryable();
            if (!string.IsNullOrEmpty(className))
            {
                query = query.Where(q => q.Class == className);
            }
            var results = await query.ToListAsync();
            return PartialView("_CustomerservicePartial", results);
        }

        //問題回報表格
        [HttpPost]
        public IActionResult SubmitQuestion(SubmitQuestionViewModel model)
        {

            using (var context = new FinalProjectContext())
            {
                // 获取ViewModel中的属性值
                string selectedQuestionType = model.QuestionType;
                string questionTitle = model.QuestionTitle;


                // 將資料寫進QuestionReport資料庫裡
                var questionReport = new QuestionReport
                {
                    MemberId = 1, // 设置成适当的MemberId
                    QuestionTitle = questionTitle,
                    QuestionType = selectedQuestionType,
                    DateTime = DateTime.Now, // 设置成适当的日期时间
                    State = "未回覆" // 设置适当的状态
                };

                // 添加QuestionReport到上下文中
                context.QuestionReports.Add(questionReport);

                // 保存更改到数据库
                context.SaveChanges();


                QuestionHistoryViewModel history = new QuestionHistoryViewModel();
                history.QuestionReportId = questionReport.QuestionReportId;
                history.Message = model.QuestionhistoriesList.OrderBy(d => d.DateTimeSecond).Select(m => m.Message).First(); // 设置 Message 的值

                model.QuestionhistoriesList.Add(history);

                // 將資料寫進 QuestionHistory資料庫裡
                var questionhistory = new QuestionHistory
                {
                    Message = history.Message,
                    QuestionReportId = history.QuestionReportId, // 设置成适当的MemberId
                    WhoAnswer = 1,
                    DateTimeSecond = DateTime.Now // 设置成适当的日期时间                   
                };

                // 添加QuestionHistory到上下文中
                context.QuestionHistories.Add(questionhistory);

                // 保存更改到数据库
                context.SaveChanges();

                // 返回成功的响应给前端，表示问题已成功存入数据库
                return Ok();
            }

        }
        // GET: Customerservices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customerservices == null)
            {
                return NotFound();
            }

            var customerservice = await _context.Customerservices
                .FirstOrDefaultAsync(m => m.CustomerserviceId == id);
            if (customerservice == null)
            {
                return NotFound();
            }

            return View(customerservice);
        }

        // GET: Customerservices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customerservices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerserviceID,Class,QuestionTitle,AnswerTitle")] Customerservice customerservice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerservice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerservice);
        }

        // GET: Customerservices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customerservices == null)
            {
                return NotFound();
            }

            var customerservice = await _context.Customerservices.FindAsync(id);
            if (customerservice == null)
            {
                return NotFound();
            }
            return View(customerservice);
        }

        // POST: Customerservices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerserviceID,Class,QuestionTitle,AnswerTitle")] Customerservice customerservice)
        {
            if (id != customerservice.CustomerserviceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerservice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerserviceExists(customerservice.CustomerserviceId))
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
            return View(customerservice);
        }

        // GET: Customerservices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customerservices == null)
            {
                return NotFound();
            }

            var customerservice = await _context.Customerservices
                .FirstOrDefaultAsync(m => m.CustomerserviceId == id);
            if (customerservice == null)
            {
                return NotFound();
            }

            return View(customerservice);
        }

        // POST: Customerservices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customerservices == null)
            {
                return Problem("Entity set 'ProductContext.Customerservice'  is null.");
            }
            var customerservice = await _context.Customerservices.FindAsync(id);
            if (customerservice != null)
            {
                _context.Customerservices.Remove(customerservice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerserviceExists(int id)
        {
          return (_context.Customerservices?.Any(e => e.CustomerserviceId == id)).GetValueOrDefault();
        }
    }
}

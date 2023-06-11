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
    public class QuestionHistoryController : Controller
    {
        private readonly FinalProjectContext _context;

        public QuestionHistoryController(FinalProjectContext context)
        {
            _context = context;
        }


        [HttpPost]
        // GET: 
        public IActionResult Index(int questionReportId, string message, int whoanswer, string datetimesec)
        {
            // 根据questionReportId找到对应的QuestionReport对象
            var questionReport = _context.QuestionHistories.FirstOrDefault(q => q.QuestionReportId == questionReportId);

            if (questionReport == null)
            {
                return NotFound(); // 如果找不到相应的QuestionReport，返回404错误
            }

            // 更新QuestionDescription属性
            questionReport.Message = message;
        
            _context.SaveChanges(); // 保存更改

            return Ok(); // 返回成功响应
            //return View();
        }

        public IActionResult SubmitQuestion(string questionTitle, string message, string questionType, string DateTime)
        {
            //using (var context = new FinalProjectContext())
            //{
            //    var questionReport = new QuestionReport
            //    {
            //        QuestionTitle = questionTitle,
            //        QuestionType = questionType,
            //        DateTime = DateTime.Parse(DateTime),
            //    };

            //    设置其他属性的值（例如MemberId和State）

            //    var questionHistory = new QuestionHistory
            //    {
            //        Message = message,
            //        WhoAnswer = null, // 设置为适当的值
            //        QuestionReport = questionReport
            //    };

            //    context.QuestionReports.Add(questionReport);
            //    context.QuestionHistories.Add(questionHistory);
            //    context.SaveChanges();

            //    返回一個適當的回應給前端，表示問題已成功傳回資料庫
            //    return Ok();
            //}
            return View();
        }

        [HttpPost]
        public ActionResult StoreQuestionID(string questionID)
        {
            // 删除旧的 QuestionID
            TempData.Remove("QuestionID");

            // 存储新的 QuestionID
            TempData["QuestionID"] = questionID;

            return Json(new { success = true });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chosen.Models;
using Newtonsoft.Json;

namespace chosen.Controllers
{
    public class QuestionReportController : Controller
    {
        private readonly FinalProjectContext _context;

        public QuestionReportController(FinalProjectContext context)
        {
            _context = context;
        }

        [HttpPost]
        // GET: 
        public IActionResult  Index(int questionReportId, string questionDescription)
        {
            //// 根据questionReportId找到对应的QuestionReport对象
            //var questionReport = _context.QuestionReports.FirstOrDefault(q => q.QuestionReportId == questionReportId);

            //if (questionReport == null)
            //{
            //    return NotFound(); // 如果找不到相应的QuestionReport，返回404错误
            //}

            //// 更新QuestionDescription属性
            //questionReport.QuestionDescription = questionDescription;

            //// 执行其他必要的操作，例如数据验证和保存更改
            //// ...

            //_context.SaveChanges(); // 保存更改

            //return Ok(); // 返回成功响应
            return View();
        }

        
    }
}

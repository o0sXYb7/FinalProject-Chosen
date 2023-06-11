using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.Domain;
using FinalProject.Dtos.webhook;
using NuGet.Protocol;
using Newtonsoft.Json;
using FinalProject.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers
{
    public class LineBotsController : Controller
    {
        private readonly FinalProjectContext _context;
        private readonly LineBotService _lineBotService;

        public LineBotsController(FinalProjectContext context)
        {
            _context = context;
            _lineBotService = new LineBotService();
        }

        //[HttpPost]
        //public IActionResult Webhook(WebhookRequestBodyDto body)
        //{
        //    // 接收回傳訊息用
        //    _lineBotService.ReceiveWebhook(body);

        //    return Ok();
        //}

        // GET: LineBots
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

        public async Task<IEnumerable<LineBot>> getRawardLibs()
        {
            return _context.LineBots;
        }

        [HttpGet]
        public async Task<LineBot> OPenModalToUpdate(int? id)
        {
            var lineBot = await _context.LineBots.FindAsync(id);

            return lineBot;
        }

        //public class LineObj {
        //    [Required(ErrorMessage = "請輸入系列名稱")]
        //    public string messageType { get; set; }

        //    // C# 可以接收 object 格式
        //    public object Messages { get; set; }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] LineBotObj lineObj)
        {
            var lineBot = new LineBot();
            lineBot.SendTime = DateTime.Now;
            lineBot.MessageType = lineObj.messageType;
            dynamic messageObj = JsonConvert.DeserializeObject(lineObj.Messages.ToString());
            lineBot.Message = messageObj.Messages[0].Text;

            if (messageObj.Messages[0].Text == "")
            {
                ModelState.AddModelError(nameof(LineBotObj.Messages), "請輸入內容");
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(lineBot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineBotExists(lineBot.LineBotId))
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        throw;
                    }
                }
                _lineBotService.BroadcastMessageHandler(lineObj.messageType, lineObj.Messages);
                return Ok();
            }
            else
            {
                // 如果模型验证失败，则返回错误消息
                return BadRequest(ModelState);
            }
        }

        private bool LineBotExists(int id)
        {
          return (_context.LineBots?.Any(e => e.LineBotId == id)).GetValueOrDefault();
        }
    }
}

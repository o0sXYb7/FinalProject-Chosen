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
    public class ReturnRecordController : Controller
    {
        private readonly FinalProjectContext _context;
        public ReturnRecordController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: ReturnRecord
        public async Task<IActionResult> Index()
        {
            //假設會員
            int memberId = 1;
            // 使用條件表達式篩選會員ID等於指定會員ID的數據
            var data = await _context.QuestionReports.Include(qr=>qr.QuestionHistories).Where(s => s.MemberId == memberId).OrderBy(d => d.DateTime).ToListAsync();
            List<SubmitQuestionViewModel> sqtemp = new List<SubmitQuestionViewModel>();
            for(int i =0 ; i < data.Count; i++)
            {
                SubmitQuestionViewModel temp = new SubmitQuestionViewModel();

                temp.QuestionReportID = data[i].QuestionReportId;            
                temp.MemberID = data[i].MemberId;
                temp.QuestionTitle = data[i].QuestionTitle;
                temp.QuestionType = data[i].QuestionType;
                temp.QuestionDate = (DateTime)data[i].DateTime;
                temp.State = data[i].State;
                for(int j =0; j < data[i].QuestionHistories.Count; j++)
                {
                    QuestionHistoryViewModel qhtemp = new QuestionHistoryViewModel();
                    qhtemp.QuestionHistoryId = data[i].QuestionHistories.Select(qh => qh.QuestionHistoryId).Skip(j).First();
                    qhtemp.Message = data[i].QuestionHistories.Select(qh=>qh.Message).Skip(j).First();
                    qhtemp.DateTimeSecond = data[i].QuestionHistories.Select(qh => qh.DateTimeSecond).Skip(j).First();
                    qhtemp.WhoAnswer = (int)data[i].QuestionHistories.Select(qh => qh.WhoAnswer).Skip(j).First();
                    temp.QuestionhistoriesList.Add(qhtemp);
                }
                sqtemp.Add(temp);
            }
            Console.WriteLine(sqtemp);
            return View(sqtemp);
        }

        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody] QuestionHistoryViewModel data)
        {
            // 獲取文本區域的值
            string inputMessage = data.Message;
            // 創建QuestionHistory模型實例並保存到數據庫
            QuestionHistory questionHistory = new ()
            {
               
                QuestionReportId = data.QuestionReportId,
                Message = inputMessage, // 使用從前端獲取的值
                DateTimeSecond = DateTime.Now, // 設置為當前時間
                WhoAnswer = 1
            };

            // 將QuestionHistory保存到數據庫
            _context.QuestionHistories.Add(questionHistory);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

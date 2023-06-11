using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly FinalProjectContext _context;

        public LoginController(FinalProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Loginview()
        {
            HttpContext.Response.Cookies.Delete("EmployeeId");
            return View();
        }

        public IActionResult LoginCookieCheck()
        {
            if (HttpContext.Request.Cookies["EmployeeId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Loginview", "Login");
        }

        [HttpPost]
        public IActionResult Login(string employeeName, string password)
        {
            
            // 根據輸入的員工名稱和密碼進行資料庫查詢
            var employee = _context.Employees.FirstOrDefault(e =>
                e.EmployeeName == employeeName && e.Password == password);

            if (employee != null)
            {
                int EmployeeId = employee.EmployeeId;
                // 創建一個新的 Cookie，將員工ID存儲其中
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(2) // 設置 Cookie 的過期時間
                };
                HttpContext.Response.Cookies.Append("EmployeeId", EmployeeId.ToString(), cookieOptions);

                // 登入成功
                return Json(new { success = true }); // 返回成功結果
            }
            else
            {
                // 登入失敗，返回相應的錯誤訊息給檢視
                return Json(new { success = false }); // 返回失敗結果
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("EmployeeId"); // 刪除名為 "EmployeeId" 的 Cookie

            // 執行其他登出操作（例如重定向到登錄頁面）
            return RedirectToAction("Loginview", "Login");
        }
    }
}

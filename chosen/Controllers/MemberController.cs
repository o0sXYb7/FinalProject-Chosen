using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using chosen.Models;
using System.Security.Cryptography;
using MailKit.Security;
using MimeKit;
using Microsoft.AspNetCore.Authentication;
using System.Text.RegularExpressions;


namespace chosen.Controllers
{
    public class MemberController : Controller
    {
        private readonly IConfiguration _config;

        private readonly FinalProjectContext _context;

        public MemberController(IConfiguration config, FinalProjectContext context)
        {
            _config = config;
            _context = context;
        }


        public class EncryptionHelper
        {
            public static string GenerateSaltedHash(string input, string salt)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
                using (SHA256Managed sha256 = new SHA256Managed())
                {
                    byte[] hashedBytes = sha256.ComputeHash(bytes);
                    return Convert.ToBase64String(hashedBytes);
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }
               
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LoginCookieCheck()
        {
            if (HttpContext.Request.Cookies["MemberId"] != null)
            {
                return RedirectToAction("MemberInfo", "Member");
            }
            return View("Login", "Member");
        }

        [HttpGet]
        public JsonResult LoginCheckforLayout()
        {
            bool isLoggedIn = HttpContext.Request.Cookies["MemberId"] != null;

            return Json(new { isLoggedIn });
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("MemberId"); // 刪除名為 "MemberId" 的 Cookie

            // 執行其他登出操作（例如重定向到登錄頁面）
            return RedirectToAction("LoginCookieCheck", "Member");
        }



        public IActionResult ResetPasswordSuccess()
        {
            return View();
        }
        public IActionResult ValidGoogleLogin()
        {
            return View();
        }
        public IActionResult InvalidGoogleLogin()
        {
            return View();
        }

        public IActionResult MemberInfo()
        {
            return View();
        }

        //獲取並傳遞會員資料至前端
        [HttpGet]
        public IActionResult GetMemberData()
        {
            int? memberId;
            if (int.TryParse(HttpContext.Request.Cookies["MemberId"], out int parsedMemberId))
            {
                memberId = parsedMemberId;
            }
            else
            {
                // 如果 Cookie 中的 MemberId 無法轉換為整數，返回適當的錯誤或轉向頁面
                return NotFound();
            }

            var member = _context.MemberInfos.Find(memberId);

            if (member == null)
            {
                // 如果找不到對應的會員資料，返回適當的錯誤或轉向頁面
                return NotFound();
            }

            // 從資料庫中獲取該會員的資料

            // 返回 JSON 格式的會員資料
            return Json(member);

        }

        //將修改後的會員資料保存至資料庫
        [HttpPost]
        public IActionResult EditMemberData([FromBody] MemberInfo inModel)
        {  
            if (ModelState.IsValid)
            {
                // 取得目前的會員資料
                var currentMemberInfo = _context.MemberInfos.Find(inModel.MemberId);

                if (currentMemberInfo == null)
                {
                    return NotFound();
                }

                // 更新會員資料
                currentMemberInfo.Email = inModel.Email;
                currentMemberInfo.MemberName = inModel.MemberName;
                currentMemberInfo.Phone = inModel.Phone;
                currentMemberInfo.Address = inModel.Address;

                // 儲存變更
                _context.SaveChanges();

                return Json(new { Changesuccess = true }); // 返回成功結果

            }
            else
            {
                return Json(new { Changefail = true }); // 返回失敗結果

            }


        }



        //執行註冊
        [HttpPost]
        public IActionResult DoRegister(MemberInfo inModel)
        {
                try
                {
                    //檢查欲註冊名稱或電郵是否已被註冊
                    bool isMemberNameExists = _context.MemberInfos.Any(r => r.MemberName == inModel.MemberName);
                    bool isEmailExists = _context.MemberInfos.Any(r => r.Email == HttpContext.Session.GetString("Email"));

                //檢查資料是否未填
                if (string.IsNullOrEmpty(inModel.MemberName) || string.IsNullOrEmpty(inModel.Password) ||  string.IsNullOrEmpty(inModel.Phone))
                {
                    return Json(new { somethingisnull = true }); // 返回失敗結果

                }
                
                //檢查密碼是否合法
                if (inModel.Password.Length < 8 || !Regex.IsMatch(inModel.Password, @"^(?=.*[a-zA-Z])(?=.*\d).{8,}$"))
                {
                    // Password 不符合條件
                    return Json(new { invalidpassword = true }); // 返回失敗結果
                }

                //檢查手機是否合法
                if (!Regex.IsMatch(inModel.Phone, @"^09\d{8}$"))
                {
                    // Phone 不符合條件
                    return Json(new { invalidphone = true }); // 返回失敗結果
                }

                else if (isMemberNameExists==true||isEmailExists==true)
                {
                    return Json(new { NameorEmailExisted = true }); // 返回失敗結果

                }
                else
                    {
                        // 檢查收到的驗證碼是否與 Session 中的驗證碼相符
                        int? verificationCode = HttpContext.Session.GetInt32("VerificationCode");
                        string email = HttpContext.Session.GetString("Email");
                        string expirationTime = HttpContext.Session.GetString("ExpirationTime");

                        if (verificationCode == null || DateTime.Now > DateTime.Parse(expirationTime))
                        {
                            return Json(new { verificationCodeisexpired = true }); // 返回失敗結果
                        }

                        // 將密碼使用 SHA256 雜湊運算(不可逆)
                        string salt = HttpContext.Session.GetString("Email").Substring(0, 1).ToLower(); 
                        //使用信箱前一碼當作密碼鹽
                        SHA256 sha256 = SHA256.Create();
                        byte[] bytes = Encoding.UTF8.GetBytes(salt + inModel.Password); //將密碼鹽及原密碼組合
                        byte[] hash = sha256.ComputeHash(bytes);
                        string newPwd = string.Concat(hash.Select(b => b.ToString("X2"))); // 雜湊運算後密碼

                        // 註冊資料新增至資料庫
                        MemberInfo registerInfo = new MemberInfo
                        {
                            MemberName = inModel.MemberName,
                            Password = newPwd,
                            Phone = inModel.Phone,
                            Email = HttpContext.Session.GetString("Email"),
                            RegisterDate = DateTime.Now,
                        };

                        _context.MemberInfos.Add(registerInfo);
                        _context.SaveChanges();

                        return Json(new { success = true }); // 返回成功結果
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

        }

        [HttpPost]
        public int SendVerificationCode(string email)
        {
            // 檢查郵箱是否已存在於資料庫
            bool isEmailExist = _context.MemberInfos.Any(r => r.Email == email);
            if (isEmailExist)
            {
                // 郵箱已存在，返回錯誤碼或訊息給前端，讓前端顯示提示
                return -1; 
            }
            else if(email==null)
            {
                //未輸入郵箱
                return -2;
            }
            else
            {
                // 讀取 email 設定
                var smtpServer = _config["EmailSettings_Google:SmtpServer"];
                var smtpPort = int.Parse(_config["EmailSettings_Google:SmtpPort"]);
                var smtpUsername = _config["EmailSettings_Google:SmtpUsername"];
                var smtpPassword = _config["EmailSettings_Google:SmtpPassword"];

                // 產生驗證碼
                Random random = new Random();
                int verificationCode = random.Next(100000, 999999);

                // 存入 Session
                HttpContext.Session.SetInt32("VerificationCode", verificationCode);
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("ExpirationTime", DateTime.Now.AddMinutes(5).ToString());

                // 發送驗證信
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("天選者", smtpUsername));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "[天選者]註冊驗證";
                message.Body = new TextPart("plain")
                {
                    Text = $"感謝您註冊天選者。您的驗證碼為{verificationCode}"
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                    client.Authenticate(smtpUsername, smtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return verificationCode;
            }
            
        }

        [HttpPost]
        public IActionResult Login([FromBody] MemberInfo inModel)
        {
            if(inModel.Email == ""||inModel.Password =="")
            {
                return Json(new { success = false });
            }
            else
            {
                // 根據您的業務邏輯從資料庫中獲取使用者的加密密碼
                string salt = inModel.Email.Substring(0, 1).ToLower(); // 使用帳號前一碼當作密碼鹽
                SHA256 sha256 = SHA256.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(salt + inModel.Password); // 將密碼鹽及原密碼組合
                byte[] hash = sha256.ComputeHash(bytes);
                string encryptedPassword = string.Concat(hash.Select(b => b.ToString("X2"))); // 雜湊運算後密碼

                // 使用LINQ進行使用者驗證
                var user = _context.MemberInfos.FirstOrDefault(u => u.Email == inModel.Email && u.Password == encryptedPassword);

                if (user != null)
                {
                    int memberId = user.MemberId;
                    // 創建一個新的 Cookie，將會員ID存儲其中
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddHours(12) // 設置 Cookie 的過期時間
                    };
                    HttpContext.Response.Cookies.Append("MemberId", memberId.ToString(), cookieOptions);

                    // 登入成功
                    return Json(new { success = true }); // 返回成功結果
                }

                else
                {
                    // 登入失敗
                    return Json(new { success = false }); // 返回成功結果
                }
            }
            
        }
                
        //寄送含重置密碼連結的驗證信
        [HttpPost]
        public IActionResult SendResetPasswordEmail([FromBody] MemberInfo inModel)
        {
            // 檢查郵箱是否已存在於資料庫
            bool isEmailExist = _context.MemberInfos.Any(r => r.Email == inModel.Email);
            if (isEmailExist)
            {
                // 產生重置碼（或使用使用者的ID）

                Random random = new Random();
                string resetCode = random.Next(100000, 999999).ToString();

                // 將加密後的重置碼存入資料庫，關聯到使用者的郵箱
                var registerInfo = _context.MemberInfos.FirstOrDefault(r => r.Email == inModel.Email);
                string salt = "yoyo";

                string encryptedResetCode = EncryptionHelper.GenerateSaltedHash(resetCode.ToString(), salt);
                if (registerInfo != null)
                {
                    registerInfo.EncryptedResetCode =encryptedResetCode ;
                    registerInfo.ResetDateTime = DateTime.Now;
                    _context.SaveChanges();
                };

                // 構建重置密碼連結(於連結尾段加上加密過的重置碼)
                var resetLink = Url.Action("ResetPassword", "Member", new { code = encryptedResetCode }, Request.Scheme);
                              

                // 讀取 email 設定
                var smtpServer = _config["EmailSettings_Google:SmtpServer"];
                var smtpPort = int.Parse(_config["EmailSettings_Google:SmtpPort"]);
                var smtpUsername = _config["EmailSettings_Google:SmtpUsername"];
                var smtpPassword = _config["EmailSettings_Google:SmtpPassword"];

                // 發送郵件
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("天選者", smtpUsername));
                message.To.Add(new MailboxAddress("", inModel.Email));
                message.Subject = "[天選者]重置密碼";
                message.Body = new TextPart("plain")
                {
                    Text = $"請點擊以下連結以重置您的密碼：\n{resetLink}"
                };

                // 以下為郵件發送的程式碼，保持不變

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                    client.Authenticate(smtpUsername, smtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return Json(new { success = true });
            }
            else
            {
                //查無該郵箱
                return Json(new { success = false });
            }
        }

        //按下連結後，前端呼叫該方法驗證連結是否有效
        [HttpGet]
        public IActionResult ResetPassword(string code)
        {

            // 在資料庫比對連結中的加密重置碼
            bool isValidCode = _context.MemberInfos.Any(r => r.EncryptedResetCode == code);
                if (isValidCode)
            {
                    // 將使用者的郵箱和重置碼儲存在 TempData，以便在提交新密碼時使用
                    TempData["encryptedResetCode"] = code;
                
                return View("ResetPasswordPending"); // 返回重置密碼的視圖
            }

            // 重置碼無效，返回錯誤視圖或其他處理方式
            return View("ResetPasswordError");
        }

        
        [HttpPost]
        public IActionResult ChangePassword([FromBody] MemberInfo inModel)
        {
            try
            {
                
                // 从 TempData 中获取用户的加密重置码
                string EncryptedResetCode= TempData["encryptedResetCode"].ToString();

                // 验证重置码的有效性
                bool isValidCode = _context.MemberInfos.Any(r => r.EncryptedResetCode == EncryptedResetCode); 
                
                var registerInfo = _context.MemberInfos.FirstOrDefault(r => r.EncryptedResetCode == EncryptedResetCode);

                //若驗證碼為有效且重置連結未過期
                if (isValidCode==true&& (registerInfo.ResetDateTime.HasValue && (DateTime.Now - registerInfo.ResetDateTime.Value).TotalMinutes <= 10))
                {

                    // 更新用户的密码
                    var user = _context.MemberInfos.FirstOrDefault(u => u.EncryptedResetCode == EncryptedResetCode);

                    
                        if (user != null)
                        {
                        // 將密碼使用 SHA256 雜湊運算(不可逆)
                        string salt = user.Email.Substring(0, 1).ToLower(); //使用信箱前一碼當作密碼鹽
                        SHA256 sha256 = SHA256.Create();
                        byte[] bytes = Encoding.UTF8.GetBytes(salt + inModel.NewPassword); //將密碼鹽及新密碼組合
                        byte[] hash = sha256.ComputeHash(bytes);
                        string newPwd = string.Concat(hash.Select(b => b.ToString("X2"))); // 雜湊運算後密碼

                        user.Password = newPwd;
                        user.NewPassword = null;
                        user.EncryptedResetCode = null;
                        _context.SaveChanges();

                        return Json(new { success = true });

                        }
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                // 处理异常
                return Json(new { success = false, error = ex.Message });
            }
        }

        //[Authorize]
        ////Google登入
        //public IActionResult GoogleLogin()
        //{
        //    var redirectUrl = Url.Action(nameof(GoogleCallback), "Member");
        //    var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}

        //[Authorize]
        ////Google回傳
        //public async Task<IActionResult> GoogleCallback()
        //{
        //    var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    if (authenticateResult.Succeeded)
        //    {
        //        // 取得使用者資訊
        //        var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
        //        var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);
        //        // 可以根據需要進行後續處理

        //        return RedirectToAction("ValidGoogleLogin", "Member"); // 登入成功後重新導向到首頁或其他頁面
        //    }
        //    else
        //    {
        //        // 登入失敗處理
        //        // 可以根據需要進行後續處理

        //        return RedirectToAction("InvalidGoogleLogin", "Member"); // 登入失敗後重新導向回登入頁面或其他處理
        //    }
        //}

    }
}

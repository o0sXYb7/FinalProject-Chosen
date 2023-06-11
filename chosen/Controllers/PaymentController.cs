using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using static chosen.ViewModel.PaymentViewModel;
using chosen.Models;
using chosen.ViewModel;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;


namespace chosen.Controllers
{
    public class PaymentController : Controller
    {
        List<KeyValuePair<string, string>> TradeInfo = new List<KeyValuePair<string, string>>();
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _config;
        private readonly FinalProjectContext _context;

        public PaymentController(ILogger<PaymentController> logger, IConfiguration config, FinalProjectContext context)
        {
            _logger = logger;
            _config = config;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaymentSuccessful()
        {
            return View();
        }

        public IActionResult PaymentUnsuccessful()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetMemberData()
        {
            // 從Cookie中獲取MemberId
            int.TryParse(HttpContext.Request.Cookies["MemberId"], out int parsedMemberId);
            
            var member = _context.MemberInfos.Find(parsedMemberId);

            if (member == null)
            {
                // 如果找不到對應的會員資料，返回適當的錯誤或轉向頁面
                return NotFound();
            }

            // 創建一個匿名對象，包含會員資料的相關屬性
            var memberData = new
            {
                isLoggedin = true,
                memberId = member.MemberId,
                memberPoint = member.Point
            };

            // 返回JSON格式的會員資料
            return Json(memberData);
        }

        [HttpGet]
        public IActionResult GetPaymentRecords()
        {
            // 從Cookie中獲取MemberId
            int.TryParse(HttpContext.Request.Cookies["MemberId"], out int parsedMemberId);

            // 根據會員id查詢儲值紀錄
            var paymentRecords = _context.Payments
                .Where(p => p.MemberId == parsedMemberId)
                .ToList();

            // 返回JSON格式的儲值紀錄資料
            return Json(paymentRecords);
        }


        //[HttpGet("{memberId}")]
        //public ActionResult<IEnumerable<Payment>> GetPaymentRecords(int memberId)
        //{
        //    // 使用 memberId 從資料庫中取得該會員的儲值紀錄
        //    var paymentRecords = _context.Payments.Where(p => p.MemberId == memberId).ToList();

        //    return Ok(paymentRecords);
        //}

        //儲值中心頁面
        public IActionResult Center()
        {
            // 檢查 Cookie 是否存在 MemberId
            if (HttpContext.Request.Cookies["MemberId"] == null)
            {
                return RedirectToAction("LoginCookieCheck", "Member"); // 導向登入頁面
            }
            else
            {
                //Local tunnel:https://4drx522l-44300.asse.devtunnels.ms/
                //Public website:https://yoyo1062705.azurewebsites.net/
                var tunnel = "https://yoyo1062705.azurewebsites.net/";
                IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                // 產生測試資訊
                ViewData["MerchantID"] = Config.GetSection("MerchantID").Value;
                ViewData["MerchantOrderNo"] = DateTime.Now.ToString("yyyyMMddHHmmss");  //訂單編號
                ViewData["ExpireDate"] = DateTime.Now.AddDays(3).ToString("yyyyMMdd"); //繳費有效期限
                ViewData["ReturnURL"] = tunnel +"Payment/CallbackReturn"; //支付完成返回商店網址
                ViewData["CustomerURL"] = tunnel +  "Payment/CallbackCustomer"; //商店取號網址
                ViewData["NotifyURL"] = tunnel + "Payment/CallbackNotify"; //支付通知網址
                ViewData["ClientBackURL"] = tunnel + "Payment/Center"; //返回商店網址

            }

            return View();
        }
              
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 傳送訂單至藍新金流
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
        {
            SendToNewebPayOut outModel = new SendToNewebPayOut();

            // 藍新金流線上付款

            //交易欄位
            List<KeyValuePair<string, string>> TradeInfo = new List<KeyValuePair<string, string>>();
            // 商店代號
            TradeInfo.Add(new KeyValuePair<string, string>("MerchantID", inModel.MerchantID));
            // 回傳格式
            TradeInfo.Add(new KeyValuePair<string, string>("RespondType", "String"));
            // TimeStamp
            TradeInfo.Add(new KeyValuePair<string, string>("TimeStamp", DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0)).ToUnixTimeSeconds().ToString()));
            // 串接程式版本
            TradeInfo.Add(new KeyValuePair<string, string>("Version", "2.0"));
            // 商店訂單編號
            TradeInfo.Add(new KeyValuePair<string, string>("MerchantOrderNo", inModel.MerchantOrderNo));
            // 訂單金額
            TradeInfo.Add(new KeyValuePair<string, string>("Amt", inModel.Amt));
            // 商品資訊
            TradeInfo.Add(new KeyValuePair<string, string>("ItemDesc", inModel.ItemDesc));
            // 繳費有效期限(適用於非即時交易)
            TradeInfo.Add(new KeyValuePair<string, string>("ExpireDate", inModel.ExpireDate));
            // 支付完成返回商店網址
            TradeInfo.Add(new KeyValuePair<string, string>("ReturnURL", inModel.ReturnURL));
            // 支付通知網址
            TradeInfo.Add(new KeyValuePair<string, string>("NotifyURL", inModel.NotifyURL));
            // 商店取號網址
            TradeInfo.Add(new KeyValuePair<string, string>("CustomerURL", inModel.CustomerURL));
            // 支付取消返回商店網址
            TradeInfo.Add(new KeyValuePair<string, string>("ClientBackURL", inModel.ClientBackURL));
            // 付款人電子信箱
            TradeInfo.Add(new KeyValuePair<string, string>("Email", inModel.Email));
            // 付款人電子信箱 是否開放修改(1=可修改 0=不可修改)
            TradeInfo.Add(new KeyValuePair<string, string>("EmailModify", "1"));
            
            string memberIdCookieValue = HttpContext.Request.Cookies["MemberId"];
            if (memberIdCookieValue != null && int.TryParse(memberIdCookieValue, out int memberid))
            {
                ViewBag.MemberId = memberid;
            }
            else
            {
                ViewBag.MemberId = null;
            }

            //信用卡 付款
            if (inModel.ChannelID == "CREDIT")
            {
                TradeInfo.Add(new KeyValuePair<string, string>("CREDIT", "1"));
            }
            //ATM 付款
            if (inModel.ChannelID == "VACC")
            {
                TradeInfo.Add(new KeyValuePair<string, string>("VACC", "1"));
            }
            string TradeInfoParam = string.Join("&", TradeInfo.Select(x => $"{x.Key}={x.Value}"));
                        
            // API 傳送欄位
            // 商店代號
            outModel.MerchantID = inModel.MerchantID;
            // 串接程式版本
            outModel.Version = "2.0";
            //交易資料 AES 加解密
            IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
            string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
            string TradeInfoEncrypt = EncryptAESHex(TradeInfoParam, HashKey, HashIV);
            outModel.TradeInfo = TradeInfoEncrypt;
            //交易資料 SHA256 加密
            outModel.TradeSha = EncryptSHA256($"HashKey={HashKey}&{TradeInfoEncrypt}&HashIV={HashIV}");
            _logger.LogDebug("Debug message");
            return Json(outModel);
        }
                
        /// <summary>
        /// 信用卡支付完成返回網址
        /// </summary>
        /// <returns></returns>
        public IActionResult CallbackReturn()
        {
            // 接收參數
            StringBuilder receive = new StringBuilder();
            foreach (var item in Request.Form)
            {
                receive.AppendLine(item.Key + "=" + item.Value + "<br>");
            }
            ViewData["ReceiveObj"] = receive.ToString();
            
            // 解密訊息
            IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
            string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
           
            string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
            NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
            receive.Length = 0;
                        
            foreach (string key in decryptTradeCollection.Keys)
            {
                    receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
                    {
                       
                    }                                        
            }

            ViewData["TradeInfo"] = receive.ToString();
            ViewData["MerchantOrderNo"] = decryptTradeCollection["MerchantOrderNo"];
            ViewData["Status"] = decryptTradeCollection["Status"];
            ViewData["Amt"] = decryptTradeCollection["Amt"];
            ViewData["TradeNo"] = decryptTradeCollection["TradeNo"];
            ViewData["ItemDesc"] = decryptTradeCollection["ItemDesc"];
            ViewData["PaymentType"] = decryptTradeCollection["PaymentType"];
            ViewData["PayTime"] = decryptTradeCollection["PayTime"];
            ViewData["Card6No"] = decryptTradeCollection["Card6No"];
            ViewData["Card4No"] = decryptTradeCollection["Card4No"];
            ViewData["Exp"] = decryptTradeCollection["Exp"];

            return View("CallbackReturn", "Payment");
        }

        public IActionResult CallbackReturnToStore(string receiveObj, string tradeInfo, string merchantOrderNo, string status, string amt, string tradeNo, string itemDesc, string paymentType, string payTime, string card6No, string card4No, string exp)
        {
            
            //更新會員點數
            var addpoint = new MemberInfo
            {
                Point = Convert.ToInt32(amt),
            };

            string memberIdCookieValue = HttpContext.Request.Cookies["MemberId"];
            int memberId;

            if (Int32.TryParse(memberIdCookieValue, out memberId))
            {
                // memberIdCookieValue 成功转换为 int 类型，可以使用 memberId 进行后续操作
                // 在此处编写您的逻辑代码
            }
            else
            {
                // memberIdCookieValue 无法转换为 int 类型，处理转换失败的情况
                // 在此处编写您的错误处理逻辑
            }

            var paymentA = new Payment
            {
                MerchantOrderNo = merchantOrderNo,
                Status = status,
                Amt = amt,
                TradeNo = tradeNo,
                ItemDesc = itemDesc,
                PaymentType = paymentType,
                PayTime = payTime,
                Card6No = card6No,
                Card4No = card4No,
                Exp = exp,
                MemberId = memberId,
            };

            var currentMemberInfo = _context.MemberInfos.FirstOrDefault(m => m.MemberId == memberId);

            if (currentMemberInfo == null)
            {
                return View("PaymentUnsuccessful", "Payment");
            }
            else
            {
                currentMemberInfo.Point += addpoint.Point;
                _context.SaveChanges();
            }

            // 將Payment模型存入資料庫
            using (var context = new FinalProjectContext())
            {
                context.Payments.Add(paymentA);
                context.SaveChanges();
            }

            // 返回一个视图
            return View("PaymentSuccessful", "Payment");
        }
        
        /// <summary>
        /// ATM支付取號完成網址
        /// </summary>
        /// <returns></returns>
        public IActionResult CallbackCustomer()
        {
            // 接收參數
            StringBuilder receive = new StringBuilder();
            foreach (var item in Request.Form)
            {
                receive.AppendLine(item.Key + "=" + item.Value + "<br>");
            }
            ViewData["ReceiveObj"] = receive.ToString();

            // 解密訊息
            IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
            string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
            string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
            NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
            receive.Length = 0;
            foreach (String key in decryptTradeCollection.AllKeys)
            {
                receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
            }
                     
            ViewData["TradeInfo"] = receive.ToString();

            ViewData["MerchantOrderNo"] = decryptTradeCollection["MerchantOrderNo"];
            ViewData["Status"] = decryptTradeCollection["Status"];
            ViewData["Amt"] = decryptTradeCollection["Amt"];
            ViewData["TradeNo"] = decryptTradeCollection["TradeNo"];
            ViewData["ItemDesc"] = decryptTradeCollection["ItemDesc"];
            ViewData["PaymentType"] = decryptTradeCollection["PaymentType"];
            ViewData["ExpireDate"] = decryptTradeCollection["ExpireDate"];
            ViewData["BankCode"] = decryptTradeCollection["BankCode"];
            ViewData["CodeNo"] = decryptTradeCollection["CodeNo"];

            return View("CallbackReturn", "Payment");
        }

        public IActionResult CallbackCustomerToStore(string receiveObj, string tradeInfo, string merchantOrderNo, string status, string amt, string tradeNo, string itemDesc, string paymentType, string expireDate, string bankCode, string codeNo)
        {
            
            //更新會員點數
            var addpoint = new MemberInfo
            {
                Point = Convert.ToInt32(amt),
            };

            string memberIdCookieValue = HttpContext.Request.Cookies["MemberId"];
            int memberId;

            if (Int32.TryParse(memberIdCookieValue, out memberId))
            {
                // memberIdCookieValue 成功转换为 int 类型，可以使用 memberId 进行后续操作
                // 在此处编写您的逻辑代码
            }
            else
            {
                // memberIdCookieValue 无法转换为 int 类型，处理转换失败的情况
                // 在此处编写您的错误处理逻辑
            }

            var paymentB = new Payment
            {
                MerchantOrderNo = merchantOrderNo,
                Status = status,
                Amt = amt,
                TradeNo = tradeNo,
                ItemDesc = itemDesc,
                PaymentType = paymentType,
                ExpireDate = expireDate,
                BankCode = bankCode,
                CodeNo = codeNo,
                MemberId = memberId,
            };

            var currentMemberInfo = _context.MemberInfos.FirstOrDefault(m => m.MemberId == memberId);

            if (currentMemberInfo == null)
            {
                return View("PaymentUnsuccessful", "Payment");
            }
            else
            {
                currentMemberInfo.Point += addpoint.Point;
                _context.SaveChanges();
            }

            // 將Payment模型存入資料庫
            using (var context = new FinalProjectContext())
            {
                context.Payments.Add(paymentB);
                context.SaveChanges();
            }

            // 執行成功的操作後，返回 PaymentSuccessful 視圖
            return View("PaymentSuccessful", "Payment");
        }



        /// <summary>
        /// 支付通知網址
        /// </summary>
        /// <returns></returns>
        public IActionResult CallbackNotify()
        {
            // 接收參數
            StringBuilder receive = new StringBuilder();
            foreach (var item in Request.Form)
            {
                receive.AppendLine(item.Key + "=" + item.Value + "<br>");
            }
            ViewData["ReceiveObj"] = receive.ToString();

            // 解密訊息
            IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
            string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
            string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
            NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
            receive.Length = 0;
            foreach (String key in decryptTradeCollection.AllKeys)
            {
                receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
            }
            ViewData["TradeInfo"] = receive.ToString();

            return View();
        }

        /// <summary>
        /// 加密後再轉 16 進制字串
        /// </summary>
        /// <param name="source">加密前字串</param>
        /// <param name="cryptoKey">加密金鑰</param>
        /// <param name="cryptoIV">cryptoIV</param>
        /// <returns>加密後的字串</returns>
        public string EncryptAESHex(string source, string cryptoKey, string cryptoIV)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(source))
            {
                var encryptValue = EncryptAES(Encoding.UTF8.GetBytes(source), cryptoKey, cryptoIV);

                if (encryptValue != null)
                {
                    result = BitConverter.ToString(encryptValue)?.Replace("-", string.Empty)?.ToLower();
                }
            }

            return result;
        }

        /// <summary>
        /// 字串加密AES
        /// </summary>
        /// <param name="source">加密前字串</param>
        /// <param name="cryptoKey">加密金鑰</param>
        /// <param name="cryptoIV">cryptoIV</param>
        /// <returns>加密後字串</returns>
        public byte[] EncryptAES(byte[] source, string cryptoKey, string cryptoIV)
        {
            byte[] dataKey = Encoding.UTF8.GetBytes(cryptoKey);
            byte[] dataIV = Encoding.UTF8.GetBytes(cryptoIV);

            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                aes.Key = dataKey;
                aes.IV = dataIV;

                using (var encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(source, 0, source.Length);
                }
            }
        }

        /// <summary>
        /// 字串加密SHA256
        /// </summary>
        /// <param name="source">加密前字串</param>
        /// <returns>加密後字串</returns>
        public string EncryptSHA256(string source)
        {
            string result = string.Empty;

            using (System.Security.Cryptography.SHA256 algorithm = System.Security.Cryptography.SHA256.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(source));

                if (hash != null)
                {
                    result = BitConverter.ToString(hash)?.Replace("-", string.Empty)?.ToUpper();
                }

            }
            return result;
        }

        /// <summary>
        /// 16 進制字串解密
        /// </summary>
        /// <param name="source">加密前字串</param>
        /// <param name="cryptoKey">加密金鑰</param>
        /// <param name="cryptoIV">cryptoIV</param>
        /// <returns>解密後的字串</returns>
        public string DecryptAESHex(string source, string cryptoKey, string cryptoIV)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(source))
            {
                // 將 16 進制字串 轉為 byte[] 後
                byte[] sourceBytes = ToByteArray(source);

                if (sourceBytes != null)
                {
                    // 使用金鑰解密後，轉回 加密前 value
                    result = Encoding.UTF8.GetString(DecryptAES(sourceBytes, cryptoKey, cryptoIV)).Trim();
                }
            }

            return result;
        }

        /// <summary>
        /// 將16進位字串轉換為byteArray
        /// </summary>
        /// <param name="source">欲轉換之字串</param>
        /// <returns></returns>
        public byte[] ToByteArray(string source)
        {
            byte[] result = null;

            if (!string.IsNullOrWhiteSpace(source))
            {
                var outputLength = source.Length / 2;
                var output = new byte[outputLength];

                for (var i = 0; i < outputLength; i++)
                {
                    output[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
                }
                result = output;
            }

            return result;
        }

        /// <summary>
        /// 字串解密AES
        /// </summary>
        /// <param name="source">解密前字串</param>
        /// <param name="cryptoKey">解密金鑰</param>
        /// <param name="cryptoIV">cryptoIV</param>
        /// <returns>解密後字串</returns>
        public static byte[] DecryptAES(byte[] source, string cryptoKey, string cryptoIV)
        {
            byte[] dataKey = Encoding.UTF8.GetBytes(cryptoKey);
            byte[] dataIV = Encoding.UTF8.GetBytes(cryptoIV);

            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                // 智付通無法直接用PaddingMode.PKCS7，會跳"填補無效，而且無法移除。"
                // 所以改為PaddingMode.None並搭配RemovePKCS7Padding
                aes.Padding = System.Security.Cryptography.PaddingMode.None;
                aes.Key = dataKey;
                aes.IV = dataIV;

                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] data = decryptor.TransformFinalBlock(source, 0, source.Length);
                    int iLength = data[data.Length - 1];
                    var output = new byte[data.Length - iLength];
                    Buffer.BlockCopy(data, 0, output, 0, output.Length);
                    return output;
                }
            }
        }
    }
}
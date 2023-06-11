using chosen.ViewModel;
using chosen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.Xml;
using System;
using System.Drawing;

namespace chosen.Controllers
{
    public class StartLotteryController : Controller
    {
        private readonly FinalProjectContext _context;

        public StartLotteryController(FinalProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult getShowRawards(int id)
        {
            var showRaward = _context.ShowRawards
                .Where(r => r.ShowRawardId == id)
                .Join(
                    _context.Factories,
                    showR => showR.FactoryId,
                    fa => fa.FactoryId,
                    (showR, fa) => new
                    {
                        showR.ShowRawardId,
                        showR.AllowStoreDay,
                        showR.OneDrawMoney,
                        showR.AddProbability,
                        showR.NowProbability,
                        showR.HasSelectNumber,
                        showR.Image,
                        showR.Series,
                        showR.Name,
                        showR.Fare,
                        showR.Introduction,
                        FactoryName = fa.Name
                    }
                )
                .ToList();

            return Json(showRaward);
        }

        [HttpGet]
        public IActionResult getShowRawardsItems(int id)
        {
            var showRawardItems = _context.ShowRawardItems
                .Where(r => r.ShowRawardId == id)
                .ToList();

            return Json(showRawardItems);
        }

        [HttpPost]
        public LotteryReturnViewModel startLottery([FromBody] StartLotteryViewModel startLottery)
        {
            // 取得資料庫資料
            var dataBase = _context.ShowRawards.Where(c => c.ShowRawardId == startLottery.PrizePoolId);
            int allowData = dataBase.Select(c => c.AllowStoreDay).FirstOrDefault();
            int oneDarkMoney = dataBase.Select(c => c.OneDrawMoney).FirstOrDefault();
            int FactoryId = dataBase.Select(c=>c.FactoryId).FirstOrDefault();
            int Fare = dataBase.Select(c=>c.Fare).FirstOrDefault();
            // 檢測獎池是否已經全數抽取完畢
            var itemDataBase = _context.ShowRawardItems.Where(c => c.ShowRawardId == startLottery.PrizePoolId);
            int laveSum = itemDataBase.Select(c => c.LaveNum).Sum(); // 剩餘數量           
            int lotteryCount = startLottery.NumList.Length; // 抽取次數

            int person = Convert.ToInt32(HttpContext.Request.Cookies["MemberId"]);
            if (person == null)
            {
                LotteryReturnViewModel lotteryReturnFail = new LotteryReturnViewModel();
                lotteryReturnFail.ReturnMsg = "fail";
                return lotteryReturnFail;
            }

            MemberInfo buyer = _context.MemberInfos.FirstOrDefault(m => m.MemberId == person);
            if (buyer == null)
            {
                LotteryReturnViewModel lotteryReturnFail = new LotteryReturnViewModel();
                lotteryReturnFail.ReturnMsg = "fail";
                return lotteryReturnFail;
            }

            if (buyer.Point < oneDarkMoney * lotteryCount)
            {
                LotteryReturnViewModel lotteryReturnFail = new LotteryReturnViewModel();
                lotteryReturnFail.ReturnMsg = "noMoney";
                return lotteryReturnFail;
            }

            var tempStorageAll = _context.TempStorages.Where(c => c.MemberId == person && c.PrizePoolId == startLottery.PrizePoolId);
            var tempStorageRecive = tempStorageAll.Where(c => c.Receive == false).FirstOrDefault();
            int payMoney = 0;

            if (tempStorageRecive == null)
            {
                buyer.Point -= (oneDarkMoney * lotteryCount) + Fare;
                payMoney = (oneDarkMoney * lotteryCount) + Fare;
            }
            else {
                buyer.Point -= (oneDarkMoney * lotteryCount);
                payMoney = (oneDarkMoney * lotteryCount);
            }

            _context.SaveChanges();

            // 建立 LotteryReturnViewModel 回傳 MODEL
            LotteryReturnViewModel lotteryReturnViewModel = new();

            // 沒選擇任何號碼
            if (lotteryCount <= 0)
            {
                lotteryReturnViewModel.ReturnMsg = "請選擇號碼";
                return lotteryReturnViewModel;
            }

            //// 如果獎池已經抽取完畢
            if (laveSum <= 0)
            {
                lotteryReturnViewModel.ReturnMsg = "該池已全數抽取完畢";
                return lotteryReturnViewModel;
            }

            // 取得資料庫內 HasSelectNumber 已被抽取掉的籤
            var hasSelectLis = dataBase.Select(c=>c.HasSelectNumber);

            // 將取得的 IQueryable<string> 轉換成 sring 格式 (註:因為內容為xx,xx,xx...，因此無法利用一般的 LinQ 語法轉換成 int[])
            string listString = string.Join("", hasSelectLis);

            // 再將 string 轉成 string array
            string[] strList = listString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // 最後把 string array 轉成 int array ()
            int[] numbersList = Array.ConvertAll(strList, int.Parse); // 已抽取序號之陣列
            // 比較2個 int[] 排除被重複抽取的內容
            IEnumerable<int> sameNumbers = numbersList.Intersect(startLottery.NumList);
            // 陣列內容數量
            int sameNumbersLen = sameNumbers.Count();

            // 防止請求端利用非正規的方式傳送請求，確定沒問題才進入抽獎判斷
            if (sameNumbersLen == 0)
            {
                // 抽獎判斷
                int sum = itemDataBase.Select(c => c.Num).Sum(); // 獎品總量
                int jackPotSum = itemDataBase.Where(c => c.IsJackpot == true).Select(c=>c.Num).Sum(); //大獎總數量
                double bigPrizeProb = Math.Floor((double)jackPotSum / sum *1000); // 大獎基礎機率

                int laveAllSum = itemDataBase.Select(c => c.LaveNum).Sum(); // 剩餘獎品總量
                int jackPotLaveSum = itemDataBase.Where(c => c.IsJackpot == true).Select(c => c.LaveNum).Sum(); //大獎剩餘總數量

                int upProbability = dataBase.Select(c => c.AddProbability).FirstOrDefault(); // 增加機率
                int nowProbability = dataBase.Select(c => c.NowProbability).FirstOrDefault(); // 目前機率
                // 獎品ID索引陣列
                List<double> prizeId = new();  // 剩餘獎品實際ID，由大獎到小獎排列

                foreach (var item in itemDataBase)
                {
                    for (int i = 0; i < item.LaveNum;i++) {
                        prizeId.Add(item.ShowRawardItemId);
                    }
                }

                // 亂數取得 1~1000 中任一數字
                Random random = new();
                List<double> sonePrizeId = new(); // 抽取出的獎品索引，對應至 prizeId

                for (int i = 0; i < lotteryCount; i++)
                {
                    while (true)
                    {
                        double randomNumber = random.Next(1, 1001);
                        double selectNum;
                        if (randomNumber <= bigPrizeProb + nowProbability)
                        {
                            selectNum = random.Next(0, jackPotLaveSum);
                        }
                        else {
                            selectNum = random.Next(jackPotLaveSum, laveAllSum);
                        }

                        bool hasNum = sonePrizeId.Contains(selectNum);

                        if (!hasNum) {
                            sonePrizeId.Add(selectNum);
                            if (selectNum < jackPotLaveSum)
                            {
                                nowProbability = 0;
                            }
                            else {
                                nowProbability += upProbability;
                            }
                            break;                          
                        }
                    }
                }

                dataBase.FirstOrDefault().NowProbability = nowProbability;
                _context.SaveChanges();

                int countListIndex = 0;
                List<int> idList = new();
                List<string> nameList = new();
                List<int> numList = new();
                List<string> imgList = new();
                DateTime nowTime = DateTime.Now;

                foreach (double item in sonePrizeId) {
                    // 開庫修改獎品剩餘數量
                    int id = (int)prizeId[(int)item];
                    var nowSelectShowRawardItem = itemDataBase.Where(c => c.ShowRawardItemId == id).FirstOrDefault();
                    nowSelectShowRawardItem.LaveNum -= 1;

                    // 並將獎品圖片名稱回傳至前台
                    string[]? imgArray = lotteryReturnViewModel.ImgList; // 取得目前的 imgList 屬性值
                    string[]? nameArray = lotteryReturnViewModel.NameList; // 取得目前的 nameArray 屬性值

                    if (imgArray == null)
                    {
                        imgArray = new string[1]; // 如果 ImgList 屬性為 null，則建立一個包含一個元素的陣列
                    }
                    else
                    {
                        Array.Resize(ref imgArray, imgArray.Length + 1); // 否則，調整陣列大小以容納新的元素
                    }

                    if (nameArray == null)
                    {
                        nameArray = new string[1];
                    }
                    else
                    {
                        Array.Resize(ref nameArray, nameArray.Length + 1); // 否則，調整陣列大小以容納新的元素
                    }

                    imgArray[imgArray.Length - 1] = nowSelectShowRawardItem.Image; // 將新的圖片路徑資料存入陣列中
                    nameArray[nameArray.Length - 1] = nowSelectShowRawardItem.Name;

                    lotteryReturnViewModel.ImgList = imgArray; // 將更新後的陣列指派回 ImgList 屬性
                    lotteryReturnViewModel.NameList = nameArray;
                    _context.SaveChanges();

                    // 將抽掉的號碼加入已抽取資料中
                    listString += startLottery.NumList[countListIndex];
                    listString += ',';
                    countListIndex++;
                    // 上列動作完成後將獎品資訊另外儲存，供後續資訊儲存
                    if (idList.IndexOf(id) == -1)
                    {
                        idList.Add(id);
                        nameList.Add(nowSelectShowRawardItem.Name);
                        numList.Add(1);
                        imgList.Add(nowSelectShowRawardItem.Image);
                    }
                    else 
                    {
                        numList[idList.IndexOf(id)] += 1;
                    }
                }

                var nowSelectShowRaward = dataBase.FirstOrDefault();

                nowSelectShowRaward.HasSelectNumber = listString;

                _context.SaveChanges();

                lotteryReturnViewModel.ReturnMsg = "購買成功!";
                lotteryReturnViewModel.NumList = startLottery.NumList;

                for (int i = 0; i < idList.Count;i++) {
                    TempStorage tempStorage = new TempStorage
                    {
                        MemberId = person,
                        PrizePoolId = startLottery.PrizePoolId,
                        PrizeId = idList[i],
                        PrizeName = nameList[i],
                        PrizeQuantity = numList[i],
                        PrizePicture = imgList[i],
                        AwardDate = nowTime,
                        Deadline = nowTime.AddDays(allowData),
                        Receive = false,
                        Created = false,
                    };
                    _context.TempStorages.Add(tempStorage);                   
                }

                _context.SaveChanges();

                DrawRecord drawRecord = new DrawRecord
                {
                    ShowRawardId = startLottery.PrizePoolId,
                    FactoryId = FactoryId,
                    MemberId = person,
                    DrawCount = lotteryCount,
                    Point = payMoney,
                    DrawTime = nowTime,
                    Settlement = false,
                    SettlementTime = null
                };
                _context.DrawRecords.Add(drawRecord);

                _context.SaveChanges();

                return lotteryReturnViewModel;
            }
            else
            {
                lotteryReturnViewModel.ReturnMsg = "請勿使用非正規的方式抽獎，如若造成系統錯誤，則後果自行負責";
                return lotteryReturnViewModel;
            }
        }
    }
}

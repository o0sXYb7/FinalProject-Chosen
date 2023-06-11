using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.ViewModels;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace FinalProject.Controllers
{
    public class ImageStoresController : Controller
    {
        private readonly FinalProjectContext _context;

        public ImageStoresController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: ImageStores
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

        public async Task<IEnumerable<ImageStore>> getImage()
        {
            return _context.ImageStores != null ?
                          _context.ImageStores :
                          null;
        }

        [HttpGet]
        public async Task<ImageStore> OPenModalToUpdate(int? id)
        {
            var imageStore = await _context.ImageStores.FindAsync(id);

            return imageStore != null ? imageStore : null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImageForFile imageForFile)
        {
            ModelState.Remove("RawardItems");

            string filename = "";

            if (imageForFile.Image != null && imageForFile.Image.Length > 0)
            {
                if (imageForFile.Image.Length > 10 * 1024 * 1024)
                {
                    //ViewBag.message = "檔案大小不能超過 10MB。";
                    ModelState.AddModelError(nameof(ImageForFile.Image), "檔案大小不能超過 10MB");
                    return BadRequest(ModelState);
                }
                if (imageForFile.Image.ContentType != "image/jpeg" && imageForFile.Image.ContentType != "image/png")
                {
                    //ViewBag.message = "只能上傳 JPG 或 PNG 格式的圖片。";
                    ModelState.AddModelError(nameof(ImageForFile.Image), "只能上傳 JPG 或 PNG 格式的圖片");
                    return BadRequest(ModelState);
                }

                // 確認圖片符合上傳格式後，建立亂數名稱
                DateTime now = DateTime.Now;
                string formattedTime = now.ToString("yyyyMMddHHmmss");

                string extension = Path.GetFileName(imageForFile.Image.FileName);
                filename = $"{formattedTime}{extension}"; //亂數命名               
            }

            ImageStore imageStore = new ImageStore() 
            {
                Series = imageForFile.Series,
                Name = imageForFile.Name,
                ImageName = filename
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(imageStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageStoreExists(imageStore.ImageId))
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        throw;
                    }
                }

                // 將圖片存放至後端伺服器資料夾中
                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//img/webDataPicture", filename);
                using (var stream = new FileStream(uploadpath, FileMode.Create))
                {
                    await imageForFile.Image.OpenReadStream().CopyToAsync(stream);
                }

                return Ok();
            }
            else
            {
                // 如果模型验证失败，则返回错误消息
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRawardItems(int id,ImageForFile imageForFile)
        {
            ModelState.Remove("Image");

            string filename = "";
            
            // 先取得本身在資料庫的資料
            ImageStore originalData = await _context.ImageStores.AsNoTracking().SingleOrDefaultAsync(e => e.ImageId == id);

            if (imageForFile.Image != null && imageForFile.Image.Length > 0)
            {
                if (imageForFile.Image.Length > 10 * 1024 * 1024)
                {
                    //ViewBag.message = "檔案大小不能超過 10MB。";
                    ModelState.AddModelError(nameof(ImageForFile.Image), "檔案大小不能超過 10MB");
                    return BadRequest(ModelState);
                }
                if (imageForFile.Image.ContentType != "image/jpeg" && imageForFile.Image.ContentType != "image/png")
                {
                    //ViewBag.message = "只能上傳 JPG 或 PNG 格式的圖片。";
                    ModelState.AddModelError(nameof(ImageForFile.Image), "只能上傳 JPG 或 PNG 格式的圖片");
                    return BadRequest(ModelState);
                }

                DateTime now = DateTime.Now;
                string formattedTime = now.ToString("yyyyMMddHHmmss");

                string extension = Path.GetFileName(imageForFile.Image.FileName);
                filename = $"{formattedTime}{extension}"; //亂數命名              
            }      

            ImageStore imageStore = new ImageStore()
            {
                ImageId = id,
                Series = imageForFile.Series,
                Name = imageForFile.Name,
                ImageName = filename == "" ? originalData.ImageName : filename
            };
          
            if (ModelState.IsValid)
            {
                _context.Entry(imageStore).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageStoreExists(imageStore.ImageId))
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        throw;
                    }
                }

                if (imageForFile.Image != null) {
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//img/webDataPicture", filename);

                    using (var stream = new FileStream(uploadpath, FileMode.Create))
                    {
                        await imageForFile.Image.OpenReadStream().CopyToAsync(stream);
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

        // DELETE: RawardLibs/Delete/5
        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            var imageStores = await _context.ImageStores.FindAsync(id);
            if (imageStores == null)
            {
                return "noData";
            }

            try
            {
                _context.ImageStores.Remove(imageStores);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "error";
            }

            return "success";
        }

        // 提供給其他 View select 的內容
        [HttpGet]
        public async Task<List<ImageStore>> getImageStores()
        {
            var imageStores = await _context.ImageStores
        .ToListAsync();

            return imageStores;
        }

        private bool ImageStoreExists(int id)
        {
          return (_context.ImageStores?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        /// <summary>
        /// ذخیره تصویر در مسیر wwwroot/uploads
        /// </summary>
        /// <param name="imageFile">فایل ارسالی</param>
        /// <returns></returns>
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "هیچ فایلی ارسال نشده است."
                });
            }

            
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

           
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";

            
            var filePath = Path.Combine(uploadsPath, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // برگرداندن پاسخ موفق
                return Ok(new
                {
                    success = true,
                    message = "تصویر با موفقیت آپلود شد.",
                    fileName = fileName,
                    fileUrl = $"/uploads/{fileName}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "خطا در ذخیره فایل.",
                    error = ex.Message
                });
            }
        }
    }
}

using Application.Dtos.Books;
using Application.Exceptions.ValidationExceptions;
using Application.Features.Definitions.Books;
using Application.Features.Implementations.Books;
using Domain.Enumorations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBookCategories _categories;
        public CategoryController(IBookCategories categories)
        {
            _categories = categories;
        }
       
        public async Task<IActionResult> listCategory(long? parentId)
        {
            var categoryList = await _categories.GetAllCategoriesAsync(parentId); 
            return View(categoryList);
        }

        public async Task<IActionResult> listSubCategory(long parentId)
        {
            if(parentId !=null)
            {
                var categoryList = await _categories.GetAllCategoriesAsync(parentId);
                return View(categoryList);
            }
            return NotFound();
          
        }

        [HttpGet]
        public async Task<ActionResult> AddSubCategory(long parentid)
        {

           if(parentid != 0)
            {
                var parent = await _categories.FindAsync(parentid);
                if (parent == null)
                    return NotFound();
                return View(parent);  
            }
            return NotFound();
           
        }
        [HttpPost]
        public async Task<IActionResult> AddSubCategory([FromBody] BookCategoriesDto dto)
        {
           

            var result = await _categories.AddChildAsync(dto);
            if (result == null)
                return BadRequest(new { success = false, message = "خطا در افزودن زیر مجموعه." });

            return Ok(new { success = true, message = "زیر مجموعه با موفقیت اضافه شد." });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(BookCategoriesDto categoriesDto)
        {
            try
            {
                var result = await _categories.AddAsync(categoriesDto); // اضافه کردن await
                return Json(new { success = true, message = "عملیات با موفقیت انجام شد!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "خطا در انجام عملیات!", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var category = await _categories.FindAsync(id); 

            if (category == null)
            {
                return NotFound();
            } 

            return View(category); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookCategoriesDto categoriesDto)
        {
            try
            {
                await _categories.UpdateAsync(categoriesDto); 
                return Json(new { success = true, message = "عملیات با موفقیت انجام شد!" });
            }
            catch
            {
                return Json(new { success = false, message = "خطا در انجام عملیات!" });
            }
        }


        [HttpGet]
        public IActionResult Delete( )
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Delete(long Id )
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }
                var category = await _categories.FindAsync(Id); 
                _categories.RemoveAsync(Id);

                return Json(new { success = true, message = "عملیات با موفقیت انجام شد!" });
            }
            catch
            {
                return View();
            }
        }
      

        
       
    }
}

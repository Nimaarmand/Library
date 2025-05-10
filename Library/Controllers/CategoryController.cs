using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Application.Features.Implementations.Books;
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
        // GET: CategoriesController
        public async Task<IActionResult> listCategory()
        {
            var categoryList = await _categories.GetAllCategoriesAsync(); 
            return View(categoryList);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]

        public IActionResult Create(BookCategoriesDto categoriesDto)
        {
            try
            {
                var result = _categories.AddAsync(categoriesDto);
                return Json(new { success = true, message = "عملیات با موفقیت انجام شد!" });
            }
            catch
            {
                return Json(new { success = false, message = "خطا در انجام عملیات!" });
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

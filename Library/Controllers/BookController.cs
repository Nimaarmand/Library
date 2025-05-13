using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookCategories _bookCategories;
        public BookController(IBookService bookService, IBookCategories bookCategories)
        {
            _bookService = bookService;
            _bookCategories = bookCategories;
        }

        public async Task<IActionResult> Booklist()
        {
            var listbook = await _bookService.GetAllBooksAsync(); // حتماً await استفاده شود
            return View(listbook);
        }


        public ActionResult IActionResult(int id)
        {
            return View();
        }


        public async Task<IActionResult> Create(long? id)
        {
            var categories = await _bookCategories.GetAllCategoriesAsync(id);

            var categoryItems = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoryItems.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });

                foreach (var child in category.Children)
                {
                    categoryItems.Add(new SelectListItem
                    {
                        Value = child.Id.ToString(),
                        Text = child.Name // نمایش با "--" برای تشخیص فرزندها
                    });
                }
            }

            ViewBag.Categories = categoryItems;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            try
            {
                var result = await _bookService.AddAsync(bookDto);
                return Json(new { success = true, message = result });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var categories = await _bookCategories.GetAllCategoriesAsync(id);

            var categoryItems = new List<SelectListItem>();

            foreach (var parentCategory in categories)
            {
                categoryItems.Add(new SelectListItem
                {
                    Value = parentCategory.Id.ToString(),
                    Text = parentCategory.Name
                });

                foreach (var child in parentCategory.Children)
                {
                    categoryItems.Add(new SelectListItem
                    {
                        Value = child.Id.ToString(),
                        Text = "-- " + child.Name 
                    });
                }
            }

            ViewBag.Categories = categoryItems;

            var book = await _bookService.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }




        [HttpPost]
       
        public IActionResult Edit(BookDto bookDto)
        {
            try
            {
                 _bookService.UpdateAsync(bookDto);
                return Json(new { success = true, message = "عملیات با موفقیت انجام شد!" });
            }
            catch
            {
                return Json(new { success = false, message = "خطا در انجام عملیات!" });
            }
        }

        // GET: BookController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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

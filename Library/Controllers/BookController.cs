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
        // GET: BookController
        public IActionResult Booklist()
        {
            return View();
        }

        // GET: BookController/Details/5
        public ActionResult IActionResult(int id)
        {
            return View();
        }


        //public async Task< IActionResult> Create()
        //{
        //    var categories = _bookCategories.GetAllCategoriesAsync().Result;

        //    var categoryItems = categories.Select(c => new SelectListItem
        //    {
        //        Value = c.Id.ToString(),
        //        Text = c.ChildName != null
        //            ? $"{c.Name} - {c.ChildName}"
        //            : c.Name
        //    }).ToList();

        //    ViewBag.Categories = categoryItems;

        //    return View();
        //}



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

        // GET: BookController/Edit/5
        public IActionResult Edit(int Id)
        {

            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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

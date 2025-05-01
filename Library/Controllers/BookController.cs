using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Application.Features.Implementations.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _BookService;
        public BookController(BookService bookService)
        {
            _BookService = bookService;
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

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]     
        public IActionResult Create(BookDto bookDto)
        {
            try
            {
                _BookService.AddAsync(bookDto);
                return RedirectToAction(nameof(Index));
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

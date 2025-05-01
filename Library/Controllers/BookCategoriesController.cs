using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookCategoriesController : Controller
    {
        // GET: BookCategoriesController
        public IActionResult Index()
        {
            return View();
        }

        // GET: BookCategoriesController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: BookCategoriesController/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(IFormCollection collection)
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


        public IActionResult Edit(int id)
        {
            return View();
        }


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

        public IActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]

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

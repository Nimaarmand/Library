using Application.Features.Definitions.Books;
using Application.Features.Implementations.Books;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using System.IO;

namespace Library.Controllers
{
    public class ReportController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IBookService bookService, IWebHostEnvironment webHostEnvironment)
        {
            _bookService = bookService;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: ReportController
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowReportBok()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
        public IActionResult GetReportBook()
        {
            string directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, $@"Reports/Book/Book.mrt");
            StiReport report = new StiReport();
           
            report.Load(directoryPath);

            var books = _bookService.GetAllBooksAsync().Result
                .Select(b => new
                {
                    b.Name,
                    b.Author,
                    b.Subject,
                    b.BookCategories,
                    //b.InsertTime,
                }).ToList(); // فقط فیلدهای موردنظر نمایش داده شوند

            report.RegData("Books", books);

            return View(report);
        }

    }
}

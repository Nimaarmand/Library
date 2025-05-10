using Application.Features.Implementations.Books;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;

namespace Library.Controllers
{
    public class ReportController : Controller
    {
        private readonly BookService _bookService;
        public ReportController(BookService bookService)
        {
            _bookService = bookService;
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
            StiReport report = new StiReport();
            report.Load("wwwroot/Reports/BookReport.mrt");

            var books = _bookService.GetAllBooksAsync().Result
                .Select(b => new
                {
                    b.Name,
                    b.Author,
                    b.Subject,
                    b.BookCategories,
                    b.InsertTime,
                }).ToList(); // فقط فیلدهای موردنظر نمایش داده شوند

            report.RegData("Books", books);

            return View(report);
        }

    }
}

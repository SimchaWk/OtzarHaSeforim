using Microsoft.AspNetCore.Mvc;
using OtzarHaSeforim.Data;
using OtzarHaSeforim.Service;

namespace OtzarHaSeforim.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISetBooksService _booksService;

        public BookController(ApplicationDbContext context, ISetBooksService booksService)
        {
            _context = context;
            _booksService = booksService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

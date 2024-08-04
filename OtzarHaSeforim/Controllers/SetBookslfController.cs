// Ignore Spelling: Seforim Otzar

using Microsoft.AspNetCore.Mvc;
using OtzarHaSeforim.Data;
using OtzarHaSeforim.Models;
using OtzarHaSeforim.Service;
using OtzarHaSeforim.ViewModel;

namespace OtzarHaSeforim.Controllers
{
    public class SetBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISetBooksService _setBooksService;
        private readonly IShelfService _shelfService;

        public SetBooksController(
            ApplicationDbContext context, 
            ISetBooksService setBooksService, 
            IShelfService shelfService
            )
        {
            _context = context;
            _setBooksService = setBooksService;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Index(long shelfId)
        {
            ViewBag.ShelfId = shelfId;

            return View(await _setBooksService.GetSetsByShelfId(shelfId));
        }

        public IActionResult Create(long shelfId)
        {
            ViewBag.ShelfId = shelfId;

            return View(new SetBooksVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SetBooksVM setBooksVM, long shelfId)
        {
            ViewBag.ShelfId = shelfId;
            //setBooksVM.ShelfId = shelfId;

            int setWidth = setBooksVM.Books.Aggregate(0, (sum, book) => sum + book.WidthBook);
            int setHigh = setBooksVM.Books.FirstOrDefault()?.HighBook ?? 0;


            long libraryId = _context.Shelves
                    .Where(s => s.Id == shelfId)
                    .FirstOrDefault()?
                    .Id?? 0;

            long suitableShelId = _shelfService.FindSuitableShelf(libraryId, setWidth, setHigh);

            while (suitableShelId < 1)
            {
                ViewBag.RequiredWidth = setWidth;
                ViewBag.RequiredHeight = setHigh;
                ViewBag.Message = $"No suitable shelf found. Please add a new shelf with at least {setWidth} width, and least {setHigh} high.";
                ViewBag.RequiredWidth = setWidth;
                return View("RequestNewShelf");
            }

            setBooksVM.ShelfId = suitableShelId;
            string libraryGenre = _context.Libraries
                .Where(l => l.Id == libraryId)
                .FirstOrDefault()
                .GenreLibrary;

            setBooksVM.Books.Select(b => b.GenderBook = libraryGenre);

            SetBooksModel newSetBooks = await _setBooksService.AddSetBooks(setBooksVM);

            return RedirectToAction("Index", new { id = ViewBag.ShelfId });
        }
    }
}

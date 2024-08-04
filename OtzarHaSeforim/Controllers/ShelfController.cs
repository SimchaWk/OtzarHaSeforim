// Ignore Spelling: Seforim Otzar

using Microsoft.AspNetCore.Mvc;
using OtzarHaSeforim.Data;
using OtzarHaSeforim.Models;
using OtzarHaSeforim.Service;
using OtzarHaSeforim.ViewModel;

namespace OtzarHaSeforim.Controllers
{
    public class ShelfController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShelfService _shelfService;

        public ShelfController(ApplicationDbContext context, IShelfService shelfService)
        {
            _context = context;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Details(long id)
        {
            ViewBag.Id = id;
            return View(await _shelfService.GetLibraryShelvesAsync(id));
        }

        public async Task<IActionResult> Create(long libraryId)
        {
            ViewBag.Id = libraryId;
            return View(new ShelfVM());
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShelfVM shelfVM, long libraryId)
        {
            ViewBag.Id = libraryId;
            await _shelfService.AddShelfToLibrary(shelfVM, libraryId);
            return RedirectToAction("Details", new { id = ViewBag.Id });
        }
    }
}

// Ignore Spelling: Otzar Seforim

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using OtzarHaSeforim.Data;
using OtzarHaSeforim.Models;
using OtzarHaSeforim.ViewModel;

namespace OtzarHaSeforim.Service
{
    public class ShelfService : IShelfService
    {
        private readonly ApplicationDbContext _context;

        public ShelfService(ApplicationDbContext context) { _context = context; }

        public async Task<List<ShelfModel>> GetLibraryShelvesAsync(long id) =>
            await _context.Shelves
            .Where(shelf => shelf.LibraryId == id)
            .Include(shelf => shelf.SetBooks)
            .ThenInclude(set => set.Books)
            .ToListAsync();

        public async Task<ShelfModel?> AddShelfToLibrary(ShelfVM? shelfVM, long libraryId)
        {
            if (shelfVM == null) { return null; }   

            ShelfModel newShelf = new()
            { 
                HighShelf = shelfVM.HighShelf, 
                WidthShelf =  shelfVM.WidthShelf,
                LibraryId = libraryId
            };

            await _context.Shelves.AddAsync(newShelf);
            await _context.SaveChangesAsync();

            return newShelf;
        }

        public async Task<ShelfModel?> GetShelfById(long shelfId) => 
            await _context.Shelves
            .Where(shelf => shelf.Id == shelfId)
            .FirstAsync();

        public int GetShelfFreeSpace(long shelfId)
        {
            ShelfModel? shelf = _context.Shelves.FirstOrDefault(s => s.Id == shelfId);

            int occupiedSpace = shelf.SetBooks
                .SelectMany(set => set.Books)
                .Select(x => x.WidthBook)
                .Sum();

            return shelf != null ? (shelf.WidthShelf - occupiedSpace) : -1;
        }

        public long FindSuitableShelf(long libraryId, int setWidth, int setHigh) =>
            _context.Shelves
            .Where(s => s.LibraryId == libraryId)
            .AsEnumerable()
                .FirstOrDefault(
                    s => GetShelfFreeSpace(s.Id) >= setWidth && 
                    s.HighShelf >= setHigh
                )?.Id ?? -1;

/*        public long FindSuitableShelf(long libraryId, int setWidth, int setHigh)
        {
            ShelfModel? suitableShelf = _context.Shelves
                .Where(s => s.LibraryId == libraryId)
                .OrderBy(s => s.HighShelf)
                .AsEnumerable()
                .OrderBy(s => GetShelfFreeSpace(s.Id))
                .FirstOrDefault(
                    s => GetShelfFreeSpace(s.Id) >= setWidth &&
                    s.HighShelf >= setHigh
                );

            long? suitableShelfId = suitableShelf?.Id;

            return suitableShelfId ?? -1;
        }*/


    }
}

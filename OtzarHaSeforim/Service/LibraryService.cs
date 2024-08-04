// Ignore Spelling: Otzar Seforim

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OtzarHaSeforim.Data;
using OtzarHaSeforim.Models;
using OtzarHaSeforim.ViewModel;

namespace OtzarHaSeforim.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDbContext _context;

        public LibraryService(ApplicationDbContext context) { _context = context; }

        public async Task<List<LibraryModel>> GetAllLibrariesAsync() => 
            await _context.Libraries.Include(library => library.Shelves)
            .ThenInclude(shlf => shlf.SetBooks)
            .ThenInclude(set => set.Books)
            .ToListAsync();

        public async Task<LibraryModel?> AddLibrary(LibraryVM? libraryVM)
        {
            if(libraryVM == null) { return null; }

            LibraryModel? existingLibrary = await _context.Libraries
                .FirstOrDefaultAsync(library => library.GenreLibrary == libraryVM.GenreLibrary);

            if (existingLibrary != null) 
            { 
                throw new Exception($"Genre '{libraryVM.GenreLibrary}' already exists."); 
            };

            LibraryModel newLibrary = new()
            { GenreLibrary = libraryVM.GenreLibrary };

            await _context.Libraries.AddAsync(newLibrary);
            await _context.SaveChangesAsync();

            return newLibrary;
        }

        public LibraryModel? GetLibraryById(long? id)
        {
            if (id == null) { return null; };

            return _context.Libraries.Find(id);
        }

        public bool RemoveLibrary(long? id)
        {
            LibraryModel? libraryToDelete = _context.Libraries.Find(id);
            if(libraryToDelete != null)
            {
                _context.Libraries.Remove(libraryToDelete);
                return true;
            }
            return false;
        }

        public async Task SortLIbraryShelves(long libraryId)
        {
            List<ShelfModel> shelves = await _context.Shelves
                .Where(shelf => shelf.LibraryId == libraryId)
                .ToListAsync();

            if (shelves == null) { return; }
        }

        public Task SortLIbraryShelvesBy(long libraryId)
        {
            throw new NotImplementedException();
        }
    }
}

using OtzarHaSeforim.Models;
// Ignore Spelling: Otzar Seforim

using OtzarHaSeforim.ViewModel;

namespace OtzarHaSeforim.Service
{
    public interface IShelfService
    {
        Task<List<ShelfModel>> GetLibraryShelvesAsync(long id);

        Task<ShelfModel?> AddShelfToLibrary(ShelfVM shelfVM, long libraryId);

        Task<ShelfModel?> GetShelfById(long shelfId);

        int GetShelfFreeSpace(long shelfId);

        long FindSuitableShelf(long libraryId, int setWidth, int setHigh);
    }
}

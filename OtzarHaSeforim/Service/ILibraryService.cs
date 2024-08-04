// Ignore Spelling: Otzar Seforim

using OtzarHaSeforim.Models;
using OtzarHaSeforim.ViewModel;

namespace OtzarHaSeforim.Service
{
    public interface ILibraryService
    {
        Task<List<LibraryModel>> GetAllLibrariesAsync();

        Task<LibraryModel?> AddLibrary(LibraryVM libraryVM);

        LibraryModel? GetLibraryById(long? id);

        bool RemoveLibrary(long? id);

        Task SortLIbraryShelvesBy(long libraryId);
    }
}

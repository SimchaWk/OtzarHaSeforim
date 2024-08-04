// Ignore Spelling: Otzar Seforim

using OtzarHaSeforim.Models;
using OtzarHaSeforim.ViewModel;

namespace OtzarHaSeforim.Service
{
    public interface ISetBooksService
    {
        Task<List<SetBooksModel>> GetSetsByShelfId(long shelfId);

        Task<SetBooksModel> AddSetBooks(SetBooksVM setBooksVM);

        Task<SetBooksModel> LinkSetToShelf(long setId, long shelfId);

        bool hasEnoughShelfWidth(long shelfId, int widthSet);

        int SumSetWidth(long setId);

        bool CheckShelfHeight(long shelfId, long setId);

        SetBooksVM UpdateBooksGenre(SetBooksVM setBooksVM, long libraryId);
    }
}

// Ignore Spelling: Otzar Seforim

using Microsoft.EntityFrameworkCore;
using OtzarHaSeforim.Data;
using OtzarHaSeforim.Models;
using OtzarHaSeforim.ViewModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace OtzarHaSeforim.Service
{
    public class SetBooksService : ISetBooksService
    {
        private readonly ApplicationDbContext _context;

        public SetBooksService(ApplicationDbContext context) { _context = context; }

        public async Task<List<SetBooksModel>> GetSetsByShelfId(long shelfId) =>
            await _context.Sets
            .Where(set => set.ShelfId == shelfId)
            .Include(set => set.Books)
            .ToListAsync();

        public void AddSet(SetBooksModel set, long libraryId)
        {
            List<ShelfModel> shelvesLibrary = _context.Shelves
                .Where(shelf => shelf.LibraryId == libraryId)
                .OrderBy(shelf => shelf.HighShelf)
                .ToList();
        }

        public bool hasEnoughShelfWidth(long shelfId, int widthSet)
        {
            ShelfModel? shelf = _context.Shelves
                .Include(s => s.SetBooks)
                .ThenInclude(set => set.Books)
                .FirstOrDefault(s => s.Id == shelfId);

            if (shelf == null) { return false; }

            int shelfWidth = shelf.WidthShelf;

            int occupiedSpace = shelf.SetBooks
                .SelectMany(set => set.Books)
                .Aggregate(0, (sum, book) => sum + book.WidthBook);

            return widthSet <= (shelfWidth - occupiedSpace);
        }

        public int SumSetWidth(long setId)
        {
            SetBooksModel? set = _context.Sets
                .Include(set => set.Books)
                .FirstOrDefault(set => set.Id == setId);

            if (set == null)
            {
                return 0;
            }

            return set.Books.Sum(book => book.WidthBook);
        }

        public bool CheckShelfHeight(long shelfId, long setId)
        {
            throw new NotImplementedException();
        }


        public async Task<SetBooksModel> AddSetBooks(SetBooksVM setBooksVM)
        {
            var books = await Task.WhenAll(
                setBooksVM.Books
                .Select(b => ConvertBookVM(b))
                .ToList()
            );

            SetBooksModel setBooks = new()
            {
                Title = setBooksVM.Title,
                ShelfId = setBooksVM.ShelfId,
                Books = books.ToList(),
            };

            await _context.Sets.AddAsync(setBooks);
            await _context.SaveChangesAsync();
            return setBooks;
        }

        public async Task<BookModel> ConvertBookVM(BookVM bookVM)
        {
            BookModel book = new BookModel
            {
                BookName = bookVM.BookName,
                GenderBook = bookVM.GenderBook,
                HighBook = bookVM.HighBook,
                WidthBook = bookVM.WidthBook,
            };

            return await Task.FromResult(book);
        }

        public async Task<SetBooksModel> LinkSetToShelf(long setId, long shelfId)
        {
            if (setId == 0 || shelfId == 0)
            {
                throw new ArgumentNullException($"{setId} / {shelfId} are null.");
            }

            SetBooksModel? set = await _context.Sets
                .Where(set => set.Id == setId)
                .FirstOrDefaultAsync();

            ShelfModel? shelf = await _context.Shelves
                .Where(shelf => shelf.Id == shelfId)
                .FirstOrDefaultAsync();

            set.ShelfId = shelfId;
            await _context.SaveChangesAsync();

            return set;
        }

        public SetBooksVM UpdateBooksGenre(SetBooksVM setBooksVM, long libraryId)
        {
            LibraryModel? library = _context.Libraries
                .Where(l => l.Id == libraryId)
                .FirstOrDefault();

            string libraryGenre = library?.GenreLibrary ?? "UnknownGenre";

            setBooksVM.Books.ForEach(b => b.GenderBook = libraryGenre);

            return setBooksVM;
        }
    }
}

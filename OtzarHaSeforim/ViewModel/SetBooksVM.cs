using OtzarHaSeforim.Models;

namespace OtzarHaSeforim.ViewModel
{
    public class SetBooksVM
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public long ShelfId { get; set; }

        public ShelfVM ShelfParent { get; set; }

        public List<BookVM> Books { get; set; } = [];
    }
}

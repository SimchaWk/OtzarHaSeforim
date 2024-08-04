// Ignore Spelling: Seforim Otzar 

using OtzarHaSeforim.Models;
using System.ComponentModel.DataAnnotations;

namespace OtzarHaSeforim.ViewModel
{
    public class ShelfVM
    {
        public long Id { get; set; }

        [Range(10, 50, ErrorMessage = "The value must be between 10 and 50")]
        public int HighShelf { get; set; }

        [Range(20, 200, ErrorMessage = "The value must be between 20 and 200")]
        public int WidthShelf { get; set; }

        public long LibraryId { get; set; }

        public LibraryVM LibraryParent { get; set; }

        public List<SetBooksVM> SetBooks { get; set; } = [];
    }
}

// Ignore Spelling: Otzar Seforim

using OtzarHaSeforim.Models;
using System.ComponentModel.DataAnnotations;

namespace OtzarHaSeforim.ViewModel
{
    public class LibraryVM
    {
        public long Id { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Genre library must contain at least 2 letters")]
        public string GenreLibrary { get; set; } = string.Empty;

        public List<ShelfVM> Shelves { get; set; } = [];
    }
}

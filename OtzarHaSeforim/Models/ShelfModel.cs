// Ignore Spelling: Otzar Seforim

using System.ComponentModel.DataAnnotations;

namespace OtzarHaSeforim.Models
{
    public class ShelfModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public required int HighShelf { get; set; }

        [Required]
        public required int WidthShelf { get; set; }

        public long LibraryId { get; set; }

        public LibraryModel LibraryParent { get; set; }

        public List<SetBooksModel> SetBooks { get; set; } = [];
    }
}
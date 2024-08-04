// Ignore Spelling: Otzar Seforim

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OtzarHaSeforim.Models
{
    [Index(nameof(GenreLibrary), IsUnique = true)]
    public class LibraryModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string GenreLibrary { get; set; }

        public List<ShelfModel> Shelves { get; set; } = [];
    }
}

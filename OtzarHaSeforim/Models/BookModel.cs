// Ignore Spelling: Otzar Seforim

using System.ComponentModel.DataAnnotations;

namespace OtzarHaSeforim.Models
{
    public class BookModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string BookName { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string GenderBook { get; set; }

        [Required]
        public required int HighBook { get; set; }

        [Required]
        public required int WidthBook { get; set; }

        public long SetBooksId { get; set; }

        public SetBooksModel SetBooksParent { get; set; }
    }
}
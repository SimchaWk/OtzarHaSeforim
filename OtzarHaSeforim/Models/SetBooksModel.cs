// Ignore Spelling: Otzar Seforim Shelt 

using System.ComponentModel.DataAnnotations;

namespace OtzarHaSeforim.Models
{
    public class SetBooksModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public long ShelfId { get; set; }

        public ShelfModel ShelfParent { get; set; }

        public List<BookModel> Books { get; set; } = [];
    }
}
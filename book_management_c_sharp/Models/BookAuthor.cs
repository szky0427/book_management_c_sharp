using System.ComponentModel.DataAnnotations.Schema;

namespace book_management_c_sharp.Models
{
    [Table("book_authors")]
    public class BookAuthor
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}

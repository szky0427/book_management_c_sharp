using book_management_c_sharp.Data;
using book_management_c_sharp.Models;

namespace book_management_c_sharp.Repository
{
    public class BookAuthorRepository
    {
        private readonly BookDbContext _context;

        public BookAuthorRepository(BookDbContext context)
        {
            _context = context;
        }

        public void AddBookAuthor(int bookId, int authorId)
        {
            BookAuthor bookAuthor = new BookAuthor{ BookId = bookId, AuthorId = authorId};
            _context.BookAuthors.Add(bookAuthor);
            _context.SaveChanges();
        }

        public bool RemoveBookAuthor(int bookId)
        {
            var bookAuthor = _context.BookAuthors.Where(ba => ba.BookId == bookId);
           
            if (bookAuthor == null) return false;

            foreach(var ba in bookAuthor)
            {
            _context.BookAuthors.Remove(ba);

            }
            _context.SaveChanges();
            return true;
        }
    }
}

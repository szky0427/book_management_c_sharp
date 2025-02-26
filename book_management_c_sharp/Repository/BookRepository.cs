using book_management_c_sharp.Data;
using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.Models;
using book_management_c_sharp.Status;
using Microsoft.EntityFrameworkCore;

namespace book_management_c_sharp.Repository
{
    public class BookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BookResponseDto> GetBooks(string? title, int? bookId, string? authorName)
        {
            var subquery = from bookAuthor in _context.BookAuthors
                           join author in _context.Authors on bookAuthor.AuthorId equals author.Id
                           where (string.IsNullOrEmpty(authorName) || author.Name.Contains(authorName))
                           select bookAuthor.BookId;

            var query = from book in _context.Books
                        join bookAuthor in _context.BookAuthors on book.Id equals bookAuthor.BookId
                        join author in _context.Authors on bookAuthor.AuthorId equals author.Id
                        where(!subquery.Any() || subquery.Contains(book.Id)) // 左がtrueの時点で右は評価されない
                        where(string.IsNullOrEmpty(title) || book.Title.Contains(title))
                        where(!bookId.HasValue || book.Id == bookId.Value)
                        group author by new { book.Id, book.Title, book.Price, book.PublishStatus } into bookGroup
                        select new BookResponseDto
                        {
                            Id = bookGroup.Key.Id,
                            Title = bookGroup.Key.Title,
                            Price = bookGroup.Key.Price,
                            PublishStatus = bookGroup.Key.PublishStatus,
                            PublishStatusName = PublishStatusExtensions.ToValue(bookGroup.Key.PublishStatus),
                            Authors = bookGroup.Select(a => new AuthorResponseDto
                            {
                                Id = a.Id,
                                Name = a.Name,
                                BirthDate = a.BirthDate.ToString("yyyy-MM-dd"),
                            }).ToList()
                        };

            return query.ToList();
        }

        public BookResponseDto GetBookByBookId(int bookId)
        {
            var result = (from book in _context.Books
                          join bookAuthor in _context.BookAuthors on book.Id equals bookAuthor.BookId
                          join author in _context.Authors on bookAuthor.AuthorId equals author.Id
                          where book.Id == bookId
                          group author by new { book.Id, book.Title, book.Price, book.PublishStatus } into bookGroup
                          select new BookResponseDto
                          {
                              Id = bookGroup.Key.Id,
                              Title = bookGroup.Key.Title,
                              Price = bookGroup.Key.Price,
                              PublishStatus = bookGroup.Key.PublishStatus,
                              PublishStatusName = PublishStatusExtensions.ToValue(bookGroup.Key.PublishStatus),
                              Authors = bookGroup.Select(a => new AuthorResponseDto
                              {
                                  Id = a.Id,
                                  Name = a.Name,
                                  BirthDate = a.BirthDate.ToString("yyyy-MM-dd"),
                              }).ToList()
                          }).FirstOrDefault();

            if (result == null) throw new Exception("この書籍は存在しません。");

            return result;
        }

        public int AddBook(string title, int price, string publishStatus)
        {
            Book book 
                = new Book
                {
                    Title = title,
                    Price = price,
                    PublishStatus = publishStatus
                };

            _context.Books.Add(book);
            _context.SaveChanges(); // DBに保存。この時点でIDがセットされる。

            return book.Id;
        }

        public Boolean ExistsByBookId(int bookId)
        {
            return _context.Books.Any(a => a.Id == bookId);
        }

        public Boolean ExistsByBookIdAndPublished(int bookId)
        {
            return _context.Books.Any(a => a.Id == bookId && a.PublishStatus == "1");
        }

        public void UpdateBook(int bookId, string title, int price, string publishStatus)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null) throw new Exception("書籍が見つかりません");

            book.Title = title;
            book.Price = price;
            book.PublishStatus = publishStatus;
            _context.SaveChanges();
        }


    }
}

using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.DTOs.Request;
using book_management_c_sharp.Repository;
using book_management_c_sharp.Status;

namespace book_management_c_sharp.Service
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;
        private readonly BookAuthorRepository _bookAuthorRepository;

        public BookService(BookRepository bookRepository, AuthorRepository authorRepository,
            BookAuthorRepository bookAuthorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookAuthorRepository = bookAuthorRepository;
        }
        
        public IEnumerable<BookResponseDto> GetBooks(string? title, int? bookId, string? authorName)
        {
            return _bookRepository.GetBooks(title, bookId, authorName);
        }

        public int AddBook(BookRequestDto book)
        {
            // 著者テーブルに存在する著者であることを確認
            foreach (int authorId in book.AuthorsIds)
            {
                if (!_authorRepository.ExistsByAuthorId(authorId))
                {
                    throw new Exception("この著者は存在しません。");
                }
            }

            // 書籍テーブルに登録し書籍IDを取得
            int bookId = _bookRepository.AddBook(book.Title, book.Price, book.PublishStatus);

            // 書籍・著者テーブルに登録
            foreach (int authorId in book.AuthorsIds)
            {
                _bookAuthorRepository.AddBookAuthor(bookId, authorId);
            }

            return bookId;

        }

        public int UpdateBook(int bookId, BookRequestDto book)
        {
            // 対象の書籍が存在するか確認
            if(!_bookRepository.ExistsByBookId(bookId)) throw new Exception("この書籍は存在しません。");

            // 出版済の書籍を未出版に変更しようとしていないか確認
            if (!_bookRepository.ExistsByBookIdAndPublished(bookId) &&
                book.PublishStatus == PublishStatus.UnPublished.ToString())
            {
                throw new Exception("出版済を未出版に変更できません。");
            }

            // 著者テーブルに存在する著者であることを確認
            foreach (int authorId in book.AuthorsIds)
            {
                if (!_authorRepository.ExistsByAuthorId(authorId))
                {
                    throw new Exception("この著者は存在しません。");
                }
            }

            // 書籍情報を更新
            _bookRepository.UpdateBook(bookId, book.Title, book.Price, book.PublishStatus);

            // 既存の書籍・著者の関連を削除
            _bookAuthorRepository.RemoveBookAuthor(bookId);

            Console.WriteLine(book.AuthorsIds);

            // 書籍・著者テーブルに登録
            foreach (int authorId in book.AuthorsIds)
            {
                _bookAuthorRepository.AddBookAuthor(bookId, authorId);
            }

            return bookId;
        }

        public BookResponseDto GetBookByBookId(int bookId)
        {
            return _bookRepository.GetBookByBookId(bookId);
        }
    }
}

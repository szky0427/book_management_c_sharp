using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.DTOs.Request;
using book_management_c_sharp.Service;
using Microsoft.AspNetCore.Mvc;

namespace book_management_c_sharp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookResponseDto>> GetBooks(string? title, int? bookId, string? authorName)
        {
            return Ok(_bookService.GetBooks(title, bookId, authorName));
        }

        [HttpGet("{bookId}")]
        public ActionResult<BookResponseDto> GetBookByBookId(int bookId)
        {
            try
            {
                return Ok(_bookService.GetBookByBookId(bookId));
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreateBook([FromBody] BookRequestDto book)
        {
            try
            {
                _bookService.AddBook(book);
                return Ok("書籍が登録されました。" );
            }
            catch (Exception ex)
            {
                return BadRequest($"書籍情報を登録できませんでした : {ex.Message}");
            }

        }

        [HttpPut("{bookId}")]
        public ActionResult UpdateBook(int bookId, [FromBody] BookRequestDto book)
        {
            try
            {
                _bookService.UpdateBook(bookId, book);
                return Ok("書籍が更新されました。");
            }
            catch (Exception ex)
            {
                return BadRequest($"書籍情報を更新できませんでした : {ex.Message}");
            }

        }
    }
}

using book_management_c_sharp.DTOs.Request;
using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.Service;
using Microsoft.AspNetCore.Mvc;

namespace book_management_c_sharp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<List<AuthorResponseDto>> GetAuthors(string? name, int? authorId, DateTime? birthDate)
        {
            return Ok(_authorService.GetAuthors(name, authorId, birthDate));
        }

        [HttpPost]
        [Route("create")]
        public ActionResult AddAuthor([FromBody]AuthorRequestDto author)
        {
            try
            {
                _authorService.AddAuthor(author);
                return Ok("著者情報を登録しました。");
            } 
            catch (Exception ex)
            {
                return BadRequest($"著者情報を登録できませんでした : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{authorId}")]
        public ActionResult<AuthorResponseDto> GetByAuthorId(int authorId)
        {
            try
            {
                return Ok(_authorService.GetByAuthorId(authorId));
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut]
        [Route("{authorId}")]
        public ActionResult UpdateAuthor(int authorId, [FromBody]AuthorRequestDto author)
        {
            try
            {
                _authorService.UpdateAuthor(authorId, author);
                return Ok("著者情報を更新しました。");
            }
            catch (Exception ex)
            {
                return BadRequest($"著者情報を更新できませんでした：{ex.Message}");
            }
        }
    }
}

using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.Service;
using Microsoft.AspNetCore.Mvc;

namespace book_management_c_sharp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class PulldownController : ControllerBase
    {
        private readonly PulldownService _pulldownService;

        public PulldownController(PulldownService pulldownService)
        {
            _pulldownService = pulldownService;
        }

        [HttpGet("authors")]
        public ActionResult<AuthorResponseDto> GetAuthors()
        {
            return Ok(_pulldownService.GetAuthors());
        }
    }
}

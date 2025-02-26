using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.Repository;

namespace book_management_c_sharp.Service
{
    public class PulldownService
    {
        private readonly AuthorRepository _authorRepository;

        public PulldownService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<AuthorResponseDto> GetAuthors()
        {
            return _authorRepository.GetAll();
        }
    }
}

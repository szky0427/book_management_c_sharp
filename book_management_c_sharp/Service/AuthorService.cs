using book_management_c_sharp.DTOs.Request;
using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.Repository;

namespace book_management_c_sharp.Service
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<AuthorResponseDto> GetAuthors(string? name, int? authorId, DateTime? birthDate)
        {
            return _authorRepository.GetAuthors(name, authorId, birthDate);
        }

        public void AddAuthor(AuthorRequestDto author)
        {
            // 生年月日が今日より過去であることを確認
            if (!IsBeforeToday(author.BirthDate)) throw new Exception("生年月日は現在の日付より過去である必要があります。");

            _authorRepository.AddAuthor(author.Name, author.BirthDate);

        }

        public AuthorResponseDto GetByAuthorId(int authorId)
        {
            return _authorRepository.GetByAuthorId(authorId);
        }

        public void UpdateAuthor(int authorId, AuthorRequestDto author)
        {
            // 更新対象の著者が存在することを確認
            if (!_authorRepository.ExistByAuthorId(authorId)) throw new Exception("この著者は登録されていません");

            // 生年月日が今日より過去であることを確認
            if (!IsBeforeToday(author.BirthDate)) throw new Exception("生年月日は現在の日付より過去である必要があります。");

            _authorRepository.UpdateAuthor(authorId, author.Name, author.BirthDate);
        }

        public Boolean IsBeforeToday(DateTime date)
        {
            return date.CompareTo(DateTime.Now) < 0;
        }
    }
}

using book_management_c_sharp.Data;
using book_management_c_sharp.DTOs.Response;
using book_management_c_sharp.Models;

namespace book_management_c_sharp.Repository
{
    public class AuthorRepository
    {
        private readonly BookDbContext _context;

        public AuthorRepository(BookDbContext context)
        {
            _context = context;
        }

        public Boolean ExistsByAuthorId(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }

        public List<AuthorResponseDto> GetAll()
        {
            return _context.Authors.Select(a => new AuthorResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                BirthDate = a.BirthDate.ToString("yyyy-MM-dd"),
            }).OrderBy(a => a.Id).ToList();
        }

        public List<AuthorResponseDto> GetAuthors(string? name, int? authorId, DateTime? birthDate)
        {
            return _context.Authors
                .Where(a => string.IsNullOrEmpty(name) || a.Name.Contains(name))
                .Where(a => !authorId.HasValue || a.Id == authorId)
                .Where(a => !birthDate.HasValue || a.BirthDate.Date == DateTime.SpecifyKind(birthDate.Value.Date, DateTimeKind.Utc)) // UTCに変換
                .Select(a => new AuthorResponseDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthDate = a.BirthDate.ToString("yyyy-MM-dd"),
                }).OrderBy (a => a.Id).ToList();
        }

        public AuthorResponseDto GetByAuthorId(int authorId)
        {
            var response = _context.Authors
                .Where(a => a.Id == authorId)
                .Select(a => new AuthorResponseDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthDate = a.BirthDate.ToString("yyyy-MM-dd")
                }).FirstOrDefault();

            if(response == null) throw new Exception("この著者は存在しません。");

            return response;
        }

        public int AddAuthor(string name, DateTime birthDate)
        {
            Author author = new Author
            {
                Name = name,
                BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author.Id;
        }

        public Boolean ExistByAuthorId(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }

        public void UpdateAuthor(int authorId, string name,  DateTime birthDate)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == authorId);

            if (author == null) throw new Exception("著者が見つかりません");

            author.Name = name;
            author.BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
            _context.SaveChanges();
        }
    }
}

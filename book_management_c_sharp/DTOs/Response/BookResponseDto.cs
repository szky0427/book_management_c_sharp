namespace book_management_c_sharp.DTOs.Response
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string PublishStatus { get; set; }
        public string PublishStatusName { get; set; }
        public List<AuthorResponseDto> Authors { get; set; }
    }
}

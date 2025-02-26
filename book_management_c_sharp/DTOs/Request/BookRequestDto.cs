namespace book_management_c_sharp.DTOs.Request
{
    public class BookRequestDto
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public string PublishStatus { get; set; }
        public List<int> AuthorsIds { get; set; }
    }
}

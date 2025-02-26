using System.ComponentModel.DataAnnotations.Schema;

namespace book_management_c_sharp.Models
{
    [Table("books")]
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string PublishStatus { get; set; }
      //  public string PublishStatusName {  get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }

    }
}

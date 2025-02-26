using System.ComponentModel.DataAnnotations.Schema;

namespace book_management_c_sharp.Models
{
    [Table("authors")]
    public class Author
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

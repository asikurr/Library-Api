using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Api.Entities
{
    public class Books
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        [ForeignKey("Authores")]
        public int AuthorID { get; set; }
        public virtual Authors? Authores { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace Library.Api.Entities
{
    public class Authors
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }
    }
}

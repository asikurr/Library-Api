using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class AuthorViewModel
    {
        public int AuthorID { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string AuthorBio { get; set; }
    }
}

using Library.Api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Models
{
    public class BorrowBookViewModel
    {
        public int BorrowID { get; set; }
        public DateTime BorrowDate { get; set; }
        [ForeignKey("Members")]
        public int MemberID { get; set; }
        public virtual Members? Members { get; set; }
        [ForeignKey("Books")]
        public int BookID { get; set; }
        public virtual Books? Books { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
    }
}

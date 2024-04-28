using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class MemberViewModel
    {
        public int MemberID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class MemberViewModel
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

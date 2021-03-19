using System;
using System.Collections.Generic;
using System.IO;

namespace CannaBro.Models
{
    public class CurrentUserData
    {
        public static string userID;

        public string FileName { get; set; }
        public string UID { get; set; }
        public DateTime MemberSince { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Initials { get; set; }
        public string[] Favorites { get; set; }
    }
}

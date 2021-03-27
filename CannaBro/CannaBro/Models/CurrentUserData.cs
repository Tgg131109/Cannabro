using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
=======
using System.IO;
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1

namespace CannaBro.Models
{
    public class CurrentUserData
    {
<<<<<<< HEAD
        //private static List<StrainData> recents { get; set; }
        //private List<StrainData> favorites { get; set; }
        //public event PropertyChangedEventHandler PropertyChanged;

        public static string userID;
=======
        public static string userID;

>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
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
<<<<<<< HEAD
        public string[] Recents { get; set; }
        //public static List<StrainData> Recents
        //{
        //    get { return recents; }
        //    set
        //    {
        //        recents = value;
        //        OnPropertyChanged(nameof(Recents));
        //    }
        //}

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace CannaBro.Models
{
    public class CurrentUserData
    {
        //private static List<StrainData> recents { get; set; }
        //private List<StrainData> favorites { get; set; }
        //public event PropertyChangedEventHandler PropertyChanged;

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
    }
}

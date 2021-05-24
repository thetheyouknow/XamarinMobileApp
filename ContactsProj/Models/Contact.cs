using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace ContactsProj.Models
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        //public DateTime Due { get; set; }
        public ImageSource pic
        {
            get
            {
                return ImageSource.FromResource("ContactsProj.Pic.narutoFace.png");
            }
        }
    }
}

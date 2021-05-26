using ContactsProj.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
namespace ContactsProj.ViewModels
{
    public class ContactItemViewModel: ViewModel
    {
        public Contact Item { get; private set; }
        public ContactItemViewModel(Contact item) => Item = item;
        public EventHandler ItemStatusChanged;
        
    }
}

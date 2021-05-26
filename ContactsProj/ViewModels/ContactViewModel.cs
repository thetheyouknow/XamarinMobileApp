using System;
using ContactsProj.Models;
using ContactsProj.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ContactsProj.Views;
namespace ContactsProj.ViewModels
{
    public class ContactViewModel : ViewModel
    {
        private ContactItemRepository repository;

        public Contact Item { get; set; }
        public ContactViewModel(ContactItemRepository repository)
        {
            this.repository = repository;
            Item = new Contact();
        }

        public ICommand Save => new Command(async () =>
        {
            await repository.AddOrUpdate(Item);
            await Navigation.PopAsync();
        });

        public ICommand Delete => new Command(async () =>
        {
            await repository.Delete(Item);
            await Navigation.PopAsync();
        });
    }

}

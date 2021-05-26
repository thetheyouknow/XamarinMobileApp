
using ContactsProj.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ContactsProj.Views;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using ContactsProj.Models;

namespace ContactsProj.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ContactItemRepository repository;
        public ObservableCollection<ContactItemViewModel> Items { get; set; }

        public MainViewModel(ContactItemRepository repository)
        {
            repository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {
            var items = await repository.GetItems();
            items = items.ToList();

            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<ContactItemViewModel>(itemViewModels);
        }

        private ContactItemViewModel CreateTodoItemViewModel(Contact item)
        {
            var itemViewModel = new ContactItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is ContactItemViewModel item)
            {
                Task.Run(async () => await repository.UpdateItem(item.Item));
            }
        }
        public ContactItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(ContactItemViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemView = Resolver.Resolve<ContactView>();
            var vm = itemView.BindingContext as ContactViewModel;
            vm.Item = item.Item;

            await Navigation.PushAsync(itemView);
        }

        public ICommand Add => new Command(async () =>
        {
            var itemView = Resolver.Resolve<ContactView>();
            await Navigation.PushAsync(itemView);
        });
    }
}

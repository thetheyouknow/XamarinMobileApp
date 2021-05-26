using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContactsProj.Models;

namespace ContactsProj.Repositories
{
    public interface IContactItemRepository
    {
        event EventHandler<Contact> OnItemAdded;
        event EventHandler<Contact> OnItemUpdated;
       

        Task<List<Contact>> GetItems();
        Task AddItem(Contact item);
        Task UpdateItem(Contact item);
        Task AddOrUpdate(Contact item);
        Task Delete(Contact item);
    }
}

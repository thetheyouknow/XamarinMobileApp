using ContactsProj.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ContactsProj.Repositories;

namespace ContactsProj.Repositories
{
    public class ContactItemRepository : IContactItemRepository
    {
        private SQLiteAsyncConnection connection;

        public event EventHandler<Contact> OnItemAdded;
        public event EventHandler<Contact> OnItemUpdated;
        public event EventHandler<Contact> OnItemDeleted;


        private async Task CreateConnection()
        {
            if (connection != null)
            {
                return;
            }

            var documentPath = Environment.GetFolderPath(
                               Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "ContactItems.db");

            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<Contact>();

            if (await connection.Table<Contact>().CountAsync() == 0)
            {
                await connection.InsertAsync(new Contact() { FirstName = "Johnny Appleseed" });
            }
        }


        public async Task<List<Contact>> GetItems()
        {
            await CreateConnection();
            return await connection.Table<Contact>().ToListAsync();
        }

        public async Task AddItem(Contact item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task AddOrUpdate(Contact item)
        {
            if (item.Id == 0)
            {
                await AddItem(item);
            }
            else
            {
                await UpdateItem(item);
            }
        }

        public async Task UpdateItem(Contact item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }

        public async Task Delete(Contact item)
        {
            await CreateConnection();
            await connection.Table<Contact>().DeleteAsync(c => c.Id == item.Id);
            OnItemDeleted?.Invoke(this, item);
        }
    }
}
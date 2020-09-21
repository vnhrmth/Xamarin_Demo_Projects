using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApp.Models;
using SQLite;
using System.IO;

namespace DemoApp.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private SQLiteAsyncConnection _connection;

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;

        private async Task CreateConnection()
        {
            // Check if there is an existing connection and return if its there
            if (_connection != null)
            {
                return;
            }
            
            // create connection to database
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Get database path and create a new file called odoItems.db
            var databasePath = Path.Combine(documentPath, "TodoItems.db");

            // start connection
            _connection = new SQLiteAsyncConnection(databasePath);

            // create a table asynchronously
            await _connection.CreateTableAsync<TodoItem>();

            // default item is added just for this example.
            if (await _connection.Table<TodoItem>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new TodoItem()
                {
                    Title = "Welcome to Todo",
                    Due = DateTime.Now
                });
            }
        }

        public async Task AddItem(TodoItem item)
        {
            await CreateConnection();
            await _connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task AddOrUpdateItem(TodoItem item)
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

        public async Task<List<TodoItem>> GetItems()
        {
            await CreateConnection();
            return await _connection.Table<TodoItem>().ToListAsync();
        }

        public async Task UpdateItem(TodoItem item)
        {
            await CreateConnection();
            await _connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }
    }
}

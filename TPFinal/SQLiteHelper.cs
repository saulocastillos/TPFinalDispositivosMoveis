using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TPFinal
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Book>().Wait();
        }

        //Insert and Update new record
        public Task<int> SaveItemAsync(Book book)
        {
            if (book.BookId != 0)
            {
                return db.UpdateAsync(book);
            }
            else
            {
                return db.InsertAsync(book);
            }
        }

        //Delete
        public Task<int> DeleteItemAsync(Book book)
        {
            return db.DeleteAsync(book);
        }

        //Read All Items
        public Task<List<Book>> GetItemsAsync()
        {
            return db.Table<Book>().ToListAsync();
        }


        //Read Item
        public Task<Book> GetItemAsync(int bookId)
        {
            return db.Table<Book>().Where(i => i.BookId == bookId).FirstOrDefaultAsync();
        }
    }
}

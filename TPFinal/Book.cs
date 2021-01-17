using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPFinal
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookISBN { get; set; }
        public string BookAuthor { get; set; }
        public string BookAuthorEmail { get; set; }
    }
}

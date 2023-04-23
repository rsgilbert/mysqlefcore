using Microsoft.EntityFrameworkCore;

using System;
using System.Text;

namespace mysqlcore
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertData();
            PrintData();
        }

        private static void InsertData()
        {
            using (var context = new LibraryContext())
            {
                context.Database.EnsureCreated();
                // add a publisher
                var publisher = new Publisher
                {
                    Name = "Okumu publishers Ltd"
                };
                context.Publisher.Add(publisher);

                context.Book.Add(new Book
                {
                    ISBN = "IS10000",
                    Author = "Jonah",
                    Language = "English",
                    Pages  = 1000,
                    Title= "Mysteries of the underworld",
                    Publisher = publisher
                });
                context.Book.Add(new Book
                {
                    ISBN = "IS20000",
                    Author = "Jack Ma",
                    Language ="Chinese",
                    Title = "How to be rich",
                    Pages = 300,
                    Publisher = publisher
                });
                context.SaveChanges();
            }
        }

        private static void PrintData()
        {
            using (var context = new LibraryContext())
            {
                var books = context.Book
                    .Include(b => b.Publisher);
                foreach(var book in books)
                {
                    var data = new StringBuilder();
                    data.AppendLine($"ISBN: {book.ISBN}");
                    data.AppendLine($"Title: {book.Title}");
                    data.AppendLine($"Publisher: {book.Publisher.ID} -> {book.Publisher.Name}");
                    data.AppendLine("*****");
                    Console.WriteLine(data);
                }
            }
        }
    }
}
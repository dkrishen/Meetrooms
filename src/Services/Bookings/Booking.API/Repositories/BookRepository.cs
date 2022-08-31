using MRA.Bookings.Data;
using MRA.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MRA.Bookings.Repositories
{
    public class BookRepository : RepositoryBase, IBookRepository
    {
        public BookRepository(MRABooksDbContext context) : base(context) { }

        public void AddBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void DeleteBook(Guid id)
        {
            Book book = context.Books.Where(b => b.Id == id).SingleOrDefault();
            context.Books.Remove(book);
            context.SaveChanges();
        }

        public IEnumerable<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public IEnumerable<Book> GetBooksByUser(Guid userId)
        {
            return context.Books.Where(b => b.UserId == userId).ToList();
        }

        public void UpdateBook(Book book)
        {
            var result = context.Books.SingleOrDefault(b => b.Id == book.Id);
            if (result != null)
            {
                result.StartTime = book.StartTime;
                result.EndTime = book.EndTime;
                result.Date = book.Date;
                context.SaveChanges();
            }
        }
    }
}

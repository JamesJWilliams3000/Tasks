using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class BookRepository
    {
        public static void Add(Book book)
        {
            using (var context = new JOBSEntities())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public static void Add(List<Book> books)
        {
            using (var context = new JOBSEntities())
            {
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }

        public static Book MakeBook(string title, int pages, DateTime date)
        {
            return new Book()
            {
                ID = GetNewID(),
                Title = title.Trim(),
                Pages = pages,
                Bought_Date = date
            };
        }

        public static void Add(string title, int pages, DateTime date)
        {
            Book book = MakeBook(title, pages, date);

            using (var context = new JOBSEntities())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public static int GetNewID()
        {
            using (var context = new JOBSEntities())
            {
                if (context.Books.Count() > 0)
                {
                    return context.Books.OrderByDescending(x => x.ID).FirstOrDefault().ID + 1;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}

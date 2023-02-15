using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller;
            bookDbContext.Genre.AddRange(
                SF = new Genre() { Name = "SF"},
                Classic = new Genre() { Name = "Classic" },
                Romance = new Genre() { Name = "Romance" },
                Thriller = new Genre() { Name = "Thriller" }
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            int i = 0;
            for (; i < 25; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Genres = new() { SF} });
            }
            for(; i < 50 ; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Genres = new() { Classic } });
            }
            for (; i < 75; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Genres = new() { Romance } });
            }
            for (; i < 100; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Genres = new() { Thriller } });
            }
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}
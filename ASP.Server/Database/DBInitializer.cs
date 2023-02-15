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
			Random random = new Random();
			const string chars = "abcdefghijklmnopqrstuvwxyz";
			string randomName = new string(Enumerable.Repeat(chars, 5)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
			int i = 0;
			float randomPrice = (float)(random.NextDouble() * 100);
			for (; i < 25; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Author = randomName, Price = randomPrice, Genres = new() { SF} });
				randomPrice = (float)(random.NextDouble() * 100);
				randomName = new string(Enumerable.Repeat(chars, 5)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
			}
            for(; i < 50 ; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Author = randomName, Price = randomPrice, Genres = new() { Classic } });
				randomPrice = (float)(random.NextDouble() * 100);
				randomName = new string(Enumerable.Repeat(chars, 5)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
			}
            for (; i < 75; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Author = randomName, Price = randomPrice, Genres = new() { Romance } });
				randomPrice = (float)(random.NextDouble() * 100);
				randomName = new string(Enumerable.Repeat(chars, 5)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
			}
            for (; i < 100; i++)
            {
                bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Author = randomName, Price = randomPrice, Genres = new() { Thriller, Romance } });
				randomPrice = (float)(random.NextDouble() * 100);
				randomName = new string(Enumerable.Repeat(chars, 5)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
			}
			for (; i < 115; i++)
			{
				bookDbContext.Books.Add(new Book() { Title = $"Book{i}", Author = randomName, Price = randomPrice, Genres = new() { Thriller } });
				randomPrice = (float)(random.NextDouble() * 100);
				randomName = new string(Enumerable.Repeat(chars, 5)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
			}
			// Vous pouvez initialiser la BDD ici

			bookDbContext.SaveChanges();
        }
    }
}
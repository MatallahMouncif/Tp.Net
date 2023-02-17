using ASP.Server.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.Server.Database
{
	public class DbInitializer
	{
		public static void Initialize(LibraryDbContext bookDbContext)
		{
			if (bookDbContext.Books.Any())
			{
				return;
			}

			Genre sf, classic, romance, thriller;
			bookDbContext.Genre.AddRange(
				sf = new Genre() { Name = "SF" },
				classic = new Genre() { Name = "Classic" },
				romance = new Genre() { Name = "Romance" },
				thriller = new Genre() { Name = "Thriller" }
			);
			bookDbContext.SaveChanges();

			Random random = new Random();
			const string chars = "abcdefghijklmnopqrstuvwxyz";
			for (int i = 0; i < 115; i++)
			{
				string randomName = new string(Enumerable.Repeat(chars, 5)
					.Select(s => s[random.Next(s.Length)])
					.ToArray());

				float randomPrice = (float)(random.NextDouble() * 100);

				var book = new Book
				{
					Title = $"Book{i}",
					Author = new Author { Name = randomName },
					Price = randomPrice,
					Genres = new List<Genre>()
				};

				if (i < 25)
				{
					book.Genres.Add(sf);
				}
				else if (i < 50)
				{
					book.Genres.Add(classic);
				}
				else if (i < 75)
				{
					book.Genres.Add(romance);
				}
				else if (i < 100)
				{
					book.Genres.Add(thriller);
					book.Genres.Add(romance);
				}
				else
				{
					book.Genres.Add(thriller);
				}

				bookDbContext.Books.Add(book);
			}

			bookDbContext.SaveChanges();
		}
	}
}

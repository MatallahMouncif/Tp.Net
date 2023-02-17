using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; internal set; }

        public DbSet<Author> Author { get; internal set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity(j => j.ToTable("BookGenre"));

			modelBuilder.Entity<Book>()
		.HasOne(b => b.Author)
		.WithMany(a => a.Books)
		.HasForeignKey(b => b.AuthorId);

			// Configure the navigation properties for the Book and Genre models
			modelBuilder.Entity<Book>()
                .HasIndex(b => b.Title); // example index for Book Name

            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.Name);

            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Genre>().ToTable("Genre");
			modelBuilder.Entity<Author>().ToTable("Author");

		}
	}
}
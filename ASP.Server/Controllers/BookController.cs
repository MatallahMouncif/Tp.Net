using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Diagnostics;

namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        [Required]
        [Display(Name = "Auteur")]
        public string Author { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Prix")]
        public float Price { get; set; }


        // Liste des genres séléctionné par l'utilisateur
        [Display(Name = "Genres")]
        public List<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init;  }
    }

    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Book> ListBooks = libraryDbContext.Books.Include(b => b.Genres).ToList();
            List<Genre> AllGenres = libraryDbContext.Genre.ToList();
            ViewData["AllGenres"] = AllGenres;
            return View(ListBooks);
        }

        public ActionResult<CreateBookModel> Create(CreateBookModel model)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Map the view model to a Book entity
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Price = model.Price,
                    Genres = model.Genres.Select(id => libraryDbContext.Genre.Find(id)).ToList()
                };
              
                libraryDbContext.Add(book);
                libraryDbContext.SaveChanges();
                ModelState.Clear();

            }



            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = libraryDbContext.Genre.ToList() } );
        }
        // I need to add a delete function
        public ActionResult Delete(int id)
        {
            var book = libraryDbContext.Books.Find(id);
            libraryDbContext.Books.Remove(book);
            libraryDbContext.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Edit(int id, string title, string author, float price, List<int> genres)
        {
            var book = libraryDbContext.Books.Include(b => b.Genres).SingleOrDefault(b => b.Id == id);

            if (book != null)
            {
                // Update the book properties
                book.Title = title;
                book.Author = author;
                book.Price = price;

                // Update the book's genres
                Console.WriteLine(book.Title);
                Console.WriteLine(book.Author);
                Console.WriteLine(book.Price);
                Console.WriteLine(book.Genres.Count);
                for (int i = 0;i < genres.Count; i++)
                {
                    Console.WriteLine("genre : " + genres[i]);
                }
                book.Genres.Clear();
                book.Genres.AddRange(genres.Select(genreId => libraryDbContext.Genre.Find(genreId)));

                // Save the changes to the database
                libraryDbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP.Server.Controllers
{
    public class CreateGenreModel
    {
        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public IEnumerable<Book> AllBooks { get; init; }


    }
    public class GenreController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Genre>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Genre> ListGenres = libraryDbContext.Genre.ToList();
            return View(ListGenres);
        }

        // A vous de faire comme BookController.List mais pour les genres !

        public ActionResult<CreateGenreModel> Create(CreateGenreModel model) 

        {
         if (ModelState.IsValid)
            {
                var genre = new Genre
                {
                    Name = model.Name
                };

                libraryDbContext.Genre.Add(genre);
                libraryDbContext.SaveChanges();

            }

            var allBooks = libraryDbContext.Books.ToList();
            var createGenreModel = new CreateGenreModel { AllBooks = allBooks };

            return View(new CreateGenreModel() { AllBooks = libraryDbContext.Books.ToList() });



        }
    }

   
}

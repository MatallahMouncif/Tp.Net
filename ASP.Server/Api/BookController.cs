using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;
using ASP.Server.DTO;
namespace ASP.Server.Api
{

    [Route("/api/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        // Methode a ajouter : 
        // - GetBooks
        //   - Entrée: Optionel -> Liste d'Id de genre, limit -> defaut à 10, offset -> défaut à 0
        //     Le but de limit et offset est dé créer un pagination pour ne pas retourner la BDD en entier a chaque appel
        //   - Sortie: Liste d'object contenant uniquement: Auteur, Genres, Titre, Id, Prix
        //     la liste restourner doit être compsé des élément entre <offset> et <offset + limit>-
        //     Dans [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20] si offset=8 et limit=5, les élément retourner seront : 8, 9, 10, 11, 12

        // - GetBook
        //   - Entrée: Id du livre
        //   - Sortie: Object livre entier

        // - GetGenres
        //   - Entrée: Rien
        //   - Sortie: Liste des genres

        // Aide:
        // Pour récupéré un objet d'une table :
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.First()
        // Pour récupéré des objets d'une table :
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.ToList()
        // Pour faire une requète avec filtre:
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Skip().<Selecteurs>
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Take().<Selecteurs>
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Where(x => x == y).<Selecteurs>
        // Pour récupérer une 2nd table depuis la base:
        //   - .Include(x => x.yyyyy)
        //     ou yyyyy est la propriété liant a une autre table a récupéré
        //
        // Exemple:
        //   - Ex: libraryDbContext.MyObjectCollection.Include(x => x.yyyyy).Where(x => x.yyyyyy.Contains(z)).Skip(i).Take(j).ToList()


        // Je vous montre comment faire la 1er, a vous de la compléter et de faire les autres !
        [HttpGet]
        [Route("book")]
        public ActionResult<List<BookDTO>> GetBooks(int limit = 10, int offset = 0, [FromQuery] int[] idGenres = null)
        {
            List<Book> books = new List<Book>();
            int totalBooks = 0;
            if (idGenres == null || idGenres.Length == 0)
            {
                totalBooks = libraryDbContext.Books.Count();
                books = libraryDbContext.Books.Include(x => x.Genres).Include(a => a.Author).Skip(offset).Take(limit).ToList();
            }
            else
            {
                totalBooks = libraryDbContext.Books.Include(x => x.Genres).Include(a => a.Author).Where(x => x.Genres.Any(y => idGenres.Contains(y.Id))).Count();
                books = libraryDbContext.Books.Include(x => x.Genres).Include(a => a.Author).Where(x => x.Genres.Any(y => idGenres.Contains(y.Id))).Skip(offset).Take(limit).ToList();
            }
            List<BookDTO> booksDTO = new List<BookDTO>();
            foreach (Book book in books)
            {
                Console.WriteLine(book.Author);
                Console.WriteLine(book.Author.Name);
                BookDTO bookDTO = new BookDTO();
                bookDTO.Id = book.Id;
                bookDTO.Title = book.Title;
                bookDTO.Author = book.Author.Name;
                bookDTO.Price = (float)Math.Round(book.Price, 2);
                bookDTO.Genres = new List<GenreDTO>();
                foreach (Genre genre in book.Genres)
                {
                    GenreDTO genreDTO = new GenreDTO();
                    genreDTO.Id = genre.Id;
                    genreDTO.Name = genre.Name;
                    bookDTO.Genres.Add(genreDTO);
                }
                booksDTO.Add(bookDTO);
            }

            int endIndex = offset + limit - 1;
            endIndex = endIndex > totalBooks - 1 ? totalBooks - 1 : endIndex;
            Response.Headers.Add("Pagination", $"{offset}-{endIndex}/{totalBooks}");
            return booksDTO;
        }
        [HttpGet]
        [Route("book/{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            Book book = libraryDbContext.Books.Include(x => x.Genres).Include(a => a.Author).First(x => x.Id == id);
            BookDTO bookDTO = new BookDTO();
            bookDTO.Id = book.Id;
            bookDTO.Title = book.Title;
            bookDTO.Author = book.Author.Name;
            bookDTO.Price = book.Price;
            bookDTO.Genres = new List<GenreDTO>();
            foreach (Genre genre in book.Genres)
            {
                GenreDTO genreDTO = new GenreDTO();
                genreDTO.Id = genre.Id;
                genreDTO.Name = genre.Name;
                bookDTO.Genres.Add(genreDTO);
            }
            return book;
        }

    }
}


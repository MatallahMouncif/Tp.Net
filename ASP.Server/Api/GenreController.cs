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
    public class GenreController : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        // - GetGenres
        //   - Entrée: Rien
        //   - Sortie: Liste des genres
        [HttpGet]
        [Route("genre")]
        public ActionResult<List<GenreDTO>> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            genres = libraryDbContext.Genre.ToList();
            List<GenreDTO> genresDTO = new List<GenreDTO>();
            foreach (Genre genre in genres)
            {
                GenreDTO genreDTO= new GenreDTO();
                genreDTO.Id = genre.Id;
                genreDTO.Name = genre.Name;
                genresDTO.Add(genreDTO);
            }
            return genresDTO;

            
        }

    }
}


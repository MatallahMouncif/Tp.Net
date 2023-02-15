using ASP.Server.Model;
using System.Collections.Generic;

namespace ASP.Server.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }

        public List<GenreDTO> Genres { get; set; }
    }
}

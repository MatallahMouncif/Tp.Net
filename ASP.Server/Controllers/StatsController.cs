using System.Collections.Generic;
using System.Linq;
using ASP.Server.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ASP.Server.Controllers
{
    public class StatsController : Controller
    {
        private readonly LibraryDbContext _context;

        public StatsController(LibraryDbContext context)
        {
            _context = context;
        }

        public ActionResult<StatsViewModel> Stats()
        {
            var bookCount = _context.Books.Count();
            Trace.WriteLine(bookCount);
            List<int> NbrMots = new(); 
            foreach (var books in _context.Books) {
                NbrMots.Add(books.Content.Split(' ').Length);
            }

            NbrMots.Sort(); 

            

            
           //var authorBookCount = _context.Books.GroupBy(b => b.Author).ToDictionary(g => g.Key, g => g.Count());
            // get the contennt and split in by a space or a comma or a dot or back to line or a tabulation
            // then count the number of words

                   

            var statsViewModel = new StatsViewModel
            {
                BookCount = bookCount,
                //AuthorBookCount = authorBookCount,
                MaxWordCount = NbrMots.Max(),
                MinWordCount = NbrMots.Min(),
                MedianWordCount = GetMedian(NbrMots),
                AverageWordCount = NbrMots.Average()
            };

            return View(statsViewModel);
        }

       private static double GetMedian(List<int> list)
        {
            int count = list.Count();
            list.Sort();
            if (count % 2 == 0)
            {
                int a = list[count / 2 - 1];
                int b = list[count / 2];
                return (a + b) / 2.0;
            }
            else
            {
                return list[count / 2];
            }
        }
    }

    public class StatsViewModel
    {
        public int BookCount { get; set; }
       // public Dictionary<Author, int> AuthorBookCount { get; set; }
        public int MaxWordCount { get; set; }
        public int MinWordCount { get; set; }
        public double MedianWordCount { get; set; }
        public double AverageWordCount { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Core.Tokens;

namespace ASP.Server.Model
{
	public class Book
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }

		public string Title { get; set; }

		[ForeignKey("Author")]
		public int AuthorId { get; set; }
		public Author Author { get; set; }

		public float Price { get; set; }
		public string Content { get; set; }

		public List<Genre> Genres { get; set; }
	}
}

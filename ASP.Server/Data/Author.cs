using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ASP.Server.Model;
using Newtonsoft.Json;

public class Author
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }

	public string Name { get; set; }

	[JsonIgnore]
	public List<Book> Books { get; set; }
}
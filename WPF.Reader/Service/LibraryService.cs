using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPF.Reader.Api;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        private BookApi bookApi = new BookApi();
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>() {
            
        };

        public LibraryService()
        {
            try {
                //List<Book> books = bookApi.BookGetBooks();
                List<Book> books = new List<Book>()
                {
                    new Book(),
                    new Book(),
                    new Book(),
                    new Book(),
                    new Book()
                };
                Books.Clear();
                books.ForEach(book => Books.Add(book));
            } catch { }
            
        }



        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 
    }
}

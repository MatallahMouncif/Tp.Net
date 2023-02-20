using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPF.Reader.Api;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        private BookApi bookApi = new BookApi();
        private GenreApi genreApi = new GenreApi();
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<BookDTO> Books { get; set; } = new ObservableCollection<BookDTO>()
        {
            
        };

        private readonly Subject<bool> _isLoading = new Subject<bool>();

        public IObservable<bool> IsLoading { get { return _isLoading; } }
        
        public ObservableCollection<GenreDTO> Genres { get; set; } = new ObservableCollection<GenreDTO>()
        {

        };

        public LibraryService()
        {
            this._isLoading.OnNext(false);
            Task.Run(() =>
            {
                this.RefreshGenres();
                this.RefreshBooks();
            });
        }

        public async void RefreshBooks (int genreId = 0, int page = 0)
        {
            this._isLoading.OnNext(true);
            List<int> idGenres = new List<int>();
            idGenres.Add(genreId);
            var genreRequest = genreId == 0 ? null : idGenres;
            List<BookDTO> books = await this.bookApi.BookGetBooksAsync(offset : page,idGenres : genreRequest);
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (books != null)
                {
                this.Books.Clear();
                books.ForEach(book => this.Books.Add(book));
                }
            });
            this._isLoading.OnNext(false);
        }

        public async void RefreshGenres ()
        {
            this._isLoading.OnNext(true);
            List<GenreDTO> genres = await this.genreApi.GenreGetGenresAsync();
            Application.Current.Dispatcher.Invoke(() =>
            {
                if(genres != null)
                {
                    this.Genres.Clear();
                    this.Genres.Add(new GenreDTO());
                    genres.ForEach(gen => this.Genres.Add(gen));
                }
            });
            this._isLoading.OnNext(false);
        } 



        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 
    }
}

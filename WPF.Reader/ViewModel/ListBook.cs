using CommunityToolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Reader.Api;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; set; }

        private GenreDTO _selectedGenre;
        public GenreDTO SelectedGenre
        {
            get { return _selectedGenre; }
            set {
                _selectedGenre = value;

                var api = new BookApi();
                var genresId = new List<int>();
                genresId.Add(SelectedGenre.Id);

                var genreRequest = SelectedGenre.Id == 0 ? null : genresId;

                var Listd = api.BookGetBooksAsync(idGenres: genreRequest);
            }
        }

        public ObservableCollection<BookDTO> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        public ObservableCollection<GenreDTO> Genres => Ioc.Default.GetRequiredService<LibraryService>().Genres;

        public ListBook()
        {
            ItemSelectedCommand = new RelayCommand(book => { /* the livre devrais etre dans la variable book */ });
        }

        public void cmbs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenreDTO genreDTOgen = (GenreDTO)sender;
            GenreDTO genreDTOgen2 = (GenreDTO)sender;

        }
    }
}

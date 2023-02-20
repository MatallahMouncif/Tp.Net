using CommunityToolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
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
        private BookDTO _selectedBook;
        public int SelectedPage { get; set; }
        public GenreDTO SelectedGenre
        {
            get { return _selectedGenre; }
            set {
                _selectedGenre = value;
                Task.Run(() =>
                {
                    Ioc.Default.GetRequiredService<LibraryService>().RefreshBooks(genreId: SelectedGenre.Id);
                });
            }
        }

        public BookDTO SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;

                if (this.SelectedBook != null)
                    Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(this.SelectedBook);
            }
        }

        public ObservableCollection<BookDTO> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        public ObservableCollection<GenreDTO> Genres => Ioc.Default.GetRequiredService<LibraryService>().Genres;

        public ListBook()
        {
            this.SelectedPage= 0;
            this.SelectedGenre = new GenreDTO();
            ItemSelectedCommand = new RelayCommand(book => { /* the livre devrais etre dans la variable book */ });
        }
    }
}

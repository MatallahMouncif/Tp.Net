using CommunityToolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Api;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

       
        public ICommand ReadCommand { get; init; } = new RelayCommand(bookFullInfo => {
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(bookFullInfo);
        });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        private BookDTO _currentBook;

        public Book BookFullInfo { get; set; }

        public BookDTO CurrentBook
        {
            get { return _currentBook; }
            set
            {
                _currentBook = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentBook)));
            }
        }

        private List<Genre> _genres;
        public List<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Genres)));
            }
        }

        public DetailsBook(BookDTO book)
        {
            var bookApi = new BookApi();
            
            this.CurrentBook = book;
            this.BookFullInfo = bookApi.BookGetBook(book.Id);
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new BookDTO() { Title = "Test Book" }) { }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

       
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => { /* A vous de définir la commande */ });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        private Book _currentBook;
        private static Book _book = new Book
        {
            Title = "The Lord of the Rings",
            Author = "J.R.R. Tolkien",
            Price = 20,
            Genres = new List<Genre>
        {
            new Genre { Name = "Fantasy" },
            new Genre { Name = "Adventure" }
        }
        };
        public Book CurrentBook
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

        public DetailsBook(Book book)
        {
            

        }

        public DetailsBook() {
            CurrentBook = _book;

        }
       
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book() { Title = "Test Book" }) { }
    }
}
